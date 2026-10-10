using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesOrderDetail;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private const string Endpoint = "/api/SalesOrderDetail";

        private readonly IApiService _apiService;
        private readonly ISalesOrderService _salesOrderService;
        private readonly IProductService _productService;

        public SalesOrderDetailService(
            IApiService apiService,
            ISalesOrderService salesOrderService,
            IProductService productService)
        {
            _apiService = apiService;
            _salesOrderService = salesOrderService;
            _productService = productService;
        }

        public async Task<IEnumerable<SalesOrderDetailListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<SalesOrderDetailListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<SalesOrderDetailListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no sales order detail records yet.
                return Enumerable.Empty<SalesOrderDetailListViewModel>();
            }
        }

        public async Task<SalesOrderDetailUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<SalesOrderDetailUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(SalesOrderDetailCreateViewModel model)
            => await _apiService.PostAsync<SalesOrderDetailCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(SalesOrderDetailUpdateViewModel model)
            => await _apiService.PutAsync<SalesOrderDetailUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.SalesOrderDetailId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(SalesOrderDetailCreateViewModel model)
        {
            model.SalesOrders = await _salesOrderService.GetDropdownAsync();
            model.Products = await DropdownHelper.ProductsAsync(_productService, null);
        }

        public async Task PopulateDropdownsAsync(SalesOrderDetailUpdateViewModel model)
        {
            model.SalesOrders = await _salesOrderService.GetDropdownAsync(model.SalesOrderId);
            model.Products = await DropdownHelper.ProductsAsync(_productService, model.ProductId);
        }
    }
}
