using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Batch;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.InventoryStock;
using WarehouseManagementSystemWeb.Application.ViewModels.Product;
using WarehouseManagementSystemWeb.Application.ViewModels.Warehouse;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class InventoryStockService : IInventoryStockService
    {
        private const string Endpoint = "/api/InventoryStock";

        private readonly IApiService _apiService;
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;

        public InventoryStockService(
            IApiService apiService,
            IWarehouseService warehouseService,
            IProductService productService,
            IBatchService batchService)
        {
            _apiService = apiService;
            _warehouseService = warehouseService;
            _productService = productService;
            _batchService = batchService;
        }

        public async Task<IEnumerable<InventoryStockListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<InventoryStockListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<InventoryStockListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no stock records yet.
                return Enumerable.Empty<InventoryStockListViewModel>();
            }
        }

        public async Task<InventoryStockUpdateViewModel?> GetByIdAsync(int id)
        {
            InventoryStockUpdateViewModel? model;

            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<InventoryStockUpdateViewModel>>(Endpoint, id);

                model = response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (model == null)
            {
                return null;
            }

            await ResolveIdsAsync(model);

            return model;
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(InventoryStockCreateViewModel model)
            => await _apiService.PostAsync<InventoryStockCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(InventoryStockUpdateViewModel model)
            => await _apiService.PutAsync<InventoryStockUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.InventoryStockId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(InventoryStockFormViewModel model)
        {
            // On Update the record's current warehouse/product must stay selectable
            // even if it has been deactivated meanwhile.
            var currentWarehouseId = (model as InventoryStockUpdateViewModel)?.WarehouseId;
            var currentProductId = (model as InventoryStockUpdateViewModel)?.ProductId;

            var warehouses = await GetAllWarehousesAsync();
            var products = await GetAllProductsAsync();
            var batches = await GetAllBatchesAsync();

            model.Warehouses = warehouses
                .Where(w => w.IsActive || w.WarehouseId == currentWarehouseId)
                .OrderBy(w => w.WarehouseName)
                .Select(w => new SelectListItem
                {
                    Value = w.WarehouseId.ToString(),
                    Text = $"{w.WarehouseCode} - {w.WarehouseName}"
                })
                .ToList();

            model.Products = products
                .Where(p => p.IsActive || p.ProductId == currentProductId)
                .OrderBy(p => p.ProductName)
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = $"{p.ProductCode} - {p.ProductName}"
                })
                .ToList();

            model.Batches = batches
                .OrderBy(b => b.BatchNumber)
                .Select(b => new BatchOptionViewModel
                {
                    Value = b.BatchId,
                    ProductId = b.ProductId,
                    Text = b.ExpiryDate.HasValue
                        ? $"{b.BatchNumber} (Exp {b.ExpiryDate.Value:dd MMM yyyy})"
                        : b.BatchNumber
                })
                .ToList();
        }

        /// <summary>
        /// GET /api/InventoryStock/{id} only returns names (WarehouseName, ProductName, BatchNumber).
        /// The Update form needs the IDs to pre-select the dropdowns, so when the API does not
        /// return them they are looked up by name. This does nothing once the API list DTO
        /// exposes WarehouseId / ProductId / BatchId.
        /// </summary>
        private async Task ResolveIdsAsync(InventoryStockUpdateViewModel model)
        {
            if (model.WarehouseId == 0)
            {
                var warehouses = await GetAllWarehousesAsync();

                model.WarehouseId = warehouses
                    .FirstOrDefault(w => w.WarehouseName == model.WarehouseName)?.WarehouseId ?? 0;
            }

            if (model.ProductId == 0)
            {
                var products = await GetAllProductsAsync();

                model.ProductId = products
                    .FirstOrDefault(p => p.ProductName == model.ProductName)?.ProductId ?? 0;
            }

            if (model.BatchId == 0)
            {
                var batches = await GetAllBatchesAsync();

                model.BatchId = batches
                    .FirstOrDefault(b => b.BatchNumber == model.BatchNumber
                                         && (model.ProductId == 0 || b.ProductId == model.ProductId))?.BatchId ?? 0;
            }
        }

        private async Task<List<WarehouseListViewModel>> GetAllWarehousesAsync()
        {
            try
            {
                var warehouses = await _warehouseService.GetAllAsync();
                return warehouses.Where(w => w != null).Select(w => w!).ToList();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<WarehouseListViewModel>();
            }
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

        private async Task<List<BatchListViewModel>> GetAllBatchesAsync()
            => (await _batchService.GetAllAsync()).ToList();
    }
}
