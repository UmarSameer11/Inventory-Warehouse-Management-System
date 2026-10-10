using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesOrder;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class SalesOrderService : ISalesOrderService
    {
        private const string Endpoint = "/api/SalesOrder";

        private readonly IApiService _apiService;
        private readonly ICustomerService _customerService;
        private readonly ISalesmanService _salesmanService;

        public SalesOrderService(
            IApiService apiService,
            ICustomerService customerService,
            ISalesmanService salesmanService)
        {
            _apiService = apiService;
            _customerService = customerService;
            _salesmanService = salesmanService;
        }

        public async Task<IEnumerable<SalesOrderListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<SalesOrderListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<SalesOrderListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no sales order records yet.
                return Enumerable.Empty<SalesOrderListViewModel>();
            }
        }

        public async Task<SalesOrderUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<SalesOrderUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(SalesOrderCreateViewModel model)
            => await _apiService.PostAsync<SalesOrderCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(SalesOrderUpdateViewModel model)
            => await _apiService.PutAsync<SalesOrderUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.SalesOrderId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(SalesOrderCreateViewModel model)
        {
            model.Customers = await _customerService.GetDropdownAsync();
            model.Salesmen = await _salesmanService.GetDropdownAsync();
        }

        public async Task PopulateDropdownsAsync(SalesOrderUpdateViewModel model)
        {
            model.Customers = await _customerService.GetDropdownAsync(model.CustomerId);
            model.Salesmen = await _salesmanService.GetDropdownAsync(model.SalesmanId);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.SalesOrderId)
                .Select(x => new SelectListItem
                {
                    Value = x.SalesOrderId.ToString(),
                    Text = x.SalesOrderNumber
                })
                .ToList();
        }
    }
}
