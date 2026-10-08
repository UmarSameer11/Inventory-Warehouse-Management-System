using System.Security.Claims;
using WarehouseManagementSystemApi.Common.Constant;

namespace WarehouseManagementSystemApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal principal) =>
            principal.FindFirstValue(ClaimTypes.NameIdentifier);

        public static Guid? GetSessionId(this ClaimsPrincipal principal) =>
            Guid.TryParse(principal.FindFirstValue(AuthClaims.SessionId), out var id) ? id : null;
    }
}
