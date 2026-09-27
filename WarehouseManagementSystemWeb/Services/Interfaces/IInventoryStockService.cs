using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.InventoryStock;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IInventoryStockService
    {
        Task<IEnumerable<InventoryStockListViewModel>> GetAllAsync();
        Task<InventoryStockUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(InventoryStockCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(InventoryStockUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the Warehouse / Product / Batch dropdowns of a Create or Update form.</summary>
        Task PopulateDropdownsAsync(InventoryStockFormViewModel model);
    }
}
