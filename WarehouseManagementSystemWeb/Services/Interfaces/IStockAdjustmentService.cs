using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustment;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IStockAdjustmentService
    {
        Task<IEnumerable<StockAdjustmentListViewModel>> GetAllAsync();
        Task<StockAdjustmentUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(StockAdjustmentCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(StockAdjustmentUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(StockAdjustmentCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(StockAdjustmentUpdateViewModel model);

        /// <summary>StockAdjustment options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
