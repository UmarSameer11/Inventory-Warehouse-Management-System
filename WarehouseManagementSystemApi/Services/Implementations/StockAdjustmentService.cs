using AutoMapper;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;
using WarehouseManagementSystemApi.Models.StockAdjustment;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class StockAdjustmentService : IStockAdjustmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StockAdjustmentService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<StockAdjustmentListDto> CreateAsync(
        StockAdjustmentCreateDto dto)
    {
        var stockAdjustment = _mapper.Map<StockAdjustments>(dto);

        await _unitOfWork.StockAdjustments.AddAsync(stockAdjustment);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<StockAdjustmentListDto>(stockAdjustment);
    }

    public async Task<IEnumerable<StockAdjustmentListDto>> GetAllAsync()
    {
        var stockAdjustments =
            await _unitOfWork.StockAdjustments.GetAllAsync();

        return _mapper.Map<IEnumerable<StockAdjustmentListDto>>(
            stockAdjustments);
    }

    public async Task<StockAdjustmentListDto> GetByIdAsync(int id)
    {
        var stockAdjustment =
            await _unitOfWork.StockAdjustments.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "StockAdjustment not found.");

        return _mapper.Map<StockAdjustmentListDto>(stockAdjustment);
    }

    public async Task DeleteAsync(int id)
    {
        var stockAdjustment =
            await _unitOfWork.StockAdjustments.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "StockAdjustment not found.");

        _unitOfWork.StockAdjustments.Delete(stockAdjustment);

        await _unitOfWork.SaveChangesAsync();
    }
}
