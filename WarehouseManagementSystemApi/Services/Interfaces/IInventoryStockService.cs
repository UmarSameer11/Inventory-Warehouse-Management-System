using WarehouseManagementSystemApi.DTOs.InventoryStock;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IInventoryStockService
    {
        Task<IEnumerable<InventoryStockListDto>> GetInventoryStockListAsync();
        Task<InventoryStockListDto?> GetInventoryStockByIdAsync(int id);
        Task CreateInventoryStockAsync(InventoryStockCreateDto dto);
        Task UpdateInventoryStockAsync(int id, InventoryStockUpdateDto dto);
        Task DeleteInventoryStockAsync(int id);
    }
}
