using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using WarehouseManagementSystemApi.Common.Settings;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.Auth;
using WarehouseManagementSystemApi.Models.ErrorResponse;

namespace WarehouseManagementSystemApi.Extensions
{
    public static class AuthServiceExtensions
    {
        /// <summary>ASP.NET Core Identity: password policy, lockout and unique e-mail.</summary>
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    // Same rules as ChangePasswordValidator / RegistrationValidator.
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;

                    // 5 wrong passwords -> locked for 15 minutes.
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>JWT bearer authentication + session check + "everything needs login" fallback policy.</summary>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtSettings>()
                .Bind(configuration.GetSection(JwtSettings.SectionName))
                .Validate(s => !string.IsNullOrWhiteSpace(s.SecretKey) && s.SecretKey.Length >= 32,
                    "JwtSettings:SecretKey must be at least 32 characters.")
                .Validate(s => !string.IsNullOrWhiteSpace(s.Issuer) && !string.IsNullOrWhiteSpace(s.Audience),
                    "JwtSettings:Issuer and JwtSettings:Audience are required.")
                .Validate(s => s.AccessTokenExpiryMinutes > 0 && s.RefreshTokenExpiryDays > 0 && s.SessionExpiryDays > 0,
                    "JwtSettings expiry values must be greater than zero.")
                .ValidateOnStart();

            var jwt = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? new JwtSettings();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    // Keep the claim names exactly as written into the token.
                    options.MapInboundClaims = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey)),
                        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                        RoleClaimType = ClaimTypes.Role,
                        NameClaimType = ClaimTypes.Name,
                        ClockSkew = TimeSpan.FromSeconds(30)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        // A valid signature is not enough: the session behind the token must still be active.
                        // This is what makes Logout / "log out all devices" / password change / deactivation
                        // take effect immediately instead of after the access token expires.
                        OnTokenValidated = async context =>
                        {
                            var userId = context.Principal?.GetUserId();
                            var sessionId = context.Principal?.GetSessionId();

                            if (string.IsNullOrEmpty(userId) || sessionId is null)
                            {
                                context.Fail("Token is not bound to a session.");
                                return;
                            }

                            var db = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
                            var now = DateTime.UtcNow;

                            var sessionActive = await db.UserSessions
                                .AsNoTracking()
                                .AnyAsync(s => s.Id == sessionId
                                               && s.UserId == userId
                                               && s.RevokedAtUtc == null
                                               && s.ExpiresAtUtc > now,
                                    context.HttpContext.RequestAborted);

                            if (!sessionActive)
                            {
                                context.Fail("Session is no longer active.");
                            }
                        },

                        // Lets the front-end know it should call /api/Account/RefreshToken.
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception is SecurityTokenExpiredException)
                            {
                                context.Response.Headers["Token-Expired"] = "true";
                            }

                            return Task.CompletedTask;
                        },

                        // 401 / 403 as JSON (same shape as the rest of the API errors) instead of an empty body.
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            if (context.Response.HasStarted)
                            {
                                return;
                            }

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsJsonAsync(new ErrorResponse
                            {
                                StatusCode = StatusCodes.Status401Unauthorized,
                                Message = "You are not authenticated. Please log in.",
                                Detail = context.AuthenticateFailure is SecurityTokenExpiredException
                                    ? "Access token expired."
                                    : null,
                                Path = context.Request.Path,
                                Time = DateTime.UtcNow,
                                CorrelationId = context.HttpContext.TraceIdentifier
                            });
                        },

                        OnForbidden = async context =>
                        {
                            if (context.Response.HasStarted)
                            {
                                return;
                            }

                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            await context.Response.WriteAsJsonAsync(new ErrorResponse
                            {
                                StatusCode = StatusCodes.Status403Forbidden,
                                Message = "You do not have permission to perform this action.",
                                Path = context.Request.Path,
                                Time = DateTime.UtcNow,
                                CorrelationId = context.HttpContext.TraceIdentifier
                            });
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                // Every endpoint requires a logged-in user unless it is marked [AllowAnonymous].
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            return services;
        }

        /// <summary>Per-IP rate limit for Login / RefreshToken (policy name "LoginPolicy").</summary>
        public static IServiceCollection AddAuthRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            var permitPerMinute = configuration.GetValue("RateLimiting:AuthRequestsPerMinute", 10);

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy("LoginPolicy", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = permitPerMinute,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0,
                            AutoReplenishment = true
                        }));

                options.OnRejected = async (context, cancellationToken) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter =
                            ((int)retryAfter.TotalSeconds).ToString();
                    }

                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsJsonAsync(new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status429TooManyRequests,
                        Message = "Too many requests. Please wait a moment and try again.",
                        Path = context.HttpContext.Request.Path,
                        Time = DateTime.UtcNow,
                        CorrelationId = context.HttpContext.TraceIdentifier
                    }, cancellationToken);
                };
            });

            return services;
        }
    }
}
