using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.SalesOrderDetail;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface ISalesOrderDetailService
    {
        Task<IEnumerable<SalesOrderDetailListViewModel>> GetAllAsync();
        Task<SalesOrderDetailUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(SalesOrderDetailCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(SalesOrderDetailUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

        /// <summary>Fills the dropdown lists of the Create form.</summary>
        Task PopulateDropdownsAsync(SalesOrderDetailCreateViewModel model);

        /// <summary>Fills the dropdown lists of the Update form (the current selection stays selectable).</summary>
        Task PopulateDropdownsAsync(SalesOrderDetailUpdateViewModel model);
    }
}
