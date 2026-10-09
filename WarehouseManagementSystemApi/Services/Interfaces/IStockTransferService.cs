using WarehouseManagementSystemApi.DTOs.StockTransfer;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IStockTransferService
{
    Task<StockTransferListDto> CreateAsync(StockTransferCreateDto dto);
    Task<IEnumerable<StockTransferListDto>> GetAllAsync();
    Task<StockTransferListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, StockTransferUpdateDto dto);
    Task DeleteAsync(int id);
}
