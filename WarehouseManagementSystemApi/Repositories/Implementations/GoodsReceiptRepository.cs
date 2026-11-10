using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Repositories.Interfaces;

namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class GoodsReceiptRepository : IGoodsReceiptRepository
    {
        private readonly ApplicationDbContext _context;

        public GoodsReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GoodsReceipts?>> GetAllGoodReceiptAsync()
        {
            var result = await _context.GoodsReceipts
                .Include(s => s.PurchaseOrder)
                .Include(s => s.Warehouse)
                .ToListAsync();
            return result;
        }

        public async Task<PurchaseOrderForGrnDto?> GetForGrnByNumberAsync(
            string purchaseOrderNumber)
        {
            if (string.IsNullOrWhiteSpace(purchaseOrderNumber))
                return null;

            var purchaseOrder = await _context.PurchaseOrders
                .AsNoTracking()
                .Include(po => po.Details)
                    .ThenInclude(detail => detail.Product)
                .FirstOrDefaultAsync(po =>
                    po.PurchaseOrderNumber == purchaseOrderNumber.Trim());

            if (purchaseOrder == null)
                return null;

            var result = new PurchaseOrderForGrnDto
            {
                PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber,
                Details = new List<PurchaseOrderGrnDetailDto>()
            };

            foreach (var detail in purchaseOrder.Details ?? [])
            {
                var previouslyReceived =
                    await _context.GoodsReceiptDetails
                        .Where(grd =>
                            grd.PurchaseOrderDetailId ==
                            detail.PurchaseOrderDetailId)
                        .SumAsync(grd =>
                            (decimal?)grd.QuantityReceived) ?? 0m;

                var orderedQuantity = detail.Quantity;

                var remainingQuantity = Math.Max(
                    0m,
                    orderedQuantity - previouslyReceived);

                result.Details.Add(new PurchaseOrderGrnDetailDto
                {
                    PurchaseOrderDetailId = detail.PurchaseOrderDetailId,
                    ProductId = detail.ProductId,

                    // Avoid NullReferenceException if Product navigation is missing.
                    ProductName = detail.Product?.ProductName ?? "Unknown Product",

                    OrderedQuantity = orderedQuantity,
                    PreviouslyReceivedQuantity = previouslyReceived,
                    RemainingQuantity = remainingQuantity,
                    UnitPrice = detail.UnitPrice
                });
            }

            return result;
        }

        public async Task<GoodsReceipts?> GetGoodReceiptByIdAsync(int id)
        {
            return await _context.GoodsReceipts
                .Include(g => g.PurchaseOrder)
                .ThenInclude(g => g.Details)
                    //.ThenInclude(p => p.Supplier)
                .Include(g => g.Warehouse)
                .Include(g => g.ReceivedByEmployee)
                .Include(g => g.Details)
                    .ThenInclude(d => d.Product)
                .Include(g => g.Details)
                    .ThenInclude(d => d.Batch)
                .FirstOrDefaultAsync(g => g.GoodsReceiptId == id);
        }

        public async Task<int> GetLastGrnNumberAsync(int year)
        {
            var prefix = "GRN-2026-";

            var numbers = await _context.GoodsReceipts
                .Where(s => s.GoodsReceiptNumber.StartsWith(prefix))
                .Select(s => s.GoodsReceiptNumber)
                .ToListAsync();

            return numbers
              .Select(number => int.TryParse(
         number.Substring(prefix.Length), out var result) ? result : 0).DefaultIfEmpty(0).Max();
        }
    }
}
