using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Vehicle;
using WarehouseManagementSystemApi.Models.Vehicle;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class VehicleService : IVehicleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VehicleService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VehicleListDto> CreateAsync(
        VehicleCreateDto dto)
    {
        var vehicle = _mapper.Map<Vehicles>(dto);

        await _unitOfWork.Vehicles.AddAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<VehicleListDto>(vehicle);
    }

    public async Task<IEnumerable<VehicleListDto>> GetAllAsync()
    {
        var vehicles = await _unitOfWork.Vehicles.GetAllAsync();

        return _mapper.Map<IEnumerable<VehicleListDto>>(vehicles);
    }

    public async Task<VehicleListDto> GetByIdAsync(int id)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Vehicle not found.");

        return _mapper.Map<VehicleListDto>(vehicle);
    }

    public async Task UpdateAsync(
        int id,
        VehicleUpdateDto dto)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Vehicle not found.");

        _mapper.Map(dto, vehicle);

        _unitOfWork.Vehicles.Update(vehicle);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Vehicle not found.");

        _unitOfWork.Vehicles.Delete(vehicle);

        await _unitOfWork.SaveChangesAsync();
    }
}
