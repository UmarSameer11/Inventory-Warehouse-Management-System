using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockTransferDetail;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class StockTransferDetailService : IStockTransferDetailService
    {
        private const string Endpoint = "/api/StockTransferDetail";

        private readonly IApiService _apiService;
        private readonly IStockTransferService _stockTransferService;
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public StockTransferDetailService(
            IApiService apiService,
            IStockTransferService stockTransferService,
            IProductService productService,
            IBatchService batchService)
        {
            _apiService = apiService;
            _stockTransferService = stockTransferService;
            _productService = productService;
            _batchService = batchService;
        }

        public async Task<IEnumerable<StockTransferDetailListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<StockTransferDetailListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<StockTransferDetailListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no stock transfer detail records yet.
                return Enumerable.Empty<StockTransferDetailListViewModel>();
            }
        }

        public async Task<StockTransferDetailUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<StockTransferDetailUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(StockTransferDetailCreateViewModel model)
            => await _apiService.PostAsync<StockTransferDetailCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(StockTransferDetailUpdateViewModel model)
            => await _apiService.PutAsync<StockTransferDetailUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.StockTransferDetailId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(StockTransferDetailCreateViewModel model)
        {
            model.StockTransfers = await _stockTransferService.GetDropdownAsync();
            model.Products = await DropdownHelper.ProductsAsync(_productService, null);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
        }

        public async Task PopulateDropdownsAsync(StockTransferDetailUpdateViewModel model)
        {
            model.StockTransfers = await _stockTransferService.GetDropdownAsync(model.StockTransferId);
            model.Products = await DropdownHelper.ProductsAsync(_productService, model.ProductId);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
        }
    }
}
