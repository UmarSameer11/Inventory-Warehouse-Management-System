using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.ProductCategory;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryListViewModel?>> GetAllAsync();
        Task<ProductCategoryUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(ProductCategoryCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(ProductCategoryUpdateViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}