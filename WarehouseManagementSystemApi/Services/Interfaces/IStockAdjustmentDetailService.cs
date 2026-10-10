using WarehouseManagementSystemApi.DTOs.StockAdjustment;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IStockAdjustmentDetailService
    {
        Task<StockAdjustmentDetailListDto> CreateAsync(StockAdjustmentDetailCreateDto dto);
        Task<IEnumerable<StockAdjustmentDetailListDto>> GetAllAsync();
        Task<StockAdjustmentDetailListDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
