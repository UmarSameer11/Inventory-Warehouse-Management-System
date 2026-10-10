using AutoMapper;
using WarehouseManagementSystemApi.DTOs.SalesReturn;
using WarehouseManagementSystemApi.Models.SalesReturn;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class SalesReturnDetailService : ISalesReturnDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SalesReturnDetailService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SalesReturnDetailListDto> CreateAsync(
            SalesReturnDetailCreateDto dto)
        {
            var salesReturn = _mapper.Map<SalesReturnDetails>(dto);

            await _unitOfWork.SalesReturnDetails.AddAsync(salesReturn);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SalesReturnDetailListDto>(salesReturn);
        }

        public async Task<IEnumerable<SalesReturnDetailListDto>> GetAllAsync()
        {
            var salesReturns = await _unitOfWork.SalesReturnDetails.GetAllAsync();

            return _mapper.Map<IEnumerable<SalesReturnDetailListDto>>(
                salesReturns);
        }

        public async Task<SalesReturnDetailListDto> GetByIdAsync(int id)
        {
            var salesReturn = await _unitOfWork.SalesReturnDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "SalesReturn detail not found.");

            return _mapper.Map<SalesReturnDetailListDto>(salesReturn);
        }

        public async Task DeleteAsync(int id)
        {
            var salesReturn = await _unitOfWork.SalesReturnDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "SalesReturn detail not found.");

            _unitOfWork.SalesReturnDetails.Delete(salesReturn);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
