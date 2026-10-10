using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.VehicleType;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IVehicleTypeService
    {
        Task<IEnumerable<VehicleTypeListViewModel>> GetAllAsync();
        Task<VehicleTypeUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(VehicleTypeCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(VehicleTypeUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>VehicleType options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
