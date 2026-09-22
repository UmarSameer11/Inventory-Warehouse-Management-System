using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.Warehouse;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseListViewModel?>> GetAllAsync();
        Task<WarehouseUpdateViewModel?> GetByIdAsync(int id); 
        Task<ApiResponseViewModel<object>?> CreateAsync(WarehouseCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(WarehouseUpdateViewModel model);
        Task<bool> DeleteAsync(int id);
        Task<List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>> GetEmployeeDropdownAsync();
    }
}