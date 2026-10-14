using WarehouseManagementSystemApi.DTOs.PurchaseOrder;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        public Task<PurchaseOrderForGrnDto?> GetForGrnByNumberAsync(string purchaseOrderNumber);
    }
}
