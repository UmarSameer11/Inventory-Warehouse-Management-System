using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Batch;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Product;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class BatchService : IBatchService
    {
        private const string Endpoint = "/api/Batch";

        private readonly IApiService _apiService;
        private readonly IProductService _productService;

        public BatchService(IApiService apiService, IProductService productService)
        {
            _apiService = apiService;
            _productService = productService;
        }

        public async Task<IEnumerable<BatchListViewModel>> GetAllAsync()
        {
            List<BatchListViewModel> batches;

            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<BatchListViewModel>>>(Endpoint);

                batches = response?.Data?.ToList() ?? new List<BatchListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no batches yet.
                return new List<BatchListViewModel>();
            }

            if (batches.Any(b => b.ProductId == 0))
            {
                var products = await GetAllProductsAsync();

                foreach (var batch in batches.Where(b => b.ProductId == 0))
                {
                    batch.ProductId = products
                        .FirstOrDefault(p => p.ProductName == batch.ProductName)?.ProductId ?? 0;
                }
            }

            return batches;
        }

        public async Task<BatchUpdateViewModel?> GetByIdAsync(int id)
        {
            BatchUpdateViewModel? model = null;

            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<BatchUpdateViewModel>>(Endpoint, id);

                model = response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Fall through to the list lookup below.
            }

            // GET /api/Batch/{id} currently filters by ProductId instead of BatchId
            // (BatchRepository.GetBatchWithProductNameById). If the wrong record comes
            // back, look the batch up in the list so we never edit/delete the wrong one.
            if (model == null || model.BatchId != id)
            {
                var match = (await GetAllAsync()).FirstOrDefault(b => b.BatchId == id);

                if (match == null)
                {
                    return null;
                }

                model = new BatchUpdateViewModel
                {
                    BatchId = match.BatchId,
                    ProductId = match.ProductId,
                    ProductName = match.ProductName,
                    BatchNumber = match.BatchNumber,
                    ManufacturingDate = match.ManufacturingDate,
                    ExpiryDate = match.ExpiryDate
                };
            }

            if (model.ProductId == 0)
            {
                var products = await GetAllProductsAsync();

                model.ProductId = products
                    .FirstOrDefault(p => p.ProductName == model.ProductName)?.ProductId ?? 0;
            }

            return model;
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(BatchCreateViewModel model)
            => await _apiService.PostAsync<BatchCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(BatchUpdateViewModel model)
            => await _apiService.PutAsync<BatchUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.BatchId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task<List<SelectListItem>> GetProductDropdownAsync(int? includeProductId = null)
        {
            var products = await GetAllProductsAsync();

            return products
                .Where(p => p.IsActive || p.ProductId == includeProductId)
                .OrderBy(p => p.ProductName)
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = $"{p.ProductCode} - {p.ProductName}"
                })
                .ToList();
        }

        private async Task<List<ProductListViewModel>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return products.Where(p => p != null).Select(p => p!).ToList();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<ProductListViewModel>();
            }
        }
    }
}
