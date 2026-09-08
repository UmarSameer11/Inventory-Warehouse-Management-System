using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Department;
using WarehouseManagementSystemApi.DTOs.Warehouse;
using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Warehouse;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WarehouseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateWarehouseAsync(WarehouseCreateDto dto)
        {
            var warehouse = _mapper.Map<Warehouses>(dto);

            await _unitOfWork.Warehouses.AddAsync(warehouse);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteWarehouseAsync(int id)
        {
            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id);

            if (warehouse == null)
                throw new KeyNotFoundException("Warehouse not found.");

            _unitOfWork.Warehouses.Delete(warehouse);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<WarehouseListDto?> GetWarehouseByIdAsync(int id)
        {
            var warehouse = await _unitOfWork.Warehouses.GetWarehouseWithEmployeeByIdAsync(id);

            if (warehouse == null)
                throw new KeyNotFoundException("Warehouse not found.");

            return _mapper.Map<WarehouseListDto>(warehouse);
        }

        public async Task<IEnumerable<WarehouseListDto>> GetWarehouseListAsync()
        {
            var warehouse = await _unitOfWork.Warehouses.GetWarehouseWithEmployeeListAsync();
            if (!warehouse.Any())
            {
                throw new KeyNotFoundException("Warehouse not found");
            }

            return _mapper.Map<IEnumerable<WarehouseListDto>>(warehouse);
        }

        public async Task UpdateWarehouseAsync(int id, WarehouseUpdateDto dto)
        {
            var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id);

            if (warehouse == null)
                throw new KeyNotFoundException("Warehouse not found.");

            _mapper.Map(dto, warehouse);

            _unitOfWork.Warehouses.Update(warehouse);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
