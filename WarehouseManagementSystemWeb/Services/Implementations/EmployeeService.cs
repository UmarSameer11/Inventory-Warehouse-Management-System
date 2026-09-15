using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Department;
using WarehouseManagementSystemWeb.Application.ViewModels.Employee;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IApiService _apiService;
        private readonly IDepartmentService _department;
        private readonly IDesignationService _designation;

        public EmployeeService(IApiService apiService, IDepartmentService department, IDesignationService designation)
        {
            _apiService = apiService;
            _department = department;
            _designation = designation;
        } 

        public async Task<ApiResponseViewModel<Object>?> CreateAsync(EmployeeCreateViewModel model)
        {
            var response =  await _apiService.PostAsync<EmployeeCreateViewModel, ApiResponseViewModel<object>>("/api/Employee", model);
            return response;
        }

        public async Task<IEnumerable<EmployeeListViewModel?>> GetAllAsync()
        {
            var response = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<EmployeeListViewModel>>>("/api/Employee");
                return response?.Data ?? Enumerable.Empty<EmployeeListViewModel>();
        }

        public async Task<EmployeeUpdateViewModel?> GetByIdAsync(int id)
        {
            var response = await _apiService.GetByIdAsync<ApiResponseViewModel<EmployeeUpdateViewModel>>("/api/Employee", id);

            return response?.Data;
        }

        ///// Employee list with department and designation name
        public async Task<EmployeeCreateViewModel> GetDeptDesigForDropdownAsync()
        {
            var department = await _department.GetAllAsync();
            var designation = await _designation.GetAllAsync();

            var result = new EmployeeCreateViewModel
            {
                Departments = department.Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.DepartmentName
                }).ToList(),

                Designations = designation.Select(s => new SelectListItem
                {
                    Value = s.DesignationId.ToString(),
                    Text = s.DesignationName
                }).ToList(),
            };
            return result;
        }

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(EmployeeUpdateViewModel model)
        {
            var response = await _apiService.PutAsync<EmployeeUpdateViewModel, ApiResponseViewModel<Object>>("/api/Employee/{model.EmployeeId}", model);
            return response;
        }
    }
}
