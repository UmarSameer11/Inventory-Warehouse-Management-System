using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockTransferDetail;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IStockTransferDetailService
    {
        Task<IEnumerable<StockTransferDetailListViewModel>> GetAllAsync();
        Task<StockTransferDetailUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(StockTransferDetailCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(StockTransferDetailUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(StockTransferDetailCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(StockTransferDetailUpdateViewModel model);
    }
}
