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

        public async Task<int> GetLastPurchaseOrderNumberAsync(int year)
        {
            var prefix = $"PO-{year}-";

            var numbers = await _context.PurchaseOrders
                .Where(p => p.PurchaseOrderNumber.StartsWith(prefix))
                .Select(p => p.PurchaseOrderNumber)
                .ToListAsync();

            return numbers
                .Select(number => int.TryParse(
           number.Substring(prefix.Length), out var result) ? result : 0) .DefaultIfEmpty(0) .Max();

        }

        public async Task<PurchaseOrders?> GetPurchaseOrderForUpdation(int id)
        {
            return await _context.PurchaseOrders
                .Include(a => a.Supplier)
                .Include(a => a.Details)
                .ThenInclude(s => s.Product)
                .FirstOrDefaultAsync(s => s.PurchaseOrderId == id);
        }

        public async Task<IEnumerable<PurchaseOrders>?> GetPurchaseOrderList()
        {
            return await _context.PurchaseOrders
                .Include(a => a.Supplier)
                .Include(a => a.Details)
                .ThenInclude(s => s.Product)
                .ToListAsync();
        }
    }
}
