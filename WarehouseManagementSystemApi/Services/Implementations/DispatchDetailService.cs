using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Dispatch;
using WarehouseManagementSystemApi.Models.Dispatch;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class DispatchDetailService : IDispatchDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DispatchDetailService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DispatchDetailListDto> CreateAsync(
            DispatchDetailCreateDto dto)
        {
            var dispatch = _mapper.Map<DispatchDetails>(dto);

            await _unitOfWork.DispatchDetails.AddAsync(dispatch);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DispatchDetailListDto>(dispatch);
        }

        public async Task<IEnumerable<DispatchDetailListDto>> GetAllAsync()
        {
            var dispatches = await _unitOfWork.DispatchDetails.GetAllAsync();

            return _mapper.Map<IEnumerable<DispatchDetailListDto>>(dispatches);
        }

        public async Task<DispatchDetailListDto> GetByIdAsync(int id)
        {
            var dispatch = await _unitOfWork.DispatchDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Dispatch detail not found.");

            return _mapper.Map<DispatchDetailListDto>(dispatch);
        }

        public async Task UpdateAsync(
            int id,
            DispatchDetailListDto dto)
        {
            var dispatch = await _unitOfWork.DispatchDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Dispatch detail not found.");

            _mapper.Map(dto, dispatch);

            _unitOfWork.DispatchDetails.Update(dispatch);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dispatch = await _unitOfWork.DispatchDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Dispatch detail not found.");

            _unitOfWork.DispatchDetails.Delete(dispatch);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
