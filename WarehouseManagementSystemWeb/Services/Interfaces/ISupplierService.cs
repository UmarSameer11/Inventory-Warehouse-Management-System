using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Supplier;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierListViewModel>> GetAllAsync();
        Task<SupplierUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(SupplierCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(SupplierUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Supplier options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
