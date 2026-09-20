

using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Designation;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationListViewModel?>> GetAllAsync();
        Task<DesignationUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<Object>?> CreateAsync(DesignationCreateViewModel model);
        Task<ApiResponseViewModel<Object>?> UpdateAsync(DesignationUpdateViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
