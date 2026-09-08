using WarehouseManagementSystemApi.DTOs.Department;
using WarehouseManagementSystemApi.DTOs.Employee;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentListDto>> GetDepartmentListAsync();
        Task<DepartmentListDto?> GetDepartmentByIdAsync(int id);
        Task CreateDepartmentAsync(DepartmentCreateDto dto);
        Task UpdateDepartmentAsync(int id, DepartmentUpdateDto dto);
        Task DeleteDepartmentAsync(int id);
    }
}
