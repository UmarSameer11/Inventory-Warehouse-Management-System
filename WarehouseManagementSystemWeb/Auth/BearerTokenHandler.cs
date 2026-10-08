using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Net.Http.Headers;

namespace WarehouseManagementSystemWeb.Auth
{
    /// <summary>
    /// Added to every HttpClient that calls the API. It
    ///  1. attaches the logged-in user's access token (kept in the auth cookie) as "Authorization: Bearer ...",
    ///  2. forwards the browser's IP and User-Agent, so the API's sessions / rate limit / audit show the real
    ///     user and not just this web server,
    ///  3. if the API answers 401 (session revoked, token invalid) clears the cookie so the user is sent to login.
    /// Requests marked with <see cref="Anonymous"/> (login / refresh) get no Bearer header.
    /// </summary>
    public class BearerTokenHandler : DelegatingHandler
    {
        public static readonly HttpRequestOptionsKey<bool> Anonymous = new("wms.anonymous");

        private readonly IHttpContextAccessor _accessor;

        public BearerTokenHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var http = _accessor.HttpContext;
            var isAnonymous = request.Options.TryGetValue(Anonymous, out var flag) && flag;

            if (http != null)
            {
                var ip = http.Connection.RemoteIpAddress?.ToString();
                if (!string.IsNullOrEmpty(ip))
                {
                    request.Headers.Remove("X-Forwarded-For");
                    request.Headers.TryAddWithoutValidation("X-Forwarded-For", ip);
                }

                var userAgent = http.Request.Headers.UserAgent.ToString();
                if (!string.IsNullOrEmpty(userAgent))
                {
                    request.Headers.UserAgent.Clear();
                    request.Headers.TryAddWithoutValidation("User-Agent", userAgent);
                }

                if (!isAnonymous)
                {
                    var token = await http.GetTokenAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, AuthConstants.AccessToken);

                    if (!string.IsNullOrEmpty(token))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (!isAnonymous
                && response.StatusCode == HttpStatusCode.Unauthorized
                && http != null
                && !http.Response.HasStarted)
            {
                // The API no longer accepts this login. Drop the cookie; the caller still gets the 401.
                await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return response;
        }
    }
}
