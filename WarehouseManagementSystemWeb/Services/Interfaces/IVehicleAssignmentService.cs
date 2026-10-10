using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.VehicleAssignment;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IVehicleAssignmentService
    {
        Task<IEnumerable<VehicleAssignmentListViewModel>> GetAllAsync();
        Task<VehicleAssignmentUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(VehicleAssignmentCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(VehicleAssignmentUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(VehicleAssignmentCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(VehicleAssignmentUpdateViewModel model);
    }
}
