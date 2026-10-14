using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
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
        public async Task<PurchaseOrderForGrnDto?> GetForGrnByNumberAsync(string purchaseOrderNumber)
        {
            var purchaseOrder = await _context.PurchaseOrders
             .AsNoTracking()
             .Include(po => po.Details)
                 .ThenInclude(detail => detail.Product)
             .FirstOrDefaultAsync(po =>
                 po.PurchaseOrderNumber == purchaseOrderNumber);

            if (purchaseOrder == null)
                return null;

            var result = new PurchaseOrderForGrnDto
            {
                PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber
            };

            foreach (var detail in purchaseOrder.Details)
            {
                var previouslyReceived =
                    await _context.GoodsReceiptDetails
                        .Where(grd =>
                            grd.PurchaseOrderDetailId ==
                            detail.PurchaseOrderDetailId)
                        .SumAsync(grd =>
                            (decimal?)grd.QuantityReceived) ?? 0m;

                var orderedQuantity = detail.Quantity;
                var remainingQuantity =
                    Math.Max(0m, orderedQuantity - previouslyReceived);

                result.Details.Add(new PurchaseOrderGrnDetailDto
                {
                    PurchaseOrderDetailId = detail.PurchaseOrderDetailId,
                    ProductId = detail.ProductId,
                    ProductName = detail.Product.ProductName,
                    OrderedQuantity = orderedQuantity,
                    PreviouslyReceivedQuantity = previouslyReceived,
                    RemainingQuantity = remainingQuantity,
                    UnitPrice = detail.UnitPrice
                });
            }

            return result;
        }
    }
}
