using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Customer;
using WarehouseManagementSystemApi.Models.Customer;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomerListDto> CreateAsync(CustomerCreateDto dto)
    {
        var customer = _mapper.Map<Customers>(dto);

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CustomerListDto>(customer);
    }

    public async Task<IEnumerable<CustomerListDto>> GetAllAsync()
    {
        var customers = await _unitOfWork.Customers.GetAllAsync();

        return _mapper.Map<IEnumerable<CustomerListDto>>(customers);
    }

    public async Task<CustomerListDto> GetByIdAsync(int id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Customer not found.");

        return _mapper.Map<CustomerListDto>(customer);
    }

    public async Task UpdateAsync(int id, CustomerUpdateDto dto)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Customer not found.");

        _mapper.Map(dto, customer);

        _unitOfWork.Customers.Update(customer);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Customer not found.");

        _unitOfWork.Customers.Delete(customer);

        await _unitOfWork.SaveChangesAsync();
    }
}
