using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
using WarehouseManagementSystemApi.DTOs.Product;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IGoodsReceiptService
{
    Task<GoodsReceiptListDto> CreateAsync(GoodsReceiptCreateDto dto);
    Task<IEnumerable<GoodsReceiptListDto>> GetAllAsync();
    Task<GoodsReceiptListDto> GetByIdAsync(int id);
    Task UpdateGoodsReceiptAsync(int id, GoodsReceiptUpdateDto dto);
    Task DeleteAsync(int id);
    Task<PurchaseOrderForGrnDto?> GetForGrnByNumberAsync(
           string purchaseOrderNumber);
}
