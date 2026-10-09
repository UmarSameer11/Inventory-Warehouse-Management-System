using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface IPurchaseOrderService
{
    Task<PurchaseOrderListDto> CreateAsync(PurchaseOrderCreateDto dto);
    Task<IEnumerable<PurchaseOrderListDto>> GetAllAsync();
    Task<PurchaseOrderListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, PurchaseOrderUpdateDto dto);
    Task DeleteAsync(int id);
}
