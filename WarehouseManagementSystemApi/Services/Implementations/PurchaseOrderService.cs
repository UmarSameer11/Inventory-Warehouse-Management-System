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

    //public async Task UpdateAsync(
    //int id,
    //PurchaseOrderUpdateDto dto)
    //{
    //    var purchaseOrder =
    //        await _purchase.GetPurchaseOrderForUpdation(id)
    //        ?? throw new KeyNotFoundException(
    //            "Purchase Order not found.");

    //    if (dto.PurchaseOrderId != id)
    //    {
    //        throw new ArgumentException(
    //            "Purchase Order ID does not match.");
    //    }

    //    // Update Purchase Order header
    //    purchaseOrder.SupplierId = dto.SupplierId;
    //    purchaseOrder.OrderDate = dto.OrderDate;
    //    purchaseOrder.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;
    //    purchaseOrder.Remarks = dto.Remarks;

    //    // Update existing details or add new details
    //    foreach (var detailDto in dto.Details)
    //    {
    //        var existingDetail = purchaseOrder.Details
    //            .FirstOrDefault(d =>
    //                d.PurchaseOrderDetailId ==
    //                detailDto.PurchaseOrderDetailId);

    //        if (existingDetail != null)
    //        {
    //            // Update existing detail instead of deleting it
    //            existingDetail.ProductId = detailDto.ProductId;
    //            existingDetail.Quantity = detailDto.Quantity;
    //            existingDetail.UnitPrice = detailDto.UnitPrice;
    //        }
    //        else
    //        {
    //            if (detailDto.PurchaseOrderDetailId != 0)
    //            {
    //                throw new InvalidOperationException(
    //                    $"Purchase Order Detail ID " +
    //                    $"{detailDto.PurchaseOrderDetailId} was not found.");
    //            }

    //            // Add a new detail
    //            purchaseOrder.Details.Add(new PurchaseOrderDetails
    //            {
    //                PurchaseOrderId = purchaseOrder.PurchaseOrderId,
    //                ProductId = detailDto.ProductId,
    //                Quantity = detailDto.Quantity,
    //                UnitPrice = detailDto.UnitPrice
    //            });
    //        }
    //    }

    //    // Do not call Update(purchaseOrder) if this entity is tracked.
    //    await _unitOfWork.SaveChangesAsync();
    //}

    public async Task UpdateAsync(
        int id,
        PurchaseOrderUpdateDto dto)
    {
        var purchaseOrder =
            await _purchase.GetPurchaseOrderForUpdation(id)
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

    public async Task<PurchaseOrderUpdateDto?> GetPoWithProductDetail(int id)
    {
        var purchaseOrder = await _purchase.GetPurchaseOrderForUpdation(id);

        return _mapper.Map<PurchaseOrderUpdateDto>(purchaseOrder);
    }
}

