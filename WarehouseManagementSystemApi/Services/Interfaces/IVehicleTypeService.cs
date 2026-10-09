using WarehouseManagementSystemApi.DTOs.VehicleType;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IVehicleTypeService
{
    Task<VehicleTypeListDto> CreateAsync(VehicleTypeCreateDto dto);
    Task<IEnumerable<VehicleTypeListDto>> GetAllAsync();
    Task<VehicleTypeListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, VehicleTypeUpdateDto dto);
    Task DeleteAsync(int id);
}
