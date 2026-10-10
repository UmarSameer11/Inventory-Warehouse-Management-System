using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrderDetail;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class PurchaseOrderDetailService : IPurchaseOrderDetailService
    {
        private const string Endpoint = "/api/PurchaseOrderDetail";

        private readonly IApiService _apiService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IProductService _productService;

        public PurchaseOrderDetailService(
            IApiService apiService,
            IPurchaseOrderService purchaseOrderService,
            IProductService productService)
        {
            _apiService = apiService;
            _purchaseOrderService = purchaseOrderService;
            _productService = productService;
        }

        public async Task<IEnumerable<PurchaseOrderDetailListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<PurchaseOrderDetailListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<PurchaseOrderDetailListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no purchase order detail records yet.
                return Enumerable.Empty<PurchaseOrderDetailListViewModel>();
            }
        }

        public async Task<PurchaseOrderDetailUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<PurchaseOrderDetailUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(PurchaseOrderDetailCreateViewModel model)
            => await _apiService.PostAsync<PurchaseOrderDetailCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(PurchaseOrderDetailUpdateViewModel model)
            => await _apiService.PutAsync<PurchaseOrderDetailUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.PurchaseOrderDetailId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(PurchaseOrderDetailCreateViewModel model)
        {
            model.PurchaseOrders = await _purchaseOrderService.GetDropdownAsync();
            model.Products = await DropdownHelper.ProductsAsync(_productService, null);
        }

        public async Task PopulateDropdownsAsync(PurchaseOrderDetailUpdateViewModel model)
        {
            model.PurchaseOrders = await _purchaseOrderService.GetDropdownAsync(model.PurchaseOrderId);
            model.Products = await DropdownHelper.ProductsAsync(_productService, model.ProductId);
        }
    }
}
