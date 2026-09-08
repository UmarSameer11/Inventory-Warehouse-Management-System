using WarehouseManagementSystemApi.DTOs.UnitOfMeasure;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IUnitOfMeasureService
    {
         Task<IEnumerable<UnitOfMeasureListDto>> GetUnitOfMeasureListAsync();
        Task<UnitOfMeasureListDto?> GetUnitOfMeasureByIdAsync(int id);
        Task CreateUnitOfMeasureAsync(UnitOfMeasureCreateDto dto);
        Task UpdateUnitOfMeasureAsync(int id, UnitOfMeasureUpdateDto dto);
        Task DeleteUnitOfMeasureAsync(int id);
    }
}
