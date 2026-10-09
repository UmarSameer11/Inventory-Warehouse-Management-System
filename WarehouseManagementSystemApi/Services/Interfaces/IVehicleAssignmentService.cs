using WarehouseManagementSystemApi.DTOs.VehicleAssignment;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IVehicleAssignmentService
{
    Task<VehicleAssignmentListDto> CreateAsync(VehicleAssignmentCreateDto dto);
    Task<IEnumerable<VehicleAssignmentListDto>> GetAllAsync();
    Task<VehicleAssignmentListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, VehicleAssignmentUpdateDto dto);
    Task DeleteAsync(int id);
}
