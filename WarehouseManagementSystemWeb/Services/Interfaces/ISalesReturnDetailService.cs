using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesReturnDetail;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface ISalesReturnDetailService
    {
        Task<IEnumerable<SalesReturnDetailListViewModel>> GetAllAsync();
        Task<SalesReturnDetailUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(SalesReturnDetailCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(SalesReturnDetailUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(SalesReturnDetailCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(SalesReturnDetailUpdateViewModel model);
    }
}
