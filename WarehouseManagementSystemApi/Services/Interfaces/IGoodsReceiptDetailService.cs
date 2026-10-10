using WarehouseManagementSystemApi.DTOs.GoodsReceipt;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IGoodsReceiptDetailService
    {
        Task<GoodsReceiptDetailCreateDto> CreateAsync(GoodsReceiptDetailCreateDto dto);
        Task<IEnumerable<GoodsReceiptDetailCreateDto>> GetAllAsync();
        Task<GoodsReceiptDetailCreateDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
