using WarehouseManagementSystemApi.DTOs.Vehicle;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IVehicleService
{
    Task<VehicleListDto> CreateAsync(VehicleCreateDto dto);
    Task<IEnumerable<VehicleListDto>> GetAllAsync();
    Task<VehicleListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, VehicleUpdateDto dto);
    Task DeleteAsync(int id);
}
