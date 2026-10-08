using WarehouseManagementSystemWeb.Auth;
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
            // Auth plumbing: reads the access token from the auth cookie and attaches it to API calls
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddTransient<BearerTokenHandler>();
            services.AddSingleton<ITokenRefresher, TokenRefresher>();

            // API HttpClient (sends "Authorization: Bearer <access token>")
            services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.BaseAddress = new Uri(
                    configuration["ApiSettings:BaseUrl"]!);
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

            // Login / Logout / Refresh / Sessions / ChangePassword / Registration
            services.AddHttpClient<IAuthApiClient, AuthApiClient>(client =>
            {
                client.BaseAddress = new Uri(
                    configuration["ApiSettings:BaseUrl"]!);
            })
            .AddHttpMessageHandler<BearerTokenHandler>();

            // Application Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IUnitOfMeasureService, UnitOfMeasureService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBatchService, BatchService>();
            services.AddScoped<IInventoryStockService, InventoryStockService>();

            return services;
        }
    }
}