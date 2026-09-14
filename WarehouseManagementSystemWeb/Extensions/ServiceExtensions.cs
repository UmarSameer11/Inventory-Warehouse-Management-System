using WarehouseManagementSystemWeb.Services.Implementations;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            // API HttpClient
            services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.BaseAddress = new Uri(
                    configuration["ApiSettings:BaseUrl"]!);
            });

            // Application Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService, DesignationService>();

            return services;
        }
    }
}