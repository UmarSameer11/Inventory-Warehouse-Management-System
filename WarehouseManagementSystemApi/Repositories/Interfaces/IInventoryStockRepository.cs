using WarehouseManagementSystemApi.Models.InventoryStock;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IInventoryStockRepository : IGenericRepository<InventoryStocks>
    {
        Task<IEnumerable<InventoryStocks?>> GetInventoryStockWithProdAndWarehouseListAsync();
        Task<InventoryStocks?> GetInventoryStockWithProdAndWarehouseByIdAsync(int id);
    }
}
