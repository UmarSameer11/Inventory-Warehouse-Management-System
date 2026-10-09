using WarehouseManagementSystemApi.DTOs.StockAdjustment;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IStockAdjustmentService
{
    Task<StockAdjustmentListDto> CreateAsync(StockAdjustmentCreateDto dto);
    Task<IEnumerable<StockAdjustmentListDto>> GetAllAsync();
    Task<StockAdjustmentListDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
