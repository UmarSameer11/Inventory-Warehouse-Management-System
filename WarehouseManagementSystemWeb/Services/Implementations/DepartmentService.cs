using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Department;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IApiService _apiService;

        public DepartmentService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(DepartmentCreateViewModel model)
        {
            var response = await _apiService.PostAsync<DepartmentCreateViewModel, ApiResponseViewModel<Object>>("/api/Department", model);

            return response;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _apiService.DeleteAsync($"/api/Department/{id}");
        }

        public async Task<IEnumerable<DepartmentListViewModel?>> GetAllAsync()
        {
            var department = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<DepartmentListViewModel>>>("/api/Department");

            return department?.Data ?? Enumerable.Empty<DepartmentListViewModel>();
        }

        public async Task<DepartmentUpdateViewModel?> GetByIdAsync(int id)
        {
            var department = await _apiService.GetByIdAsync<ApiResponseViewModel<DepartmentUpdateViewModel>>("/api/Department", id);

            return department?.Data;
        }

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(DepartmentUpdateViewModel model)
        {
            var department = await _apiService.PutAsync<DepartmentUpdateViewModel, ApiResponseViewModel<Object>>($"/api/Department/{model.DepartmentId}", model);

            return department;
        }
    }
}
