using WarehouseManagementSystemApi.DTOs.Customer;
namespace WarehouseManagementSystemApi.Services.Interfaces;
public interface ICustomerService
{
    Task<CustomerListDto> CreateAsync(CustomerCreateDto dto);
    Task<IEnumerable<CustomerListDto>> GetAllAsync();
    Task<CustomerListDto> GetByIdAsync(int id);
    Task UpdateAsync(int id, CustomerUpdateDto dto);
    Task DeleteAsync(int id);
}
