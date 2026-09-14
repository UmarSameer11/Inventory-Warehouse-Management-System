using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Designation;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class DesignationService : IDesignationService
    {
        private readonly IApiService _apiService;

        public DesignationService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<DesignationViewModel?>> GetAllAsync()
        {
            var designation = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<DesignationViewModel>>>("/api/Designation");

            return designation?.Data ?? Enumerable.Empty<DesignationViewModel>();
        }
    }
}
