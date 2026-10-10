using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Vehicle;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private const string Endpoint = "/api/Vehicle";

        private readonly IApiService _apiService;
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IEmployeeService _employeeService;

        public VehicleService(
            IApiService apiService,
            IVehicleTypeService vehicleTypeService,
            IEmployeeService employeeService)
        {
            _apiService = apiService;
            _vehicleTypeService = vehicleTypeService;
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<VehicleListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<VehicleListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<VehicleListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no vehicle records yet.
                return Enumerable.Empty<VehicleListViewModel>();
            }
        }

        public async Task<VehicleUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<VehicleUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(VehicleCreateViewModel model)
            => await _apiService.PostAsync<VehicleCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(VehicleUpdateViewModel model)
            => await _apiService.PutAsync<VehicleUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.VehicleId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(VehicleCreateViewModel model)
        {
            model.VehicleTypes = await _vehicleTypeService.GetDropdownAsync();
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }

        public async Task PopulateDropdownsAsync(VehicleUpdateViewModel model)
        {
            model.VehicleTypes = await _vehicleTypeService.GetDropdownAsync(model.VehicleTypeId);
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .Where(x => x.IsActive || x.VehicleId == includeId)
                .OrderBy(x => x.VehicleId)
                .Select(x => new SelectListItem
                {
                    Value = x.VehicleId.ToString(),
                    Text = x.VehicleNumber
                })
                .ToList();
        }
    }
}
