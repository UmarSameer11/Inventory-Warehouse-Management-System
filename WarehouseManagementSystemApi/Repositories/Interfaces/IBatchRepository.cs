using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Employee;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IBatchRepository : IGenericRepository<Batches>
    {
        Task<IEnumerable<Batches?>> GetBatchListWithProductNameAsync();
        Task<Batches?> GetBatchWithProductNameById(int id);
    }
}
