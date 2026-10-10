using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IGoodsReceiptService
    {
        Task<IEnumerable<GoodsReceiptListViewModel>> GetAllAsync();
        Task<GoodsReceiptUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(GoodsReceiptCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(GoodsReceiptUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(GoodsReceiptCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(GoodsReceiptUpdateViewModel model);

        /// <summary>GoodsReceipt options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
