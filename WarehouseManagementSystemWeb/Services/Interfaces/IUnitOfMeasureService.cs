using WarehouseManagementSystemWeb.Application.ViewModels.Common;
using WarehouseManagementSystemWeb.Application.ViewModels.UnitOfMeasure;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IUnitOfMeasureService
    {
        Task<IEnumerable<UnitOfMeasureListViewModel?>> GetAllAsync();
        Task<UnitOfMeasureUpdateViewModel?> GetByIdAsync(int id);
        Task<ApiResponseViewModel<object>?> CreateAsync(UnitOfMeasureCreateViewModel model);
        Task<ApiResponseViewModel<object>?> UpdateAsync(UnitOfMeasureUpdateViewModel model);
        Task<bool> DeleteAsync(int id);
    }
}