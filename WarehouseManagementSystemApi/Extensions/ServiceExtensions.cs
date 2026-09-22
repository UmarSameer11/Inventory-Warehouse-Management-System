using WarehouseManagementSystemApi.Repositories.Implementations;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Implementations;
using WarehouseManagementSystemApi.Services.Interfaces;
using WarehouseManagementSystemApi.Mappings;

namespace WarehouseManagementSystemApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IApiPerformanceRepository, ApiPerformanceRepository>();

            // Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfMeasureService, UnitOfMeasureService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IBatchService, BatchService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IInventoryStockService, InventoryStockService>();
            services.AddScoped<IApiPerformanceService, ApiPerformanceService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Auth Service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();


            // AutoMapper
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            return services;
        }
    }
}
