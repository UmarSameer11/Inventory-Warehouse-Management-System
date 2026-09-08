using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Designations;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Models.ProductCategory;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;
using WarehouseManagementSystemApi.Models.Warehouse;

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
        Task<int> SaveChangesAsync();
    }
}
