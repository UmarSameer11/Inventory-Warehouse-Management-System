using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Models.PurchaseOrder;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;
using static WarehouseManagementSystemApi.DTOs.PurchaseOrder.PurchaseOrderForGrnDto;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class PurchaseOrderService : IPurchaseOrderService 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGoodsReceiptRepository _repository;
    private readonly IPurchaseOrderRepository _purchase;
    private readonly IMapper _mapper;

    public PurchaseOrderService(
        IUnitOfWork unitOfWork,
        IGoodsReceiptRepository repository,
        IPurchaseOrderRepository purchase,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
        _purchase = purchase;
    }

    public async Task<PurchaseOrderListDto> CreateAsync(
        PurchaseOrderCreateDto dto)
    {
        var year = DateTime.Now.Year;
        var lastNum = await _purchase.GetLastPurchaseOrderNumberAsync(year);
        var nextNumber = lastNum + 1;

        var purchaseOrder = _mapper.Map<PurchaseOrders>(dto);

        purchaseOrder.PurchaseOrderNumber = $"PO-{year}-{nextNumber:D3}";
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
            await _purchase.GetPurchaseOrderForUpdation(id)
            ?? throw new KeyNotFoundException(
                "PurchaseOrder not found.");

        var po = _mapper.Map(dto, purchaseOrder);

        _unitOfWork.PurchaseOrders.Update(po);

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

    public async Task<PurchaseOrderUpdateDto?> GetPoWithProductDetail(int id)
    {
        var purchaseOrder = await _purchase.GetPurchaseOrderForUpdation(id);

        return _mapper.Map<PurchaseOrderUpdateDto>(purchaseOrder);
    }

    public async Task<IEnumerable<PurchaseOrderListDto>?> GetPoListWithSuppNameAsync()
    {
        var po = await _purchase.GetPurchaseOrderList();
        return _mapper.Map<IEnumerable<PurchaseOrderListDto>>(po);
    }
}

