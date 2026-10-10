using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.DispatchDetail;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IDispatchDetailService
    {
        Task<IEnumerable<DispatchDetailListViewModel>> GetAllAsync();
        Task<DispatchDetailUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(DispatchDetailCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(DispatchDetailUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(DispatchDetailCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(DispatchDetailUpdateViewModel model);
    }
}
