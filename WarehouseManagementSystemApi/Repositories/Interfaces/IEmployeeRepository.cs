using WarehouseManagementSystemApi.Models.Employee;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employees>
    {
        Task<IEnumerable<Employees?>> GetEmployeeWithDepartmentAndDesignationAsync();
        Task<Employees?> GetEmployeeWithDepartmentAndDesignationByIdAsync(int id);
    }
}
 