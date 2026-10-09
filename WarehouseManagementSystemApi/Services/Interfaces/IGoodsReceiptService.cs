using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IGoodsReceiptService
{
    Task<GoodsReceiptListDto> CreateAsync(GoodsReceiptCreateDto dto);
    Task<IEnumerable<GoodsReceiptListDto>> GetAllAsync();
    Task<GoodsReceiptListDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
