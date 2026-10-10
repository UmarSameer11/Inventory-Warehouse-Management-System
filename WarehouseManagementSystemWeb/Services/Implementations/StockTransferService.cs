using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockTransfer;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class StockTransferService : IStockTransferService
    {
        private const string Endpoint = "/api/StockTransfer";

        private readonly IApiService _apiService;
        private readonly IWarehouseService _warehouseService;

        public StockTransferService(IApiService apiService, IWarehouseService warehouseService)
        {
            _apiService = apiService;
            _warehouseService = warehouseService;
        }

        public async Task<IEnumerable<StockTransferListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<StockTransferListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<StockTransferListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no stock transfer records yet.
                return Enumerable.Empty<StockTransferListViewModel>();
            }
        }

        public async Task<StockTransferUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<StockTransferUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(StockTransferCreateViewModel model)
            => await _apiService.PostAsync<StockTransferCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(StockTransferUpdateViewModel model)
            => await _apiService.PutAsync<StockTransferUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.StockTransferId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(StockTransferCreateViewModel model)
        {
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, null);
        }

        public async Task PopulateDropdownsAsync(StockTransferUpdateViewModel model)
        {
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, model.FromWarehouseId);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.StockTransferId)
                .Select(x => new SelectListItem
                {
                    Value = x.StockTransferId.ToString(),
                    Text = x.TransferNumber
                })
                .ToList();
        }
    }
}
