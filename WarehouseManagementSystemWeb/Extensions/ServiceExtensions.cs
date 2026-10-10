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
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IReturnReasonService, ReturnReasonService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();
            services.AddScoped<ISalesmanService, SalesmanService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleAssignmentService, VehicleAssignmentService>();
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IPurchaseOrderDetailService, PurchaseOrderDetailService>();
            services.AddScoped<IGoodsReceiptService, GoodsReceiptService>();
            services.AddScoped<IGoodsReceiptDetailService, GoodsReceiptDetailService>();
            services.AddScoped<ISalesOrderService, SalesOrderService>();
            services.AddScoped<ISalesOrderDetailService, SalesOrderDetailService>();
            services.AddScoped<ISalesReturnService, SalesReturnService>();
            services.AddScoped<ISalesReturnDetailService, SalesReturnDetailService>();
            services.AddScoped<IDispatchService, DispatchService>();
            services.AddScoped<IDispatchDetailService, DispatchDetailService>();
            services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();
            services.AddScoped<IStockAdjustmentDetailService, StockAdjustmentDetailService>();
            services.AddScoped<IStockMovementService, StockMovementService>();
            services.AddScoped<IStockTransferService, StockTransferService>();
            services.AddScoped<IStockTransferDetailService, StockTransferDetailService>();
            return services;
        }
    }
}