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

        public async Task<ApiResponseViewModel<object>?> CreateAsync(DesignationCreateViewModel model)
        {
            var response = await _apiService.PostAsync<DesignationCreateViewModel, ApiResponseViewModel<Object>>("/api/Designation", model);

            return response;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _apiService.DeleteAsync($"/api/Designation/{id}");
        }

        public async Task<IEnumerable<DesignationListViewModel?>> GetAllAsync()
        {
            var designation = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<DesignationListViewModel>>>("/api/Designation");

            return designation?.Data ?? Enumerable.Empty<DesignationListViewModel>();
        }

        public async Task<DesignationUpdateViewModel?> GetByIdAsync(int id)
        {
            var designation = await _apiService.GetByIdAsync<ApiResponseViewModel<DesignationUpdateViewModel>>("/api/Designation", id);

            return designation?.Data;
        }

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(DesignationUpdateViewModel model)
        {
            var designation = await _apiService.PutAsync<DesignationUpdateViewModel, ApiResponseViewModel<Object>>($"/api/Designation/{model.DesignationId}", model);

            return designation;
        }
    }
}
