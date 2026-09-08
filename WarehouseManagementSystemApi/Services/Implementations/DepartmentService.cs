using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Department;
using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateDepartmentAsync(DepartmentCreateDto dto)
        {
            var Department = _mapper.Map<Departments>(dto);

            await _unitOfWork.Departments.AddAsync(Department);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var Department = await _unitOfWork.Departments.GetByIdAsync(id);

            if (Department == null)
                throw new KeyNotFoundException("Department not found.");

            _unitOfWork.Departments.Delete(Department);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DepartmentListDto?> GetDepartmentByIdAsync(int id)
        {
            var Department = await _unitOfWork.Departments.GetByIdAsync(id);

            if (Department == null)
                throw new KeyNotFoundException("Department not found.");

            return _mapper.Map<DepartmentListDto>(Department);
        }

        public async Task<IEnumerable<DepartmentListDto>> GetDepartmentListAsync()
        {
            var Departments = await _unitOfWork.Departments.GetAllAsync();
            if (!Departments.Any())
            {
                throw new KeyNotFoundException("Department not found");
            }

            return _mapper.Map<IEnumerable<DepartmentListDto>>(Departments);
        }

        public async Task UpdateDepartmentAsync(int id, DepartmentUpdateDto dto)
        {
            var Department = await _unitOfWork.Departments.GetByIdAsync(id);

            if (Department == null)
                throw new KeyNotFoundException("Department not found.");

            _mapper.Map(dto, Department);

            _unitOfWork.Departments.Update(Department);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

