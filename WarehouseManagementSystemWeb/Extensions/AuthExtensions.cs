using Microsoft.AspNetCore.Authentication.Cookies;
using WarehouseManagementSystemWeb.Auth;

namespace WarehouseManagementSystemWeb.Extensions
{
    public static class AuthExtensions
    {
        /// <summary>Cookie authentication for the browser; the API's JWTs are kept inside the cookie.</summary>
        public static IServiceCollection AddWebAuthentication(this IServiceCollection services, IHostEnvironment environment)
        {
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";

                    options.Cookie.Name = "WMS.Auth";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    // Plain http://localhost works in Development; always HTTPS-only elsewhere.
                    options.Cookie.SecurePolicy = environment.IsDevelopment()
                        ? CookieSecurePolicy.SameAsRequest
                        : CookieSecurePolicy.Always;

                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.SlidingExpiration = true;

                    options.Events.OnValidatePrincipal = CookieTokenEvents.ValidatePrincipalAsync;
                });

            return services;
        }
    }
}
