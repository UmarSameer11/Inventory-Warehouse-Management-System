

using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Department;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentListViewModel?>> GetAllAsync();
        Task<DepartmentUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<Object>?> CreateAsync(DepartmentCreateViewModel model);
        Task<ApiResponseViewModel<Object>?> UpdateAsync(DepartmentUpdateViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}
