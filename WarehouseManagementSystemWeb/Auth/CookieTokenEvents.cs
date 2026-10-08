using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;

namespace WarehouseManagementSystemWeb.Auth
{
    public static class CookieTokenEvents
    {
        /// <summary>Refresh the access token this many seconds BEFORE it really expires.</summary>
        private const int RefreshSkewSeconds = 60;

        /// <summary>
        /// Runs on every request that carries the auth cookie. If the access token (inside the cookie) is about to
        /// expire it silently calls the API's RefreshToken endpoint and stores the new tokens in the cookie.
        /// If the API says the session is over (revoked / expired / account disabled) the user is signed out.
        /// </summary>
        public static async Task ValidatePrincipalAsync(CookieValidatePrincipalContext context)
        {
            var properties = context.Properties;

            var accessToken = properties.GetTokenValue(AuthConstants.AccessToken);
            var refreshToken = properties.GetTokenValue(AuthConstants.RefreshToken);
            var expiresRaw = properties.GetTokenValue(AuthConstants.ExpiresAt);

            if (string.IsNullOrEmpty(accessToken)
                || string.IsNullOrEmpty(refreshToken)
                || !DateTimeOffset.TryParse(expiresRaw, CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind, out var expiresAt))
            {
                await RejectAsync(context);
                return;
            }

            if (expiresAt > DateTimeOffset.UtcNow.AddSeconds(RefreshSkewSeconds))
            {
                return; // token still good
            }

            var refresher = context.HttpContext.RequestServices.GetRequiredService<ITokenRefresher>();
            var result = await refresher.RefreshAsync(refreshToken, context.HttpContext.RequestAborted);

            if (result.Success && result.Data != null)
            {
                AuthSignIn.UpdateTokens(properties, result.Data);
                context.ShouldRenew = true; // re-issue the cookie with the new tokens
                return;
            }

            // 503/504 = API temporarily unreachable: keep the user signed in, they can retry in a moment.
            if (result.StatusCode is 503 or 504)
            {
                return;
            }

            await RejectAsync(context);
        }

        private static async Task RejectAsync(CookieValidatePrincipalContext context)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
