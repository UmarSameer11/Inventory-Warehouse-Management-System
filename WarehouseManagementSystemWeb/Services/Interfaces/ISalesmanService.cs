using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Salesman;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface ISalesmanService
    {
        Task<IEnumerable<SalesmanListViewModel>> GetAllAsync();
        Task<SalesmanUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(SalesmanCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(SalesmanUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(SalesmanCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(SalesmanUpdateViewModel model);

        /// <summary>Salesman options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
