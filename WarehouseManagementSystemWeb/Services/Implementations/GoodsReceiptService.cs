using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class GoodsReceiptService : IGoodsReceiptService
    {
        private const string Endpoint = "/api/GoodsReceipt";

        private readonly IApiService _apiService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IWarehouseService _warehouseService;

        public GoodsReceiptService(
            IApiService apiService,
            IPurchaseOrderService purchaseOrderService,
            IWarehouseService warehouseService)
        {
            _apiService = apiService;
            _purchaseOrderService = purchaseOrderService;
            _warehouseService = warehouseService;
        }

        public async Task<IEnumerable<GoodsReceiptListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<GoodsReceiptListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<GoodsReceiptListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no goods receipt records yet.
                return Enumerable.Empty<GoodsReceiptListViewModel>();
            }
        }

        public async Task<GoodsReceiptUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<GoodsReceiptUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(GoodsReceiptCreateViewModel model)
            => await _apiService.PostAsync<GoodsReceiptCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(GoodsReceiptUpdateViewModel model)
            => await _apiService.PutAsync<GoodsReceiptUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.GoodsReceiptId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(GoodsReceiptCreateViewModel model)
        {
            model.PurchaseOrders = await _purchaseOrderService.GetDropdownAsync();
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, null);
        }

        public async Task PopulateDropdownsAsync(GoodsReceiptUpdateViewModel model)
        {
            model.PurchaseOrders = await _purchaseOrderService.GetDropdownAsync(model.PurchaseOrderId);
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, model.WarehouseId);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.GoodsReceiptId)
                .Select(x => new SelectListItem
                {
                    Value = x.GoodsReceiptId.ToString(),
                    Text = x.ReceiptNumber
                })
                .ToList();
        }
    }
}
