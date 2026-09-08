using WarehouseManagementSystemApi.DTOs.ProductCategory;
using WarehouseManagementSystemApi.DTOs.UnitOfMeasure;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryListDto>> GetProductCategoryListAsync();
        Task<ProductCategoryListDto?> GetProductCategoryByIdAsync(int id);
        Task CreateProductCategoryAsync(ProductCategoryCreateDto dto);
        Task UpdateProductCategoryAsync(int id, ProductCategoryUpdateDto dto);
        Task DeleteProductCategoryAsync(int id);
    }
}
