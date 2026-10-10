using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceiptDetail;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class GoodsReceiptDetailService : IGoodsReceiptDetailService
    {
        private const string Endpoint = "/api/GoodsReceiptDetail";

        private readonly IApiService _apiService;
        private readonly IGoodsReceiptService _goodsReceiptService;
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public GoodsReceiptDetailService(
            IApiService apiService,
            IGoodsReceiptService goodsReceiptService,
            IProductService productService,
            IBatchService batchService)
        {
            _apiService = apiService;
            _goodsReceiptService = goodsReceiptService;
            _productService = productService;
            _batchService = batchService;
        }

        public async Task<IEnumerable<GoodsReceiptDetailListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<GoodsReceiptDetailListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<GoodsReceiptDetailListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no goods receipt detail records yet.
                return Enumerable.Empty<GoodsReceiptDetailListViewModel>();
            }
        }

        public async Task<GoodsReceiptDetailUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<GoodsReceiptDetailUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(GoodsReceiptDetailCreateViewModel model)
            => await _apiService.PostAsync<GoodsReceiptDetailCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(GoodsReceiptDetailUpdateViewModel model)
            => await _apiService.PutAsync<GoodsReceiptDetailUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.GoodsReceiptDetailId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(GoodsReceiptDetailCreateViewModel model)
        {
            model.GoodsReceipts = await _goodsReceiptService.GetDropdownAsync();
            model.Products = await DropdownHelper.ProductsAsync(_productService, null);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
        }

        public async Task PopulateDropdownsAsync(GoodsReceiptDetailUpdateViewModel model)
        {
            model.GoodsReceipts = await _goodsReceiptService.GetDropdownAsync(model.GoodsReceiptId);
            model.Products = await DropdownHelper.ProductsAsync(_productService, model.ProductId);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
        }
    }
}
