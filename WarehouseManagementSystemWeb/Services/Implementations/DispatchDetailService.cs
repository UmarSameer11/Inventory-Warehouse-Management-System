using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.DispatchDetail;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class DispatchDetailService : IDispatchDetailService
    {
        private const string Endpoint = "/api/DispatchDetail";

        private readonly IApiService _apiService;
        private readonly IDispatchService _dispatchService;
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public DispatchDetailService(
            IApiService apiService,
            IDispatchService dispatchService,
            IProductService productService,
            IBatchService batchService)
        {
            _apiService = apiService;
            _dispatchService = dispatchService;
            _productService = productService;
            _batchService = batchService;
        }

        public async Task<IEnumerable<DispatchDetailListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<DispatchDetailListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<DispatchDetailListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no dispatch detail records yet.
                return Enumerable.Empty<DispatchDetailListViewModel>();
            }
        }

        public async Task<DispatchDetailUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<DispatchDetailUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(DispatchDetailCreateViewModel model)
            => await _apiService.PostAsync<DispatchDetailCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(DispatchDetailUpdateViewModel model)
            => await _apiService.PutAsync<DispatchDetailUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.DispatchDetailId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(DispatchDetailCreateViewModel model)
        {
            model.Dispatches = await _dispatchService.GetDropdownAsync();
            model.Products = await DropdownHelper.ProductsAsync(_productService, null);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
        }

        public async Task PopulateDropdownsAsync(DispatchDetailUpdateViewModel model)
        {
            model.Dispatches = await _dispatchService.GetDropdownAsync(model.DispatchId);
            model.Products = await DropdownHelper.ProductsAsync(_productService, model.ProductId);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
        }
    }
}
