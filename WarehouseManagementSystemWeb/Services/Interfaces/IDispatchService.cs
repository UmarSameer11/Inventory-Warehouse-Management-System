using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Dispatch;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IDispatchService
    {
        Task<IEnumerable<DispatchListViewModel>> GetAllAsync();
        Task<DispatchUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(DispatchCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(DispatchUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(DispatchCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(DispatchUpdateViewModel model);

        /// <summary>Dispatch options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
