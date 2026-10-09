using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Supplier;
using WarehouseManagementSystemApi.Models.Supplier;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations;

public class SupplierService : ISupplierService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SupplierService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SupplierListDto> CreateAsync(
        SupplierCreateDto dto)
    {
        var supplier = _mapper.Map<Suppliers>(dto);

        await _unitOfWork.Suppliers.AddAsync(supplier);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SupplierListDto>(supplier);
    }

    public async Task<IEnumerable<SupplierListDto>> GetAllAsync()
    {
        var suppliers = await _unitOfWork.Suppliers.GetAllAsync();

        return _mapper.Map<IEnumerable<SupplierListDto>>(suppliers);
    }

    public async Task<SupplierListDto> GetByIdAsync(int id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Supplier not found.");

        return _mapper.Map<SupplierListDto>(supplier);
    }

    public async Task UpdateAsync(
        int id,
        SupplierUpdateDto dto)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Supplier not found.");

        _mapper.Map(dto, supplier);

        _unitOfWork.Suppliers.Update(supplier);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Supplier not found.");

        _unitOfWork.Suppliers.Delete(supplier);

        await _unitOfWork.SaveChangesAsync();
    }
}
