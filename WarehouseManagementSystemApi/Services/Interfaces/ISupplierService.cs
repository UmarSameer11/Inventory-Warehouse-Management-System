using WarehouseManagementSystemApi.DTOs.Supplier;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface ISupplierService
{
    Task<SupplierListDto> CreateAsync(SupplierCreateDto dto);
    Task<IEnumerable<SupplierListDto>> GetAllAsync();
    Task<SupplierListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, SupplierUpdateDto dto);
    Task DeleteAsync(int id);
}
