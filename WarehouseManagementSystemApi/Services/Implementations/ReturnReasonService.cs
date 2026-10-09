
using AutoMapper;
using WarehouseManagementSystemApi.DTOs.ReturnReason;
using WarehouseManagementSystemApi.Models.ReturnReason;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class ReturnReasonService : IReturnReasonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReturnReasonService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReturnReasonListDto> CreateAsync(
        ReturnReasonCreateDto dto)
    {
        var returnReason = _mapper.Map<ReturnReasons>(dto);

        await _unitOfWork.ReturnReasons.AddAsync(returnReason);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ReturnReasonListDto>(returnReason);
    }

    public async Task<IEnumerable<ReturnReasonListDto>> GetAllAsync()
    {
        var returnReasons = await _unitOfWork.ReturnReasons.GetAllAsync();

        return _mapper.Map<IEnumerable<ReturnReasonListDto>>(
            returnReasons);
    }

    public async Task<ReturnReasonListDto> GetByIdAsync(int id)
    {
        var returnReason = await _unitOfWork.ReturnReasons.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "ReturnReason not found.");

        return _mapper.Map<ReturnReasonListDto>(returnReason);
    }

    public async Task UpdateAsync(
        int id,
        ReturnReasonUpdateDto dto)
    {
        var returnReason = await _unitOfWork.ReturnReasons.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "ReturnReason not found.");

        _mapper.Map(dto, returnReason);

        _unitOfWork.ReturnReasons.Update(returnReason);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var returnReason = await _unitOfWork.ReturnReasons.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "ReturnReason not found.");

        _unitOfWork.ReturnReasons.Delete(returnReason);

        await _unitOfWork.SaveChangesAsync();
    }
}
