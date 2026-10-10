using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Customer;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private const string Endpoint = "/api/Customer";

        private readonly IApiService _apiService;

        public CustomerService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<CustomerListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<CustomerListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<CustomerListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no customer records yet.
                return Enumerable.Empty<CustomerListViewModel>();
            }
        }

        public async Task<CustomerUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<CustomerUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(CustomerCreateViewModel model)
            => await _apiService.PostAsync<CustomerCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(CustomerUpdateViewModel model)
            => await _apiService.PutAsync<CustomerUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.CustomerId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .Where(x => x.IsActive || x.CustomerId == includeId)
                .OrderBy(x => x.CustomerId)
                .Select(x => new SelectListItem
                {
                    Value = x.CustomerId.ToString(),
                    Text = $"{x.CustomerCode} - {x.CustomerName}"
                })
                .ToList();
        }
    }
}
