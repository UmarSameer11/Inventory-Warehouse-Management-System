using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesReturn;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface ISalesReturnService
    {
        Task<IEnumerable<SalesReturnListViewModel>> GetAllAsync();
        Task<SalesReturnUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(SalesReturnCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(SalesReturnUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(SalesReturnCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(SalesReturnUpdateViewModel model);

        /// <summary>SalesReturn options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
