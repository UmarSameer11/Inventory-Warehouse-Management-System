
using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Salesman;
using WarehouseManagementSystemApi.Models.Salesman;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class SalesmanService : ISalesmanService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SalesmanService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SalesmanListDto> CreateAsync(
        SalesmanCreateDto dto)
    {
        var salesman = _mapper.Map<Salesmen>(dto);

        await _unitOfWork.Salesmen.AddAsync(salesman);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SalesmanListDto>(salesman);
    }

    public async Task<IEnumerable<SalesmanListDto>> GetAllAsync()
    {
        var salesmen = await _unitOfWork.Salesmen.GetAllAsync();

        return _mapper.Map<IEnumerable<SalesmanListDto>>(salesmen);
    }

    public async Task<SalesmanListDto> GetByIdAsync(int id)
    {
        var salesman = await _unitOfWork.Salesmen.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Salesman not found.");

        return _mapper.Map<SalesmanListDto>(salesman);
    }

    public async Task UpdateAsync(
        int id,
        SalesmanUpdateDto dto)
    {
        var salesman = await _unitOfWork.Salesmen.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Salesman not found.");

        _mapper.Map(dto, salesman);

        _unitOfWork.Salesmen.Update(salesman);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var salesman = await _unitOfWork.Salesmen.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Salesman not found.");

        _unitOfWork.Salesmen.Delete(salesman);

        await _unitOfWork.SaveChangesAsync();
    }
}
