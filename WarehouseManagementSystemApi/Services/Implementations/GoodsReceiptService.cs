using AutoMapper;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class GoodsReceiptService : IGoodsReceiptService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GoodsReceiptService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GoodsReceiptListDto> CreateAsync(
        GoodsReceiptCreateDto dto)
    {
        var goodsReceipt = _mapper.Map<GoodsReceipts>(dto);

        await _unitOfWork.GoodsReceipts.AddAsync(goodsReceipt);
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
}
