using WarehouseManagementSystemApi.DTOs.Salesman;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface ISalesmanService
{
    Task<SalesmanListDto> CreateAsync(SalesmanCreateDto dto);
    Task<IEnumerable<SalesmanListDto>> GetAllAsync();
    Task<SalesmanListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, SalesmanUpdateDto dto);
    Task DeleteAsync(int id);
}
