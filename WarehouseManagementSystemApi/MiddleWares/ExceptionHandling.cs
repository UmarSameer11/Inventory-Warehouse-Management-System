using WarehouseManagementSystemApi.Models.ErrorResponse;

namespace WarehouseManagementSystemApi.MiddleWares
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandling> _logger;

        public ExceptionHandling(
            RequestDelegate next,
            ILogger<ExceptionHandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandle(context, ex);
            }
        }

        private async Task ExceptionHandle(
            HttpContext context,
            Exception ex)
        {
            int statusCode = ex switch
            {
                ArgumentException =>
                    StatusCodes.Status400BadRequest,

                UnauthorizedAccessException =>
                    StatusCodes.Status401Unauthorized,

                KeyNotFoundException =>
                    StatusCodes.Status404NotFound,

                InvalidOperationException =>
                    StatusCodes.Status409Conflict,

                _ =>
                    StatusCodes.Status500InternalServerError
            };

            _logger.LogError(
                ex,
                "Unhandled exception occurred. Method: {Method}, Path: {Path}, StatusCode: {StatusCode}",
                context.Request.Method,
                context.Request.Path,
                statusCode);

            // Response already started
            if (context.Response.HasStarted)
            {
                _logger.LogWarning(
                    "The response has already started. Exception response cannot be written.");

                return;
            }

            context.Response.Clear();

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                StatusCode = statusCode,

                Message = statusCode ==
                          StatusCodes.Status500InternalServerError
                    ? "Internal Server Error"
                    : ex.Message,

                Path = context.Request.Path,
                Detail = ex.InnerException?.Message ?? ex.Message,
                Time = DateTime.UtcNow,
                CorrelationId = context.TraceIdentifier
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}