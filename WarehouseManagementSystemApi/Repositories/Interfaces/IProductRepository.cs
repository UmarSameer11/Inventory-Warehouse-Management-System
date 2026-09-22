using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product?>> GetProductListWithCategoryAndUnitAsync();
        Task<Product?> GetProductWithCategoryAndUnitByIdAsync(int id);
    }
}
