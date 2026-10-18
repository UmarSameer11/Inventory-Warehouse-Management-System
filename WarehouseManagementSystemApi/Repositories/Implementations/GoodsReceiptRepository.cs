using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
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

    }
}
