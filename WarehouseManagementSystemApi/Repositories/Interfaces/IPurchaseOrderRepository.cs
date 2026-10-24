using WarehouseManagementSystemApi.Models.PurchaseOrder;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrders?> GetPurchaseOrderForUpdation(int id);
    }
}
 