using WarehouseManagementSystemApi.DTOs.SalesOrder;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface ISalesOrderDetailService
    {
        Task<SalesOrderDetailListDto> CreateAsync(SalesOrderDetailCreateDto dto);
        Task<IEnumerable<SalesOrderDetailListDto>> GetAllAsync();
        Task<SalesOrderDetailListDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, SalesOrderDetailListDto dto);
        Task DeleteAsync(int id);
    }
}
