using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Employee;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListViewModel?>> GetAllAsync();

        //Task<EmployeeUpdateViewModel?> GetByIdAsync(int id);

        Task<bool> CreateAsync(EmployeeCreateViewModel model);

        //Task<bool> UpdateAsync(EmployeeUpdateViewModel model); 

        //Task<bool> DeleteAsync(int id);

        Task<EmployeeCreateViewModel> GetDeptDesigForDropdownAsync();
    }
}
