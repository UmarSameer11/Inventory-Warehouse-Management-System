using AutoMapper;
using WarehouseManagementSystemApi.DTOs.SalesReturn;
using WarehouseManagementSystemApi.Models.SalesReturn;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class SalesReturnService : ISalesReturnService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SalesReturnService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SalesReturnListDto> CreateAsync(
        SalesReturnCreateDto dto)
    {
        var salesReturn = _mapper.Map<SalesReturns>(dto);

        await _unitOfWork.SalesReturns.AddAsync(salesReturn);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SalesReturnListDto>(salesReturn);
    }

    public async Task<IEnumerable<SalesReturnListDto>> GetAllAsync()
    {
        var salesReturns = await _unitOfWork.SalesReturns.GetAllAsync();

        return _mapper.Map<IEnumerable<SalesReturnListDto>>(
            salesReturns);
    }

    public async Task<SalesReturnListDto> GetByIdAsync(int id)
    {
        var salesReturn = await _unitOfWork.SalesReturns.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "SalesReturn not found.");

        return _mapper.Map<SalesReturnListDto>(salesReturn);
    }

    public async Task DeleteAsync(int id)
    {
        var salesReturn = await _unitOfWork.SalesReturns.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "SalesReturn not found.");

        _unitOfWork.SalesReturns.Delete(salesReturn);

        await _unitOfWork.SaveChangesAsync();
    }
}
