using System.Diagnostics;

namespace WarehouseManagementSystemApi.MiddleWares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var method = context.Request.Method;
            var path = context.Request.Path;

            var user = context.User.Identity?.Name ?? "Anonymous";

            var ipAddress =
                context.Connection.RemoteIpAddress?.ToString()
                ?? "Unknown";

            var correlationId =
                context.Request.Headers["Correlation-ID"].ToString();

            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = context.TraceIdentifier;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                _logger.LogInformation(
                    "HTTP {Method} {Path} responded {StatusCode} in {Duration} ms | User: {User} | IP: {IPAddress} | CorrelationId: {CorrelationId}",
                    method,
                    path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds,
                    user,
                    ipAddress,
                    correlationId);
            }
        }
    }
}

