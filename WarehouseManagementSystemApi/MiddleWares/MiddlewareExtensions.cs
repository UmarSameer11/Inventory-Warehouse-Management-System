namespace WarehouseManagementSystemApi.MiddleWares
{ 
    public static class MiddlewareExtensions
        {
         public static IApplicationBuilder UseGlobelExceptionHandling(this IApplicationBuilder application)
         {
            return application.UseMiddleware<ExceptionHandling>();
         }

        public static IApplicationBuilder RequestLogging(this IApplicationBuilder application)
        {
            return application.UseMiddleware<RequestLoggingMiddleware>();
        }

        public static IApplicationBuilder ResponseWrapping(this IApplicationBuilder application)
        {
            return application.UseMiddleware<ResponseWrappingMiddleware>();
        }

        public static IApplicationBuilder CorrelationMiddleWare(this IApplicationBuilder application)
        {
            return application.UseMiddleware<CorrelationMiddleware>();
        }

        public static IApplicationBuilder ApiPerformanceMiddlware(this IApplicationBuilder application)
        {
            return application.UseMiddleware<PerformanceMonitoringMiddleware>();
        }

    }
    }

