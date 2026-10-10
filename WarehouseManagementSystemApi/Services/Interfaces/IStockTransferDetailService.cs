using WarehouseManagementSystemApi.DTOs.StockTransfer;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IStockTransferDetailService
    {
        Task<StockTransferDetailListDto> CreateAsync(StockTransferDetailCreateDto dto);
        Task<IEnumerable<StockTransferDetailListDto>> GetAllAsync();
        Task<StockTransferDetailListDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, StockTransferDetailListDto dto);
        Task DeleteAsync(int id);
    }
}
