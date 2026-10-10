using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrderListViewModel>> GetAllAsync();
        Task<PurchaseOrderUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(PurchaseOrderCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(PurchaseOrderUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(PurchaseOrderCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(PurchaseOrderUpdateViewModel model);

        /// <summary>PurchaseOrder options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
