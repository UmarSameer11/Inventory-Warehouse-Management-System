using WarehouseManagementSystemApi.DTOs.PurchaseOrder;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IGoodsReceiptRepository
    {
        public Task<PurchaseOrderForGrnDto?> GetForGrnByNumberAsync(string purchaseOrderNumber);
    }
}
