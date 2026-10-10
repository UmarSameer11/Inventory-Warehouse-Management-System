using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.ReturnReason;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class ReturnReasonService : IReturnReasonService
    {
        private const string Endpoint = "/api/ReturnReason";

        private readonly IApiService _apiService;

        public ReturnReasonService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<ReturnReasonListViewModel>> GetAllAsync()
        {
            try
            {
                var response = await _apiService
                    .GetAsync<ApiResponseViewModel<IEnumerable<ReturnReasonListViewModel>>>(Endpoint);

                return response?.Data ?? Enumerable.Empty<ReturnReasonListViewModel>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // The API answers 404 when there are no return reason records yet.
                return Enumerable.Empty<ReturnReasonListViewModel>();
            }
        }

        public async Task<ReturnReasonUpdateViewModel?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _apiService
                    .GetByIdAsync<ApiResponseViewModel<ReturnReasonUpdateViewModel>>(Endpoint, id);

                return response?.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<ApiResponseViewModel<object>?> CreateAsync(ReturnReasonCreateViewModel model)
            => await _apiService.PostAsync<ReturnReasonCreateViewModel, ApiResponseViewModel<object>>(Endpoint, model);

        public async Task<ApiResponseViewModel<object>?> UpdateAsync(ReturnReasonUpdateViewModel model)
            => await _apiService.PutAsync<ReturnReasonUpdateViewModel, ApiResponseViewModel<object>>(
                $"{Endpoint}/{model.ReturnReasonId}", model);

        public async Task<bool> DeleteAsync(int id)
            => await _apiService.DeleteAsync($"{Endpoint}/{id}");

        public async Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null)
        {
            var items = await GetAllAsync();

            return items
                .OrderBy(x => x.ReturnReasonId)
                .Select(x => new SelectListItem
                {
                    Value = x.ReturnReasonId.ToString(),
                    Text = x.ReasonName
                })
                .ToList();
        }
    }
}
