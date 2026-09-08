using WarehouseManagementSystemApi.DTOs.Batch;
using WarehouseManagementSystemApi.DTOs.InventoryStock;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IBatchService
    {
        Task<IEnumerable<BatchListDto>> GetBatchListAsync();
        Task<BatchListDto?> GetBatchByIdAsync(int id);
        Task CreateBatchAsync(BatchCreateDto dto);
        Task UpdateBatchAsync(int id, BatchUpdateDto dto);
        Task DeleteBatchAsync(int id);
    }
}
