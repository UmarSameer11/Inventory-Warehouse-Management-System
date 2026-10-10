using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrderDetail;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IPurchaseOrderDetailService
    {
        Task<IEnumerable<PurchaseOrderDetailListViewModel>> GetAllAsync();
        Task<PurchaseOrderDetailUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(PurchaseOrderDetailCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(PurchaseOrderDetailUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(PurchaseOrderDetailCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(PurchaseOrderDetailUpdateViewModel model);
    }
}
