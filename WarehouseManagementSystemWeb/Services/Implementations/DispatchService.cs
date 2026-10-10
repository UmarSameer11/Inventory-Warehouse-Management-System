using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Dispatch;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class DispatchService : IDispatchService
    {
        private const string Endpoint = "/api/Dispatch";

        private readonly IApiService _apiService;
        private readonly ISalesOrderService _salesOrderService;
        private readonly IWarehouseService _warehouseService;
        private readonly IVehicleService _vehicleService;

        public DispatchService(
            IApiService apiService,
            ISalesOrderService salesOrderService,
            IWarehouseService warehouseService,
            IVehicleService vehicleService)
        {
            _apiService = apiService;
            _salesOrderService = salesOrderService;
            _warehouseService = warehouseService;
            _vehicleService = vehicleService;
        }

        public async Task<IEnumerable<DispatchListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<DispatchListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<DispatchListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no dispatch records yet.
                return Enumerable.Empty<DispatchListViewModel>();
            }
        }

        public async Task<DispatchUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<DispatchUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(DispatchCreateViewModel model)
            => await _apiService.PostAsync<DispatchCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(DispatchUpdateViewModel model)
            => await _apiService.PutAsync<DispatchUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.DispatchId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(DispatchCreateViewModel model)
        {
            model.SalesOrders = await _salesOrderService.GetDropdownAsync();
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, null);
            model.Vehicles = await _vehicleService.GetDropdownAsync();
        }

        public async Task PopulateDropdownsAsync(DispatchUpdateViewModel model)
        {
            model.SalesOrders = await _salesOrderService.GetDropdownAsync(model.SalesOrderId);
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, model.WarehouseId);
            model.Vehicles = await _vehicleService.GetDropdownAsync(model.VehicleId);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.DispatchId)
                .Select(x => new SelectListItem
                {
                    Value = x.DispatchId.ToString(),
                    Text = x.DispatchNumber
                })
                .ToList();
        }
    }
}
