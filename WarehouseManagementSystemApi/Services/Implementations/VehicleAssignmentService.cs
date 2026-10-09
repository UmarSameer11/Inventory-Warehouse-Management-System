
using AutoMapper;
using WarehouseManagementSystemApi.DTOs.VehicleAssignment;
using WarehouseManagementSystemApi.Models.VehicleAssignment;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class VehicleAssignmentService : IVehicleAssignmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VehicleAssignmentService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VehicleAssignmentListDto> CreateAsync(
        VehicleAssignmentCreateDto dto)
    {
        var vehicleAssignment = _mapper.Map<VehicleAssignments>(dto);

        await _unitOfWork.VehicleAssignments.AddAsync(vehicleAssignment);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<VehicleAssignmentListDto>(vehicleAssignment);
    }

    public async Task<IEnumerable<VehicleAssignmentListDto>> GetAllAsync()
    {
        var vehicleAssignments =
            await _unitOfWork.VehicleAssignments.GetAllAsync();

        return _mapper.Map<IEnumerable<VehicleAssignmentListDto>>(
            vehicleAssignments);
    }

    public async Task<VehicleAssignmentListDto> GetByIdAsync(int id)
    {
        var vehicleAssignment =
            await _unitOfWork.VehicleAssignments.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "VehicleAssignment not found.");

        return _mapper.Map<VehicleAssignmentListDto>(vehicleAssignment);
    }

    public async Task UpdateAsync(
        int id,
        VehicleAssignmentUpdateDto dto)
    {
        var vehicleAssignment =
            await _unitOfWork.VehicleAssignments.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "VehicleAssignment not found.");

        _mapper.Map(dto, vehicleAssignment);

        _unitOfWork.VehicleAssignments.Update(vehicleAssignment);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleAssignment =
            await _unitOfWork.VehicleAssignments.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                "VehicleAssignment not found.");

        _unitOfWork.VehicleAssignments.Delete(vehicleAssignment);

        await _unitOfWork.SaveChangesAsync();
    }
}
