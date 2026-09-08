using System.Security.Claims;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CorrelationHeader = "Correlation-ID";
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

        public string? UserId => HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserName => HttpContext?.User?.Identity?.Name;

        public string? Role => HttpContext?.User?.FindFirstValue(ClaimTypes.Role);

        public string? Method => HttpContext?.Request.Method;

        public string? Path => HttpContext?.Request.Path;

        public string? IPAddress => HttpContext?.Connection.RemoteIpAddress?.ToString();

        public string? UserAgent => HttpContext?.Request.Headers["User-Agent"].ToString();

        public string? TraceId => HttpContext?.TraceIdentifier;

        public string? CorrelationId => HttpContext?.Request.Headers[CorrelationHeader].ToString();
    }
}

