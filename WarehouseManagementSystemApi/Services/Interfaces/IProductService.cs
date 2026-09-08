using WarehouseManagementSystemApi.DTOs.Product;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDto>> GetProductListAsync();
        Task<ProductListDto?> GetProductByIdAsync(int id);
        Task CreateProductAsync(ProductCreateDto dto);
        Task UpdateProductAsync(int id, ProductUpdateDto dto);
        Task DeleteProductAsync(int id);
    }
}
