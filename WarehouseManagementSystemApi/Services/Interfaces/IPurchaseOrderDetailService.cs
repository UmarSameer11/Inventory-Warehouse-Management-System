using WarehouseManagementSystemApi.DTOs.PurchaseOrder;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IPurchaseOrderDetailService
    {
        Task<PurchaseOrderDetailListDto> CreateAsync(PurchaseOrderDetailCreateDto dto);
        Task<IEnumerable<PurchaseOrderDetailListDto>> GetAllAsync();
        Task<PurchaseOrderDetailListDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, PurchaseOrderDetailListDto dto);
        Task DeleteAsync(int id);
    }
}
