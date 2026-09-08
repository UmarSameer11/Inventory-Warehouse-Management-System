namespace WarehouseManagementSystemApi.MiddleWares
{
    public class CorrelationMiddleware
    {
        private const string CorrelationHeader = "Correlation-ID";

        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers
                .TryGetValue(CorrelationHeader, out var headerValue)
                && !string.IsNullOrWhiteSpace(headerValue)
                    ? headerValue.ToString()
                    : Guid.NewGuid().ToString();

            context.Request.Headers[CorrelationHeader] = correlationId;

            context.TraceIdentifier = correlationId;

            context.Response.Headers[CorrelationHeader] = correlationId;

            await _next(context);
        }
    }
}
