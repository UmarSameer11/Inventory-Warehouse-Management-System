using WarehouseManagementSystemApi.DTOs.Department;
using WarehouseManagementSystemApi.DTOs.Designation;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationListDto>> GetDesignationListAsync();
        Task<DesignationListDto?> GetDesignationByIdAsync(int id);
        Task CreateDesignationAsync(DesignationCreateDto dto);
        Task UpdateDesignationAsync(int id, DesignationUpdateDto dto);
        Task DeleteDesignationAsync(int id);
    }
}
