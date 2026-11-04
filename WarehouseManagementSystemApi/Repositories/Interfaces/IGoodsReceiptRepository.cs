using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Models.PurchaseOrder;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IGoodsReceiptRepository
    {
        public Task<PurchaseOrderForGrnDto?> GetForGrnByNumberAsync(string purchaseOrderNumber);
        Task<int> GetLastGrnNumberAsync(int year);
        Task<GoodsReceipts?> GetGoodReceiptByIdAsync(int id);
        //Task<IEnumerable<GoodsReceipts>?> GetGoodReceiptListAsync();
    }
}
