using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Designations;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Models.ProductCategory;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;
using WarehouseManagementSystemApi.Models.Warehouse;
using WarehouseManagementSystemApi.Repositories.Interfaces;

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
        }
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
