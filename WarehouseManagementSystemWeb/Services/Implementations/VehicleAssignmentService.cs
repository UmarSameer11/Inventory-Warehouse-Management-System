using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.VehicleAssignment;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class VehicleAssignmentService : IVehicleAssignmentService
    {
        private const string Endpoint = "/api/VehicleAssignment";

        private readonly IApiService _apiService;
        private readonly IVehicleService _vehicleService;
        private readonly IEmployeeService _employeeService;

        public VehicleAssignmentService(
            IApiService apiService,
            IVehicleService vehicleService,
            IEmployeeService employeeService)
        {
            _apiService = apiService;
            _vehicleService = vehicleService;
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<VehicleAssignmentListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<VehicleAssignmentListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<VehicleAssignmentListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no vehicle assignment records yet.
                return Enumerable.Empty<VehicleAssignmentListViewModel>();
            }
        }

        public async Task<VehicleAssignmentUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<VehicleAssignmentUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(VehicleAssignmentCreateViewModel model)
            => await _apiService.PostAsync<VehicleAssignmentCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(VehicleAssignmentUpdateViewModel model)
            => await _apiService.PutAsync<VehicleAssignmentUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.VehicleAssignmentId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(VehicleAssignmentCreateViewModel model)
        {
            model.Vehicles = await _vehicleService.GetDropdownAsync();
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }

        public async Task PopulateDropdownsAsync(VehicleAssignmentUpdateViewModel model)
        {
            model.Vehicles = await _vehicleService.GetDropdownAsync(model.VehicleId);
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }
    }
}
