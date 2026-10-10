using WarehouseManagementSystemApi.DTOs.Dispatch;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IDispatchDetailService
    {
        Task<DispatchDetailListDto> CreateAsync(DispatchDetailCreateDto dto);
        Task<IEnumerable<DispatchDetailListDto>> GetAllAsync();
        Task<DispatchDetailListDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, DispatchDetailListDto dto);
        Task DeleteAsync(int id);
    }
}
