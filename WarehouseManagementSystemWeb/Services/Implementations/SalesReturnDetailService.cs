using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesReturnDetail;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class SalesReturnDetailService : ISalesReturnDetailService
    {
        private const string Endpoint = "/api/SalesReturnDetail";

        private readonly IApiService _apiService;
        private readonly ISalesReturnService _salesReturnService;
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;
        private readonly IReturnReasonService _returnReasonService;

        public SalesReturnDetailService(
            IApiService apiService,
            ISalesReturnService salesReturnService,
            IProductService productService,
            IBatchService batchService,
            IReturnReasonService returnReasonService)
        {
            _apiService = apiService;
            _salesReturnService = salesReturnService;
            _productService = productService;
            _batchService = batchService;
            _returnReasonService = returnReasonService;
        }

        public async Task<IEnumerable<SalesReturnDetailListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<SalesReturnDetailListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<SalesReturnDetailListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no sales return detail records yet.
                return Enumerable.Empty<SalesReturnDetailListViewModel>();
            }
        }

        public async Task<SalesReturnDetailUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<SalesReturnDetailUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(SalesReturnDetailCreateViewModel model)
            => await _apiService.PostAsync<SalesReturnDetailCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(SalesReturnDetailUpdateViewModel model)
            => await _apiService.PutAsync<SalesReturnDetailUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.SalesReturnDetailId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(SalesReturnDetailCreateViewModel model)
        {
            model.SalesReturns = await _salesReturnService.GetDropdownAsync();
            model.Products = await DropdownHelper.ProductsAsync(_productService, null);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
            model.ReturnReasons = await _returnReasonService.GetDropdownAsync();
        }

        public async Task PopulateDropdownsAsync(SalesReturnDetailUpdateViewModel model)
        {
            model.SalesReturns = await _salesReturnService.GetDropdownAsync(model.SalesReturnId);
            model.Products = await DropdownHelper.ProductsAsync(_productService, model.ProductId);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
            model.ReturnReasons = await _returnReasonService.GetDropdownAsync(model.ReturnReasonId);
        }
    }
}
