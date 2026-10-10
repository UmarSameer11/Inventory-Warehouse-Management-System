using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceiptDetail;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IGoodsReceiptDetailService
    {
        Task<IEnumerable<GoodsReceiptDetailListViewModel>> GetAllAsync();
        Task<GoodsReceiptDetailUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(GoodsReceiptDetailCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(GoodsReceiptDetailUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(GoodsReceiptDetailCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(GoodsReceiptDetailUpdateViewModel model);
    }
}
