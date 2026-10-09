using WarehouseManagementSystemApi.DTOs.SalesReturn;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface ISalesReturnService
{
    Task<SalesReturnListDto> CreateAsync(SalesReturnCreateDto dto);
    Task<IEnumerable<SalesReturnListDto>> GetAllAsync();
    Task<SalesReturnListDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
