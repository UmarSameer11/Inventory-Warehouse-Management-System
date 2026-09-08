using System.Diagnostics;
using WarehouseManagementSystemApi.Models.ApiPerformance;
using WarehouseManagementSystemApi.Services.Interfaces;
using static WarehouseManagementSystemApi.Models.ApiPerformance.ApiPerformanceLog;

namespace WarehouseManagementSystemApi.MiddleWares
{
    public class PerformanceMonitoringMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<PerformanceMonitoringMiddleware> _logger;


        public PerformanceMonitoringMiddleware(
            RequestDelegate next)
        //ILogger<PerformanceMonitoringMiddleware> logger)
        {
            _next = next;
            //_logger = logger;
        }


        public async Task InvokeAsync(HttpContext context, IApiPerformanceService apiPerformanceService)
        {

            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            var performanceService = new ApiPerformanceLog
            {
                HttpMethod = context.Request.Method,
                Endpoint = context.Request.Path,
                ExecutionTime = stopwatch.ElapsedMilliseconds,
                StatusCode = context.Response.StatusCode,
                UserId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                UserName = context.User.Identity?.Name,
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                CorrelationId = context.Request.Headers["Correlation-ID"].ToString(),
                CreatedDate = DateTime.UtcNow
            };

            await apiPerformanceService.ApiPerformanceAddAsync(performanceService);


        }
    }
}
