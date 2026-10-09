using WarehouseManagementSystemApi.Repositories.Implementations;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Implementations;
using WarehouseManagementSystemApi.Services.Interfaces;
using WarehouseManagementSystemApi.Mappings;
using WarehouseManagementSystemApi.Filters;

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
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleAssignmentService, VehicleAssignmentService>();
            services.AddScoped<IStockTransferService, StockTransferService>();
            services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();
            services.AddScoped<ISalesReturnService, SalesReturnService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();
            services.AddScoped<ISalesmanService, SalesmanService>();
            services.AddScoped<IReturnReasonService, ReturnReasonService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IGoodsReceiptService, GoodsReceiptService>();
            services.AddScoped<IDispatchService, DispatchService>();
            services.AddScoped<ICustomerService, CustomerService>();

            // Auth Service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ValidationFilter>();
            services.AddHostedService<ExpiredSessionCleanupService>();


            // AutoMapper
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            return services;
        }
    }
}
