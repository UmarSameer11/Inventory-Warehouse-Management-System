using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Employee;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateEmployeeAsync(EmployeeCreateDto dto)
        {
            var employee = _mapper.Map<Employees>(dto);

            await _unitOfWork.Employees.AddAsync(employee);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            _unitOfWork.Employees.Delete(employee);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<EmployeeListDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetEmployeeWithDepartmentAndDesignationByIdAsync(id);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            return _mapper.Map<EmployeeListDto>(employee);
        }

        public async Task<IEnumerable<EmployeeListDto>> GetEmployeeListAsync()
        {
            var employees = await _unitOfWork.Employees.GetEmployeeWithDepartmentAndDesignationAsync();
            if (!employees.Any())
            {
                throw new KeyNotFoundException("Employee not found");
            }

            var res = _mapper.Map<IEnumerable<EmployeeListDto>>(employees);

            return res;
        }
      
        public async Task UpdateEmployeeAsync(int id, EmployeeUpdateDto dto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            _mapper.Map(dto, employee);

            _unitOfWork.Employees.Update(employee);

            await _unitOfWork.SaveChangesAsync();
        }

        //public async Task<IEnumerable<EmployeeListDto?>> GetEmployeeWithDepartmentAndDesignationAsync()
        //{
        //    await _repository.GetEmployeeWithDepartmentAndDesignationAsync();
        //}

    }
}
