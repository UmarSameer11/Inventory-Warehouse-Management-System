using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Designation;
using WarehouseManagementSystemApi.Models.Designations;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class DesignationService : IDesignationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DesignationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateDesignationAsync(DesignationCreateDto dto)
        {
            var Designation = _mapper.Map<Designation>(dto);

            await _unitOfWork.Designation.AddAsync(Designation);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDesignationAsync(int id)
        {
            var Designation = await _unitOfWork.Designation.GetByIdAsync(id);

            if (Designation == null)
                throw new KeyNotFoundException("Designation not found.");

            _unitOfWork.Designation.Delete(Designation);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<DesignationListDto?> GetDesignationByIdAsync(int id)
        {
            var Designation = await _unitOfWork.Designation.GetByIdAsync(id);

            if (Designation == null)
                throw new KeyNotFoundException("Designation not found.");

            return _mapper.Map<DesignationListDto>(Designation);
        }

        public async Task<IEnumerable<DesignationListDto>> GetDesignationListAsync()
        {
            var Designation = await _unitOfWork.Designation.GetAllAsync();
            if (!Designation.Any())
            {
                throw new KeyNotFoundException("Designation not found");
            }

            return _mapper.Map<IEnumerable<DesignationListDto>>(Designation);
        }

        public async Task UpdateDesignationAsync(int id, DesignationUpdateDto dto)
        {
            var Designation = await _unitOfWork.Designation.GetByIdAsync(id);

            if (Designation == null)
                throw new KeyNotFoundException("Designation not found.");

            _mapper.Map(dto, Designation);

            _unitOfWork.Designation.Update(Designation);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
