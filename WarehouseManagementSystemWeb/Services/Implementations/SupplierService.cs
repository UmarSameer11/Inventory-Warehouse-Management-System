using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Supplier;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private const string Endpoint = "/api/Supplier";

        private readonly IApiService _apiService;

        public SupplierService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<SupplierListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<SupplierListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<SupplierListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no supplier records yet.
                return Enumerable.Empty<SupplierListViewModel>();
            }
        }

        public async Task<SupplierUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<SupplierUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(SupplierCreateViewModel model)
            => await _apiService.PostAsync<SupplierCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(SupplierUpdateViewModel model)
            => await _apiService.PutAsync<SupplierUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.SupplierId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .Where(x => x.IsActive || x.SupplierId == includeId)
                .OrderBy(x => x.SupplierId)
                .Select(x => new SelectListItem
                {
                    Value = x.SupplierId.ToString(),
                    Text = $"{x.SupplierCode} - {x.SupplierName}"
                })
                .ToList();
        }
    }
}
