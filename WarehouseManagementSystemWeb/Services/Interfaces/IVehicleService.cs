using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Vehicle;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleListViewModel>> GetAllAsync();
        Task<VehicleUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(VehicleCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(VehicleUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(VehicleCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(VehicleUpdateViewModel model);

        /// <summary>Vehicle options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
