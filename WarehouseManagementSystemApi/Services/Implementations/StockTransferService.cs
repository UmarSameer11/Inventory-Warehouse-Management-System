using AutoMapper;
using WarehouseManagementSystemApi.DTOs.StockTransfer;
using WarehouseManagementSystemApi.Models.StockTransfer;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class StockTransferService : IStockTransferService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StockTransferService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<StockTransferListDto> CreateAsync(
        StockTransferCreateDto dto)
    {
        var stockTransfer = _mapper.Map<StockTransfers>(dto);

        await _unitOfWork.StockTransfers.AddAsync(stockTransfer);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<StockTransferListDto>(stockTransfer);
    }

    public async Task<IEnumerable<StockTransferListDto>> GetAllAsync()
    {
        var stockTransfers =
            await _unitOfWork.StockTransfers.GetAllAsync();

        return _mapper.Map<IEnumerable<StockTransferListDto>>(
            stockTransfers);
    }

    public async Task<StockTransferListDto> GetByIdAsync(int id)
    {
        var stockTransfer =
            await _unitOfWork.StockTransfers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "StockTransfer not found.");

        return _mapper.Map<StockTransferListDto>(stockTransfer);
    }

    public async Task UpdateAsync(
        int id,
        StockTransferUpdateDto dto)
    {
        var stockTransfer =
            await _unitOfWork.StockTransfers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "StockTransfer not found.");

        _mapper.Map(dto, stockTransfer);

        _unitOfWork.StockTransfers.Update(stockTransfer);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var stockTransfer =
            await _unitOfWork.StockTransfers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "StockTransfer not found.");

        _unitOfWork.StockTransfers.Delete(stockTransfer);

        await _unitOfWork.SaveChangesAsync();
    }
}
