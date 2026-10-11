using WarehouseManagementSystemApi.DTOs.GoodsReceipt;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IGoodsReceiptDetailService
    {
        Task<GoodsReceiptDetailListDto> CreateAsync(GoodsReceiptDetailCreateDto dto);
        Task<IEnumerable<GoodsReceiptDetailListDto>> GetAllAsync();
        Task<GoodsReceiptDetailListDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
