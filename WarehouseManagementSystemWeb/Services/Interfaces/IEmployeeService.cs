
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Employee;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListViewModel?>> GetAllAsync();

        Task<EmployeeUpdateViewModel?> GetByIdAsync(int id);

        Task<ApiResponseViewModel<Object>?> CreateAsync(EmployeeCreateViewModel model);

        Task<ApiResponseViewModel<Object>?> UpdateAsync(EmployeeUpdateViewModel model);

        Task<bool> DeleteAsync(int id);

        Task<EmployeeCreateViewModel> GetDeptDesigForDropdownAsync();
    }
}
