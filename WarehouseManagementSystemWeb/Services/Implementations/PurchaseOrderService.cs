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

        public PurchaseOrderService(IApiService apiService, ISupplierService supplierService, IProductService productService)
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
                    .GetAsync<ApiResponseViewModel<IEnumerable<PurchaseOrderListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<PurchaseOrderListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no purchase order records yet.
                return Enumerable.Empty<PurchaseOrderListViewModel>();
            }
        }

        public async Task<PurchaseOrderUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<PurchaseOrderUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(PurchaseOrderCreateViewModel model)
            => await _apiService.PostAsync<PurchaseOrderCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(PurchaseOrderUpdateViewModel model)
            => await _apiService.PutAsync<PurchaseOrderUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.PurchaseOrderId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(PurchaseOrderCreateViewModel model)
        {
            model.Suppliers = await _supplierService.GetDropdownAsync();
            var product = await _productService.GetAllAsync();
            model.ProductList = product.Select(c => new SelectListItem
            {
                Value = c!.ProductId.ToString(),
                Text = c!.ProductName,
            }).ToList();
        }
     

        public async Task PopulateDropdownsAsync(PurchaseOrderUpdateViewModel model)
        {
            model.Suppliers = await _supplierService.GetDropdownAsync(model.SupplierId);
        }

        //public async Task PopulateDropdownsAsync(ProductCreateViewModel model)
        //{
        //   _productService.
        //}
        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.PurchaseOrderId)
                .Select(x => new SelectListItem
                {
                    Value = x.PurchaseOrderId.ToString(),
                    Text = x.PurchaseOrderNumber
                })
                .ToList();
        }

        //public async Task<ProductCreateViewModel> GetDropdownForProductAsync()
        //{
        //    var product = await _productService.GetAllAsync();

        //    var result = new ProductCreateViewModel
        //    {
        //        ProductCategories = product.Select(c => new SelectListItem
        //        {
        //            Value = c!.ProductId.ToString(),
        //            Text = c!.ProductName,
        //        }).ToList()

        //    };

        //    return result;
        //}


    }
}


