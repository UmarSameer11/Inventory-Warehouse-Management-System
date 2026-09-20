using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.UnitOfMeasure;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly IApiService _apiService;

        public UnitOfMeasureService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(UnitOfMeasureCreateViewModel model)
            => await _apiService.PostAsync<UnitOfMeasureCreateViewModel, ApiResponseViewModel<object>>("/api/UnitOfMeasure", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"/api/UnitOfMeasure/{id}");

        public async Task<IEnumerable<UnitOfMeasureListViewModel?>> GetAllAsync()
        {
            var response = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<UnitOfMeasureListViewModel>>>("/api/UnitOfMeasure");
            return response?.Data ?? Enumerable.Empty<UnitOfMeasureListViewModel>();
        }

        public async Task<UnitOfMeasureUpdateViewModel?> GetByIdAsync(int id)
        {
            var response = await _apiService.GetByIdAsync<ApiResponseViewModel<UnitOfMeasureUpdateViewModel>>("/api/UnitOfMeasure", id);
            return response?.Data;
        }

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(UnitOfMeasureUpdateViewModel model)
            => await _apiService.PutAsync<UnitOfMeasureUpdateViewModel, ApiResponseViewModel<object>>($"/api/UnitOfMeasure/{model.UnitOfMeasureId}", model);
    }
}