using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.VehicleType;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private const string Endpoint = "/api/VehicleType";

        private readonly IApiService _apiService;

        public VehicleTypeService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<VehicleTypeListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<VehicleTypeListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<VehicleTypeListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no vehicle type records yet.
                return Enumerable.Empty<VehicleTypeListViewModel>();
            }
        }

        public async Task<VehicleTypeUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<VehicleTypeUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(VehicleTypeCreateViewModel model)
            => await _apiService.PostAsync<VehicleTypeCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(VehicleTypeUpdateViewModel model)
            => await _apiService.PutAsync<VehicleTypeUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.VehicleTypeId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.VehicleTypeId)
                .Select(x => new SelectListItem
                {
                    Value = x.VehicleTypeId.ToString(),
                    Text = x.TypeName
                })
                .ToList();
        }
    }
}
