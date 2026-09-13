using WarehouseManagementSystemWeb.Application.ViewModels.Designation;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationViewModel?>> GetAllAsync();
    }
}
