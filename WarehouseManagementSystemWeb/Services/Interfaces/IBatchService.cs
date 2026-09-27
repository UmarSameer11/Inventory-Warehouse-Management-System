using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Batch;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IBatchService
    {
        Task<IEnumerable<BatchListViewModel>> GetAllAsync();
        Task<BatchUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(BatchCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(BatchUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Active products for the dropdown (plus the currently selected one, if any).</summary>
        Task<List<SelectListItem>> GetProductDropdownAsync(int? includeProductId = null);
    }
}
