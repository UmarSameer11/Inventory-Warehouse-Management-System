
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IWarehouseRepository : IGenericRepository<Warehouses>
    {
        Task<IEnumerable<Warehouses?>> GetWarehouseWithEmployeeListAsync();
        Task<Warehouses?> GetWarehouseWithEmployeeByIdAsync(int id);
    }
}
