using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesReturn;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class SalesReturnService : ISalesReturnService
    {
        private const string Endpoint = "/api/SalesReturn";

        private readonly IApiService _apiService;
        private readonly ICustomerService _customerService;
        private readonly ISalesOrderService _salesOrderService;
        private readonly ISalesmanService _salesmanService;

        public SalesReturnService(
            IApiService apiService,
            ICustomerService customerService,
            ISalesOrderService salesOrderService,
            ISalesmanService salesmanService)
        {
            _apiService = apiService;
            _customerService = customerService;
            _salesOrderService = salesOrderService;
            _salesmanService = salesmanService;
        }

        public async Task<IEnumerable<SalesReturnListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<SalesReturnListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<SalesReturnListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no sales return records yet.
                return Enumerable.Empty<SalesReturnListViewModel>();
            }
        }

        public async Task<SalesReturnUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<SalesReturnUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(SalesReturnCreateViewModel model)
            => await _apiService.PostAsync<SalesReturnCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(SalesReturnUpdateViewModel model)
            => await _apiService.PutAsync<SalesReturnUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.SalesReturnId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(SalesReturnCreateViewModel model)
        {
            model.Customers = await _customerService.GetDropdownAsync();
            model.SalesOrders = await _salesOrderService.GetDropdownAsync();
            model.Salesmen = await _salesmanService.GetDropdownAsync();
        }

        public async Task PopulateDropdownsAsync(SalesReturnUpdateViewModel model)
        {
            model.Customers = await _customerService.GetDropdownAsync(model.CustomerId);
            model.SalesOrders = await _salesOrderService.GetDropdownAsync(model.SalesOrderId);
            model.Salesmen = await _salesmanService.GetDropdownAsync(model.SalesmanId);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.SalesReturnId)
                .Select(x => new SelectListItem
                {
                    Value = x.SalesReturnId.ToString(),
                    Text = x.ReturnNumber
                })
                .ToList();
        }
    }
}
