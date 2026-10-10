using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustmentDetail;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IStockAdjustmentDetailService
    {
        Task<IEnumerable<StockAdjustmentDetailListViewModel>> GetAllAsync();
        Task<StockAdjustmentDetailUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(StockAdjustmentDetailCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(StockAdjustmentDetailUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(StockAdjustmentDetailCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(StockAdjustmentDetailUpdateViewModel model);
    }
}
