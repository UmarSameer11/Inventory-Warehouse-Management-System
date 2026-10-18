using AutoMapper;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class GoodsReceiptService : IGoodsReceiptService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGoodsReceiptRepository _repository;
    private readonly IMapper _mapper;

    public GoodsReceiptService(
        IUnitOfWork unitOfWork,
        IGoodsReceiptRepository repository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GoodsReceiptListDto> CreateAsync(
        GoodsReceiptCreateDto dto)
    {
        var goodsReceipt = _mapper.Map<GoodsReceipts>(dto);

        await _unitOfWork.GoodsReceipts.AddAsync(goodsReceipt);

        foreach (var detail in goodsReceipt.Details)
        {
            System.Diagnostics.Debug.WriteLine(
                $"PurchaseOrderDetailId: {detail.PurchaseOrderDetailId}, " +
                $"ProductId: {detail.ProductId}");
        }
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<GoodsReceiptListDto>(goodsReceipt);
    }

    public async Task<IEnumerable<GoodsReceiptListDto>> GetAllAsync()
    {
        var goodsReceipts =
            await _unitOfWork.GoodsReceipts.GetAllAsync();

        return _mapper.Map<IEnumerable<GoodsReceiptListDto>>(
            goodsReceipts);
    }

    public async Task<GoodsReceiptListDto> GetByIdAsync(int id)
    {
        var goodsReceipt =
            await _unitOfWork.GoodsReceipts.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "GoodsReceipt not found.");

        return _mapper.Map<GoodsReceiptListDto>(goodsReceipt);
    }

    public async Task DeleteAsync(int id)
    {
        var goodsReceipt =
            await _unitOfWork.GoodsReceipts.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "GoodsReceipt not found.");

        _unitOfWork.GoodsReceipts.Delete(goodsReceipt);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PurchaseOrderForGrnDto?> GetForGrnByNumberAsync(
     string purchaseOrderNumber)
    {
        if (string.IsNullOrWhiteSpace(purchaseOrderNumber))
        {
            throw new ArgumentException(
                "Purchase order number is required.",
                nameof(purchaseOrderNumber));
        }

        if (_repository == null)
        {
            throw new InvalidOperationException(
                "GoodsReceipt repository is not initialized.");
        }

        return await _repository.GetForGrnByNumberAsync(
            purchaseOrderNumber.Trim());
    }
}
