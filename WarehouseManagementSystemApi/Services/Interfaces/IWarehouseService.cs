using WarehouseManagementSystemApi.DTOs.Department;
using WarehouseManagementSystemApi.DTOs.Warehouse;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseListDto>> GetWarehouseListAsync();
        Task<WarehouseListDto?> GetWarehouseByIdAsync(int id);
        Task CreateWarehouseAsync(WarehouseCreateDto dto);
        Task UpdateWarehouseAsync(int id, WarehouseUpdateDto dto);
        Task DeleteWarehouseAsync(int id);
    }
}
