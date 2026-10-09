using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Designations;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Models.ProductCategory;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;
using WarehouseManagementSystemApi.Models.Warehouse;
using WarehouseManagementSystemApi.Repositories.Interfaces;

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
namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepository Employees { get; }
        public IGenericRepository<Departments> Departments {  get; }
        public IGenericRepository<Designation> Designation { get; }
        public IGenericRepository<Product> Product { get; }
        public IGenericRepository<ProductCategories> ProductCategories { get; }
        public IGenericRepository<UnitOfMeasures> UnitOfMeasures { get; }
        public IWarehouseRepository Warehouses { get; }
        public IBatchRepository Batches { get; }
        public IInventoryStockRepository InventoryStocks { get; }
        public IGenericRepository<VehicleTypes> VehicleTypes { get; }
        public IGenericRepository<VehicleAssignments> VehicleAssignments { get; }
        public IGenericRepository<Vehicles> Vehicles { get; }
        public IGenericRepository<Suppliers> Suppliers { get; }
        public IGenericRepository<StockTransfers> StockTransfers { get; }
        public IGenericRepository<StockAdjustments> StockAdjustments { get; }
        public IGenericRepository<SalesReturns> SalesReturns { get; }
        public IGenericRepository<SalesOrders> SalesOrders { get; }
        public IGenericRepository<Salesmen> Salesmen { get; }
        public IGenericRepository<ReturnReasons> ReturnReasons { get; }
        public IGenericRepository<PurchaseOrders> PurchaseOrders { get; }
        public IGenericRepository<GoodsReceipts> GoodsReceipts { get; }
        public IGenericRepository<Dispatches> Dispatches { get; }
        public IGenericRepository<Customers> Customers { get; }

      

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(context);
            Departments = new GenericRepository<Departments>(context);
            Designation = new GenericRepository<Designation>(context);
            Product = new GenericRepository<Product>(context);
            ProductCategories = new GenericRepository<ProductCategories>(context);
            UnitOfMeasures = new GenericRepository<UnitOfMeasures>(context);
            Warehouses = new WarehouseRepository(context);
            Batches = new BatchRepository(context);
            InventoryStocks = new InventoryStockRepository(context);
            VehicleTypes = new GenericRepository<VehicleTypes>(context);
            VehicleAssignments = new GenericRepository<VehicleAssignments>(context);
            Vehicles = new GenericRepository<Vehicles>(context);
            Suppliers = new GenericRepository<Suppliers>(context);
            StockTransfers = new GenericRepository<StockTransfers>(context);
            StockAdjustments = new GenericRepository<StockAdjustments>(context);
            SalesReturns = new GenericRepository<SalesReturns>(context);
            SalesOrders = new GenericRepository<SalesOrders>(context);
            Salesmen = new GenericRepository<Salesmen>(context);
            ReturnReasons = new GenericRepository<ReturnReasons>(context);
            PurchaseOrders = new GenericRepository<PurchaseOrders>(context);
            GoodsReceipts = new GenericRepository<GoodsReceipts>(context);
            Dispatches = new GenericRepository<Dispatches>(context);
            Customers = new GenericRepository<Customers>(context);
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
