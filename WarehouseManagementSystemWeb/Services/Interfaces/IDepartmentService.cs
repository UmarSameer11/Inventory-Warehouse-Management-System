

using WarehouseManagementSystemWeb.Application.ViewModels.Department;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentViewModel?>> GetAllAsync();
    }
}
