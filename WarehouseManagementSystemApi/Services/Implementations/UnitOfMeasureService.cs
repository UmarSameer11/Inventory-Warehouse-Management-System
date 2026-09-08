using AutoMapper;
using WarehouseManagementSystemApi.DTOs.UnitOfMeasure;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UnitOfMeasureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateUnitOfMeasureAsync(UnitOfMeasureCreateDto dto)
        {
            var unitOfMeasure = _mapper.Map<UnitOfMeasures>(dto);

            await _unitOfWork.UnitOfMeasures.AddAsync(unitOfMeasure);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUnitOfMeasureAsync(int id)
        {
            var unitOfMeasure = await _unitOfWork.UnitOfMeasures.GetByIdAsync(id);

            if (unitOfMeasure == null)
                throw new KeyNotFoundException("UnitOfMeasure not found.");

            _unitOfWork.UnitOfMeasures.Delete(unitOfMeasure);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UnitOfMeasureListDto?> GetUnitOfMeasureByIdAsync(int id)
        {
            var unitOfMeasure = await _unitOfWork.UnitOfMeasures.GetByIdAsync(id);

            if (unitOfMeasure == null)
                throw new KeyNotFoundException("UnitOfMeasure not found.");

            return _mapper.Map<UnitOfMeasureListDto>(unitOfMeasure);
        }

        public async Task<IEnumerable<UnitOfMeasureListDto>> GetUnitOfMeasureListAsync()
        {
            var unitOfMeasures = await _unitOfWork.UnitOfMeasures.GetAllAsync();
            if (!unitOfMeasures.Any())
            {
                throw new KeyNotFoundException("UnitOfMeasure not found");
            }

            return _mapper.Map<IEnumerable<UnitOfMeasureListDto>>(unitOfMeasures);
        }

        public async Task UpdateUnitOfMeasureAsync(int id, UnitOfMeasureUpdateDto dto)
        {
            var unitOfMeasure = await _unitOfWork.UnitOfMeasures.GetByIdAsync(id);

            if (unitOfMeasure == null)
                throw new KeyNotFoundException("UnitOfMeasure not found.");

            _mapper.Map(dto, unitOfMeasure);

            _unitOfWork.UnitOfMeasures.Update(unitOfMeasure);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}

