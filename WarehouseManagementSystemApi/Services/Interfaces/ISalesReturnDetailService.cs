using WarehouseManagementSystemApi.DTOs.SalesReturn;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface ISalesReturnDetailService
    {
        Task<SalesReturnDetailListDto> CreateAsync(SalesReturnDetailCreateDto dto);
        Task<IEnumerable<SalesReturnDetailListDto>> GetAllAsync();
        Task<SalesReturnDetailListDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
