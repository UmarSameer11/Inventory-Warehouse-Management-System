using AutoMapper;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Models.PurchaseOrder;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PurchaseOrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PurchaseOrderListDto> CreateAsync(
        PurchaseOrderCreateDto dto)
    {
        var purchaseOrder = _mapper.Map<PurchaseOrders>(dto);

        await _unitOfWork.PurchaseOrders.AddAsync(purchaseOrder);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PurchaseOrderListDto>(purchaseOrder);
    }

    public async Task<IEnumerable<PurchaseOrderListDto>> GetAllAsync()
    {
        var purchaseOrders =
            await _unitOfWork.PurchaseOrders.GetAllAsync();

        return _mapper.Map<IEnumerable<PurchaseOrderListDto>>(
            purchaseOrders);
    }

    public async Task<PurchaseOrderListDto> GetByIdAsync(int id)
    {
        var purchaseOrder =
            await _unitOfWork.PurchaseOrders.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "PurchaseOrder not found.");

        return _mapper.Map<PurchaseOrderListDto>(purchaseOrder);
    }

    public async Task UpdateAsync(
        int id,
        PurchaseOrderUpdateDto dto)
    {
        var purchaseOrder =
            await _unitOfWork.PurchaseOrders.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "PurchaseOrder not found.");

        _mapper.Map(dto, purchaseOrder);

        _unitOfWork.PurchaseOrders.Update(purchaseOrder);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var purchaseOrder =
            await _unitOfWork.PurchaseOrders.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "PurchaseOrder not found.");

        _unitOfWork.PurchaseOrders.Delete(purchaseOrder);

        await _unitOfWork.SaveChangesAsync();
    }
}
