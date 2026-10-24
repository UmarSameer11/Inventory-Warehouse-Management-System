using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.PurchaseOrder;
using WarehouseManagementSystemApi.Repositories.Interfaces;

namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PurchaseOrders?> GetPurchaseOrderForUpdation(int id)
        {
            return await _context.PurchaseOrders
                .Include(a => a.Supplier)
                .Include(a => a.Details)
                .ThenInclude(s => s.Product)
                .FirstOrDefaultAsync(s => s.PurchaseOrderId == id);
        }
    }
}
