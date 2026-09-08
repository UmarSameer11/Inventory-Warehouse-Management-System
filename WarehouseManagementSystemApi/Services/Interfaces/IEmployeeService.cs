using WarehouseManagementSystemApi.DTOs.Employee;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListDto>> GetEmployeeListAsync();
        Task<EmployeeListDto?> GetEmployeeByIdAsync(int id);
        Task CreateEmployeeAsync(EmployeeCreateDto dto);
        Task UpdateEmployeeAsync(int id, EmployeeUpdateDto dto);
        Task DeleteEmployeeAsync(int id);

    }
}
