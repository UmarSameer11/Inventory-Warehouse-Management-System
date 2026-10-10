using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustment;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class StockAdjustmentService : IStockAdjustmentService
    {
        private const string Endpoint = "/api/StockAdjustment";

        private readonly IApiService _apiService;
        private readonly IWarehouseService _warehouseService;
        private readonly IEmployeeService _employeeService;

        public StockAdjustmentService(
            IApiService apiService,
            IWarehouseService warehouseService,
            IEmployeeService employeeService)
        {
            _apiService = apiService;
            _warehouseService = warehouseService;
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<StockAdjustmentListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<StockAdjustmentListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<StockAdjustmentListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no stock adjustment records yet.
                return Enumerable.Empty<StockAdjustmentListViewModel>();
            }
        }

        public async Task<StockAdjustmentUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<StockAdjustmentUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(StockAdjustmentCreateViewModel model)
            => await _apiService.PostAsync<StockAdjustmentCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(StockAdjustmentUpdateViewModel model)
            => await _apiService.PutAsync<StockAdjustmentUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.StockAdjustmentId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(StockAdjustmentCreateViewModel model)
        {
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, null);
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }

        public async Task PopulateDropdownsAsync(StockAdjustmentUpdateViewModel model)
        {
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, model.WarehouseId);
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.StockAdjustmentId)
                .Select(x => new SelectListItem
                {
                    Value = x.StockAdjustmentId.ToString(),
                    Text = x.AdjustmentNumber
                })
                .ToList();
        }
    }
}
