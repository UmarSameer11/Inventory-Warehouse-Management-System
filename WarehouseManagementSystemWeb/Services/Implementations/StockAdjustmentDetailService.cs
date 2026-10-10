using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustmentDetail;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class StockAdjustmentDetailService : IStockAdjustmentDetailService
    {
        private const string Endpoint = "/api/StockAdjustmentDetail";

        private readonly IApiService _apiService;
        private readonly IStockAdjustmentService _stockAdjustmentService;
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public StockAdjustmentDetailService(
            IApiService apiService,
            IStockAdjustmentService stockAdjustmentService,
            IProductService productService,
            IBatchService batchService)
        {
            _apiService = apiService;
            _stockAdjustmentService = stockAdjustmentService;
            _productService = productService;
            _batchService = batchService;
        }

        public async Task<IEnumerable<StockAdjustmentDetailListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<StockAdjustmentDetailListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<StockAdjustmentDetailListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no stock adjustment detail records yet.
                return Enumerable.Empty<StockAdjustmentDetailListViewModel>();
            }
        }

        public async Task<StockAdjustmentDetailUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<StockAdjustmentDetailUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(StockAdjustmentDetailCreateViewModel model)
            => await _apiService.PostAsync<StockAdjustmentDetailCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(StockAdjustmentDetailUpdateViewModel model)
            => await _apiService.PutAsync<StockAdjustmentDetailUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.StockAdjustmentDetailId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(StockAdjustmentDetailCreateViewModel model)
        {
            model.StockAdjustments = await _stockAdjustmentService.GetDropdownAsync();
            model.Products = await DropdownHelper.ProductsAsync(_productService, null);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
        }

        public async Task PopulateDropdownsAsync(StockAdjustmentDetailUpdateViewModel model)
        {
            model.StockAdjustments = await _stockAdjustmentService.GetDropdownAsync(model.StockAdjustmentId);
            model.Products = await DropdownHelper.ProductsAsync(_productService, model.ProductId);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
        }
    }
}
