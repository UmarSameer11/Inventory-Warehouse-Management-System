using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.ProductCategory;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IApiService _apiService;

        public ProductCategoryService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(ProductCategoryCreateViewModel model)
            => await _apiService.PostAsync<ProductCategoryCreateViewModel, ApiResponseViewModel<object>>("/api/ProductCategory", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"/api/ProductCategory/{id}");

        public async Task<IEnumerable<ProductCategoryListViewModel?>> GetAllAsync()
        {
            var response = await _apiService.GetAsync<ApiResponseViewModel<IEnumerable<ProductCategoryListViewModel>>>("/api/ProductCategory");
            return response?.Data ?? Enumerable.Empty<ProductCategoryListViewModel>();
        }

        public async Task<ProductCategoryUpdateViewModel?> GetByIdAsync(int id)
        {
            var response = await _apiService.GetByIdAsync<ApiResponseViewModel<ProductCategoryUpdateViewModel>>("/api/ProductCategory", id);
            return response?.Data;
        }

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(ProductCategoryUpdateViewModel model)
            => await _apiService.PutAsync<ProductCategoryUpdateViewModel, ApiResponseViewModel<object>>($"/api/ProductCategory/{model.ProductCategoryId}", model);
    }
}