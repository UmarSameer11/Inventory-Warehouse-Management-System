using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.ReturnReason;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IReturnReasonService
    {
        Task<IEnumerable<ReturnReasonListViewModel>> GetAllAsync();
        Task<ReturnReasonUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(ReturnReasonCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(ReturnReasonUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>ReturnReason options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
