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
        public async Task<IEnumerable<DepartmentViewModel?>> GetAllAsync()
        {
            var department = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<DepartmentViewModel>>>("/api/Department");

            return department?.Data ?? Enumerable.Empty<DepartmentViewModel>();
        }
    }
}
