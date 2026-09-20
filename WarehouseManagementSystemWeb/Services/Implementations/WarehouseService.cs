using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Warehouse;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IApiService _apiService;
        private readonly IEmployeeService _employee;

        public WarehouseService(IApiService apiService, IEmployeeService employee)
        {
            _apiService = apiService;
            _employee = employee;
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(WarehouseCreateViewModel model)
            => await _apiService.PostAsync<WarehouseCreateViewModel, ApiResponseViewModel<object>>("/api/Warehouse", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"/api/Warehouse/{id}");

        public async Task<IEnumerable<WarehouseListViewModel?>> GetAllAsync()
        {
            var response = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<WarehouseListViewModel>>>("/api/Warehouse");
            return response?.Data ?? Enumerable.Empty<WarehouseListViewModel>();
        }

        public async Task<WarehouseUpdateViewModel?> GetByIdAsync(int id)
        {
            var response = await _apiService.GetByIdAsync<ApiResponseViewModel<WarehouseUpdateViewModel>>("/api/Warehouse", id);
            return response?.Data;
        }

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(WarehouseUpdateViewModel model)
            => await _apiService.PutAsync<WarehouseUpdateViewModel, ApiResponseViewModel<object>>($"/api/Warehouse/{model.WarehouseId}", model);

        public async Task<List<SelectListItem>> GetEmployeeDropdownAsync()
        {
            var employees = await _employee.GetAllAsync();

            return employees
                .Where(e => e != null && e.DesignationName == "Asst.Manager")
                .Select(e => new SelectListItem
                {
                    Value = e!.EmployeeId.ToString(),
                    Text = $"{e.EmployeeCode} - {e.FirstName} {e.LastName}"
                })
                .ToList();
        }
    }
}