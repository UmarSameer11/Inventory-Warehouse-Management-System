using System.Text.Json;
using WarehouseManagementSystemApi.Common;

namespace WarehouseManagementSystemApi.MiddleWares
{
    public class ResponseWrappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalResponseBody = context.Response.Body;

            var memoryStream = new MemoryStream();

            try
            {
                context.Response.Body = memoryStream;

                await _next(context);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                if (context.Response.StatusCode >= 200 &&
                    context.Response.StatusCode < 300)
                {
                    var response = new ResponseWrappingMiddlwareModel<object>
                    {
                        Success = true,
                        Message = "Request completed successfully",
                        Data = JsonSerializer.Deserialize<object>(responseBody),
                        StatusCode = context.Response.StatusCode,
                        TraceId = context.TraceIdentifier,
                        Timestamp = DateTime.UtcNow
                    };

                    var json = JsonSerializer.Serialize(response);

                    context.Response.Body = originalResponseBody;

                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(json);
                }
                else
                {
                    context.Response.Body = originalResponseBody;

                    await context.Response.WriteAsync(responseBody);
                }
            }
            finally
            {
                context.Response.Body = originalResponseBody;

                await memoryStream.DisposeAsync();
            }
        }
    }
}