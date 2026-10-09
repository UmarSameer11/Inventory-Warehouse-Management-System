using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Dispatch;
using WarehouseManagementSystemApi.Models.Dispatch;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class DispatchService : IDispatchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DispatchService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DispatchListDto> CreateAsync(
        DispatchCreateDto dto)
    {
        var dispatch = _mapper.Map<Dispatches>(dto);

        await _unitOfWork.Dispatches.AddAsync(dispatch);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<DispatchListDto>(dispatch);
    }

    public async Task<IEnumerable<DispatchListDto>> GetAllAsync()
    {
        var dispatches = await _unitOfWork.Dispatches.GetAllAsync();

        return _mapper.Map<IEnumerable<DispatchListDto>>(dispatches);
    }

    public async Task<DispatchListDto> GetByIdAsync(int id)
    {
        var dispatch = await _unitOfWork.Dispatches.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Dispatch not found.");

        return _mapper.Map<DispatchListDto>(dispatch);
    }

    public async Task UpdateAsync(
        int id,
        DispatchUpdateDto dto)
    {
        var dispatch = await _unitOfWork.Dispatches.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Dispatch not found.");

        _mapper.Map(dto, dispatch);

        _unitOfWork.Dispatches.Update(dispatch);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var dispatch = await _unitOfWork.Dispatches.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Dispatch not found.");

        _unitOfWork.Dispatches.Delete(dispatch);

        await _unitOfWork.SaveChangesAsync();
    }
}
