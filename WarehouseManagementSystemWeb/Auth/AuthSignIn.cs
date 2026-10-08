using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using System.Security.Claims;
using WarehouseManagementSystemWeb.Models.Auth;

namespace WarehouseManagementSystemWeb.Auth
{
    /// <summary>
    /// Turns the API's login response into the MVC auth cookie.
    /// The cookie is encrypted + HttpOnly; the JWT access token and the refresh token live INSIDE it
    /// (as authentication tokens), so they never reach browser JavaScript.
    /// </summary>
    public static class AuthSignIn
    {
        public static async Task SignInAsync(HttpContext http, AuthTokenResponse tokens, bool persistent)
        {
            var principal = BuildPrincipal(tokens);

            var properties = new AuthenticationProperties
            {
                IsPersistent = persistent,
                IssuedUtc = DateTimeOffset.UtcNow,
                ExpiresUtc = AsUtc(tokens.RefreshTokenExpiresAtUtc)
            };

            UpdateTokens(properties, tokens);

            await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        }

        public static ClaimsPrincipal BuildPrincipal(AuthTokenResponse tokens)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, tokens.UserId),
                new(ClaimTypes.Name, string.IsNullOrWhiteSpace(tokens.Name) ? tokens.UserName : tokens.Name),
                new(ClaimTypes.Email, tokens.Email),
                new(AuthConstants.SessionIdClaim, tokens.SessionId.ToString())
            };

            var roles = tokens.Roles.Count > 0 ? tokens.Roles : new List<string> { tokens.Role };
            claims.AddRange(roles.Where(r => !string.IsNullOrWhiteSpace(r)).Select(r => new Claim(ClaimTypes.Role, r)));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }

        /// <summary>Stores (or replaces) the token values inside the cookie ticket.</summary>
        public static void UpdateTokens(AuthenticationProperties properties, AuthTokenResponse tokens)
        {
            properties.StoreTokens(new[]
            {
                new AuthenticationToken { Name = AuthConstants.AccessToken, Value = tokens.Token },
                new AuthenticationToken { Name = AuthConstants.RefreshToken, Value = tokens.RefreshToken },
                new AuthenticationToken { Name = AuthConstants.ExpiresAt, Value = Format(tokens.ExpiresAtUtc) },
                new AuthenticationToken { Name = AuthConstants.RefreshExpiresAt, Value = Format(tokens.RefreshTokenExpiresAtUtc) }
            });
        }

        public static string? GetSessionId(ClaimsPrincipal user) =>
            user.FindFirstValue(AuthConstants.SessionIdClaim);

        private static DateTimeOffset AsUtc(DateTime value) =>
            new(DateTime.SpecifyKind(value, DateTimeKind.Utc));

        private static string Format(DateTime value) =>
            AsUtc(value).ToString("o", CultureInfo.InvariantCulture);
    }
}
