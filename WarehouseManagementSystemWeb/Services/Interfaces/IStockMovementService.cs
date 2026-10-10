using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockMovement;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IStockMovementService
    {
        Task<IEnumerable<StockMovementListViewModel>> GetAllAsync();
        Task<StockMovementUpdateViewModel?> GetByIdAsync(long id);
        Task<ApiResponseViewModel<object>?> CreateAsync(StockMovementCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(StockMovementUpdateViewModel model);
        Task<bool> DeleteAsync(long id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(StockMovementCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(StockMovementUpdateViewModel model);
    }
}
