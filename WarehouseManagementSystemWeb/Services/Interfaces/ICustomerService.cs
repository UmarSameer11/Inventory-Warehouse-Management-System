using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Customer;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerListViewModel>> GetAllAsync();
        Task<CustomerUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(CustomerCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(CustomerUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Customer options for the dropdowns of other forms.</summary>
        Task<List<SelectListItem>> GetDropdownAsync(int? includeId = null);
    }
}
