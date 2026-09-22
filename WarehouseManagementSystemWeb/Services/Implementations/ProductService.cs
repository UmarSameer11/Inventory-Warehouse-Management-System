
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Product;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IApiService _apiService;
        private readonly IUnitOfMeasureService _unitOfMeasure;
        private readonly IProductCategoryService _productCategory;
        public ProductService(IApiService apiService, IUnitOfMeasureService unitOfMeasure, IProductCategoryService productCategory)
        {
            _apiService = apiService;
            _unitOfMeasure = unitOfMeasure;
            _productCategory = productCategory;
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(ProductCreateViewModel model)
        {
            var response = await _apiService.PostAsync<ProductCreateViewModel, ApiResponseViewModel<object>>("/api/Product", model);
            return response;
        }

        public async Task<ProductCreateViewModel> DropdownProductWithUnitAndCategoryAsync()
        {
            var category = await _productCategory.GetAllAsync();
            var unit = await _unitOfMeasure.GetAllAsync();

            var result = new ProductCreateViewModel
            {
                ProductCategories = category.Select(c => new SelectListItem
                {
                    Value = c.ProductCategoryId.ToString(),
                    Text = c.CategoryName,
                }).ToList(),

                UnitOfMeasures = unit.Select(u => new SelectListItem 
                {
                    Value = u.UnitOfMeasureId.ToString(),
                    Text = u.UnitName,
                }).ToList()
            };

            return result;
        }

        public async Task<IEnumerable<ProductListViewModel?>> GetAllAsync()
        {
            var response = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<ProductListViewModel>>>("/api/Product");

            return response?.Data ?? Enumerable.Empty<ProductListViewModel>();
        }

        public async Task<ProductUpdateViewModel?> GetByIdAsync(int id)
        {
            var response = await _apiService.GetByIdAsync<ApiResponseViewModel<ProductUpdateViewModel>>("/api/Product", id);
            return response?.Data;
        }

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(ProductUpdateViewModel model)
        {
            var response = await _apiService.PutAsync<ProductUpdateViewModel, ApiResponseViewModel<object>>($"/api/Product/{model.ProductId}", model);
            return response;
        }
    }
}
