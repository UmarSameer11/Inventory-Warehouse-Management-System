using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.StockMovement;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class StockMovementService : IStockMovementService
    {
        private const string Endpoint = "/api/StockMovement";

        private readonly IApiService _apiService;
        private readonly IWarehouseService _warehouseService;
        private readonly IProductService _productService;
        private readonly IBatchService _batchService;
        private readonly IEmployeeService _employeeService;

        public StockMovementService(
            IApiService apiService,
            IWarehouseService warehouseService,
            IProductService productService,
            IBatchService batchService,
            IEmployeeService employeeService)
        {
            _apiService = apiService;
            _warehouseService = warehouseService;
            _productService = productService;
            _batchService = batchService;
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<StockMovementListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<StockMovementListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<StockMovementListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no stock movement records yet.
                return Enumerable.Empty<StockMovementListViewModel>();
            }
        }

        public async Task<StockMovementUpdateViewModel?> GetByIdAsync(long id)
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<StockMovementUpdateViewModel>>($"{Endpoint}/{id}");

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(StockMovementCreateViewModel model)
            => await _apiService.PostAsync<StockMovementCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(StockMovementUpdateViewModel model)
            => await _apiService.PutAsync<StockMovementUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.StockMovementId}", model);

        public async Task<bool> DeleteAsync(long id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(StockMovementCreateViewModel model)
        {
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, null);
            model.Products = await DropdownHelper.ProductsAsync(_productService, null);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }

        public async Task PopulateDropdownsAsync(StockMovementUpdateViewModel model)
        {
            model.Warehouses = await DropdownHelper.WarehousesAsync(_warehouseService, model.WarehouseId);
            model.Products = await DropdownHelper.ProductsAsync(_productService, model.ProductId);
            model.Batches = await DropdownHelper.BatchesAsync(_batchService);
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }
    }
}
