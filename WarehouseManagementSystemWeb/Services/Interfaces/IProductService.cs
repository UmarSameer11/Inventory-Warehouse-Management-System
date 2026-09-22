using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Product;
using WarehouseManagementSystemWeb.Application.ViewModels.Warehouse;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListViewModel?>> GetAllAsync();
        Task<ProductUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(ProductCreateViewModel model);
        Task<ProductCreateViewModel> DropdownProductWithUnitAndCategoryAsync();
        Task<ApiResponseViewModel<object>?> UpdateAsync(ProductUpdateViewModel model);
        //Task<bool> DeleteAsync(int id);
    }
}
