using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Designations;
using WarehouseManagementSystemApi.Models.ProductCategory;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;

using WarehouseManagementSystemApi.Models.Customer;
using WarehouseManagementSystemApi.Models.Dispatch;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Models.PurchaseOrder;
using WarehouseManagementSystemApi.Models.ReturnReason;
using WarehouseManagementSystemApi.Models.Salesman;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.Models.SalesReturn;
using WarehouseManagementSystemApi.Models.StockAdjustment;
using WarehouseManagementSystemApi.Models.StockTransfer;
using WarehouseManagementSystemApi.Models.Supplier;
using WarehouseManagementSystemApi.Models.Vehicle;
using WarehouseManagementSystemApi.Models.VehicleAssignment;
using WarehouseManagementSystemApi.Models.VehicleType;
namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IEmployeeRepository Employees { get; }
        IGenericRepository<Departments> Departments { get; }
        IGenericRepository<Designation> Designation { get; }
        IGenericRepository<Product> Product { get; }
        IGenericRepository<ProductCategories> ProductCategories { get; }
        IGenericRepository<UnitOfMeasures> UnitOfMeasures { get; }
        IWarehouseRepository Warehouses { get; }
        IBatchRepository Batches { get; }
        IInventoryStockRepository InventoryStocks { get; }
        IGenericRepository<Customers> Customers { get; }
        IGenericRepository<Dispatches> Dispatches { get; }
        IGenericRepository<GoodsReceipts> GoodsReceipts { get; }
        IGenericRepository<PurchaseOrders> PurchaseOrders { get; }
        IGenericRepository<ReturnReasons> ReturnReasons { get; }
        IGenericRepository<Salesmen> Salesmen { get; }
        IGenericRepository<SalesOrders> SalesOrders { get; }
        IGenericRepository<SalesReturns> SalesReturns { get; }
        IGenericRepository<StockAdjustments> StockAdjustments { get; }
        IGenericRepository<StockTransfers> StockTransfers { get; }
        IGenericRepository<Suppliers> Suppliers { get; }
        IGenericRepository<Vehicles> Vehicles { get; }
        IGenericRepository<VehicleAssignments> VehicleAssignments { get; }
        IGenericRepository<VehicleTypes> VehicleTypes { get; }
        Task<int> SaveChangesAsync();
    }
}
