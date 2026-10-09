using WarehouseManagementSystemApi.DTOs.Dispatch;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IDispatchService
{
    Task<DispatchListDto> CreateAsync(DispatchCreateDto dto);
    Task<IEnumerable<DispatchListDto>> GetAllAsync();
    Task<DispatchListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, DispatchUpdateDto dto);
    Task DeleteAsync(int id);
}
