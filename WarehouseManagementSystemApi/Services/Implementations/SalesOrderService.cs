using AutoMapper;
using WarehouseManagementSystemApi.DTOs.SalesOrder;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class SalesOrderService : ISalesOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SalesOrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SalesOrderListDto> CreateAsync(
        SalesOrderCreateDto dto)
    {
        var salesOrder = _mapper.Map<SalesOrders>(dto);

        await _unitOfWork.SalesOrders.AddAsync(salesOrder);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SalesOrderListDto>(salesOrder);
    }

    public async Task<IEnumerable<SalesOrderListDto>> GetAllAsync()
    {
        var salesOrders = await _unitOfWork.SalesOrders.GetAllAsync();

        return _mapper.Map<IEnumerable<SalesOrderListDto>>(salesOrders);
    }

    public async Task<SalesOrderListDto> GetByIdAsync(int id)
    {
        var salesOrder = await _unitOfWork.SalesOrders.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "SalesOrder not found.");

        return _mapper.Map<SalesOrderListDto>(salesOrder);
    }

    public async Task UpdateAsync(
        int id,
        SalesOrderUpdateDto dto)
    {
        var salesOrder = await _unitOfWork.SalesOrders.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "SalesOrder not found.");

        _mapper.Map(dto, salesOrder);

        _unitOfWork.SalesOrders.Update(salesOrder);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var salesOrder = await _unitOfWork.SalesOrders.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "SalesOrder not found.");

        _unitOfWork.SalesOrders.Delete(salesOrder);

        await _unitOfWork.SaveChangesAsync();
    }
}
