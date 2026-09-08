using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Repositories.Interfaces;

namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class InventoryStockRepository : GenericRepository<InventoryStocks>, IInventoryStockRepository
    {
        private readonly ApplicationDbContext _context;
        public InventoryStockRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<InventoryStocks?> GetInventoryStockWithProdAndWarehouseByIdAsync(int id)
        {
            return await _context.InventoryStocks
                .Where(a => a.InventoryStockId == id)
                .Include(a => a.Product)
                .Include(a => a.Warehouse)
                .Include(a => a.Batch)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<InventoryStocks?>> GetInventoryStockWithProdAndWarehouseListAsync()
        {
            return await _context.InventoryStocks
                .Include(a => a.Product)
                .Include(a => a.Warehouse)
                .Include(a => a.Batch)
                .ToListAsync();
        }
    }
}
