using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Employee;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IApiService _apiService;

        public EmployeeService(IApiService apiService)
        {
            _apiService = apiService;
        } 

        public async Task<bool> CreateAsync(EmployeeCreateViewModel model)
        {
            var response =  await _apiService.PostAsync<EmployeeCreateViewModel, ApiResponseViewModel<object>>("/api/Employee", model);
            return response?.Success == true;
        }

        public async Task<IEnumerable<EmployeeListViewModel?>> GetAllAsync()
        {
            var response = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<EmployeeListViewModel>>>("/api/Employee");
                return response?.Data ?? Enumerable.Empty<EmployeeListViewModel>();
        }
    }
}
