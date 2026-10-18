
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Product;
using WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private const string Endpoint = "/api/PurchaseOrder";

        private readonly IApiService _apiService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;

        public PurchaseOrderService(
            IApiService apiService,
            ISupplierService supplierService,
            IProductService productService)
        {
            _apiService = apiService;
            _supplierService = supplierService;
            _productService = productService;
        }

        public async Task<IEnumerable<PurchaseOrderListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<PurchaseOrderListViewModel>>>(
                        Endpoint);

                return response?.Data
                    ?? Enumerable.Empty<PurchaseOrderListViewModel>();
            }
            catch (HttpRequestException ex)
                when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Return an empty collection when no purchase orders exist.
                return Enumerable.Empty<PurchaseOrderListViewModel>();
            }
        }

        public async Task<PurchaseOrderUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<PurchaseOrderUpdateViewModel>>(
                        Endpoint,
                        id);

                return response?.Data;
            }
            catch (HttpRequestException ex)
                when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(
            PurchaseOrderCreateViewModel model)
        {
            return await _apiService
                .PostAsync<PurchaseOrderCreateViewModel, ApiResponseViewModel<object>>(
                    Endpoint,
                    model);
        }

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(
            PurchaseOrderUpdateViewModel model)
        {
            return await _apiService
                .PutAsync<PurchaseOrderUpdateViewModel, ApiResponseViewModel<object>>(
                    $"{Endpoint}/{model.PurchaseOrderId}",
                    model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _apiService.DeleteAsync($"{Endpoint}/{id}");
        }

        public async Task PopulateDropdownsAsync(
            PurchaseOrderCreateViewModel model)
        {
            model.Suppliers = await _supplierService.GetDropdownAsync();

            var products = await _productService.GetAllAsync();

            model.ProductList = products
                .Select(product => new SelectListItem
                {
                    Value = product!.ProductId.ToString(),
                    Text = product.ProductName
                })
                .ToList();
        }

        public async Task PopulateDropdownsAsync(
            PurchaseOrderUpdateViewModel model)
        {
            model.Suppliers = await _supplierService
                .GetDropdownAsync(model.SupplierId);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(
            int? includeId = null)
        {
            var purchaseOrders = await GetAllAsync();

            return purchaseOrders
                .OrderBy(order => order.PurchaseOrderId)
                .Select(order => new SelectListItem
                {
                    Value = order.PurchaseOrderId.ToString(),
                    Text = order.PurchaseOrderNumber
                })
                .ToList();
        }
    }
}
