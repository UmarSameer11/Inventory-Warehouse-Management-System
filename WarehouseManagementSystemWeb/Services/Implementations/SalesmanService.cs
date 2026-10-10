using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Salesman;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class SalesmanService : ISalesmanService
    {
        private const string Endpoint = "/api/Salesman";

        private readonly IApiService _apiService;
        private readonly IEmployeeService _employeeService;

        public SalesmanService(IApiService apiService, IEmployeeService employeeService)
        {
            _apiService = apiService;
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<SalesmanListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<SalesmanListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<SalesmanListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no salesman records yet.
                return Enumerable.Empty<SalesmanListViewModel>();
            }
        }

        public async Task<SalesmanUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<SalesmanUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(SalesmanCreateViewModel model)
            => await _apiService.PostAsync<SalesmanCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(SalesmanUpdateViewModel model)
            => await _apiService.PutAsync<SalesmanUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.SalesmanId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task PopulateDropdownsAsync(SalesmanCreateViewModel model)
        {
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }

        public async Task PopulateDropdownsAsync(SalesmanUpdateViewModel model)
        {
            model.Employees = await DropdownHelper.EmployeesAsync(_employeeService);
        }

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var salesmen = await GetAllAsync();
            var employees = await DropdownHelper.GetEmployeesAsync(_employeeService);

            return salesmen
                .Where(x => x.IsActive || x.SalesmanId == includeId)
                .OrderBy(x => x.SalesmanCode)
                .Select(x =>
                {
                    var employee = employees.FirstOrDefault(e => e.EmployeeId == x.EmployeeId);

                    return new SelectListItem
                    {
                        Value = x.SalesmanId.ToString(),
                        Text = employee == null
                            ? x.SalesmanCode
                            : $"{x.SalesmanCode} - {employee.FirstName} {employee.LastName}"
                    };
                })
                .ToList();
        }
    }
}
