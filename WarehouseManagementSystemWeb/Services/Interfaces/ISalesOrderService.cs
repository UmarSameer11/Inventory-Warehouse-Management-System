using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesOrder;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface ISalesOrderService
    {
        Task<IEnumerable<SalesOrderListViewModel>> GetAllAsync();
        Task<SalesOrderUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(SalesOrderCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(SalesOrderUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(SalesOrderCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(SalesOrderUpdateViewModel model);

        /// <summary>SalesOrder options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
