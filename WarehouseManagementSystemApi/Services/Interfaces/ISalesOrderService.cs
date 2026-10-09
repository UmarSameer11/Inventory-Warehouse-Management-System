using WarehouseManagementSystemApi.DTOs.SalesOrder;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface ISalesOrderService
{
    Task<SalesOrderListDto> CreateAsync(SalesOrderCreateDto dto);
    Task<IEnumerable<SalesOrderListDto>> GetAllAsync();
    Task<SalesOrderListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, SalesOrderUpdateDto dto);
    Task DeleteAsync(int id);
}
