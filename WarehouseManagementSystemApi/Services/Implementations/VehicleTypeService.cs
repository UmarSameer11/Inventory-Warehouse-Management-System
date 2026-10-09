using AutoMapper;
using WarehouseManagementSystemApi.DTOs.VehicleType;
using WarehouseManagementSystemApi.Models.VehicleType;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class VehicleTypeService : IVehicleTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VehicleTypeService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VehicleTypeListDto> CreateAsync(
        VehicleTypeCreateDto dto)
    {
        var vehicleType = _mapper.Map<VehicleTypes>(dto);

        await _unitOfWork.VehicleTypes.AddAsync(vehicleType);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<VehicleTypeListDto>(vehicleType);
    }

    public async Task<IEnumerable<VehicleTypeListDto>> GetAllAsync()
    {
        var vehicleTypes = await _unitOfWork.VehicleTypes.GetAllAsync();

        return _mapper.Map<IEnumerable<VehicleTypeListDto>>(vehicleTypes);
    }

    public async Task<VehicleTypeListDto> GetByIdAsync(int id)
    {
        var vehicleType = await _unitOfWork.VehicleTypes.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("VehicleType not found.");

        return _mapper.Map<VehicleTypeListDto>(vehicleType);
    }

    public async Task UpdateAsync(
        int id,
        VehicleTypeUpdateDto dto)
    {
        var vehicleType = await _unitOfWork.VehicleTypes.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("VehicleType not found.");

        _mapper.Map(dto, vehicleType);

        _unitOfWork.VehicleTypes.Update(vehicleType);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleType = await _unitOfWork.VehicleTypes.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("VehicleType not found.");

        _unitOfWork.VehicleTypes.Delete(vehicleType);

        await _unitOfWork.SaveChangesAsync();
    }
}
