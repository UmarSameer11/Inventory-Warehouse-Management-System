using WarehouseManagementSystemApi.DTOs.ReturnReason;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IReturnReasonService
{
    Task<ReturnReasonListDto> CreateAsync(ReturnReasonCreateDto dto);
    Task<IEnumerable<ReturnReasonListDto>> GetAllAsync();
    Task<ReturnReasonListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, ReturnReasonUpdateDto dto);
    Task DeleteAsync(int id);
}
