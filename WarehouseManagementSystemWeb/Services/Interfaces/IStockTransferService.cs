using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockTransfer;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IStockTransferService
    {
        Task<IEnumerable<StockTransferListViewModel>> GetAllAsync();
        Task<StockTransferUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(StockTransferCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(StockTransferUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(StockTransferCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(StockTransferUpdateViewModel model);

        /// <summary>StockTransfer options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
