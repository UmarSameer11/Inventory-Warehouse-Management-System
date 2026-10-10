using AutoMapper;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;
using WarehouseManagementSystemApi.Models.StockAdjustment;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class StockAdjustmentDetailService : IStockAdjustmentDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StockAdjustmentDetailService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StockAdjustmentDetailListDto> CreateAsync(
            StockAdjustmentDetailCreateDto dto)
        {
            var stockAdjustment = _mapper.Map<StockAdjustmentDetails>(dto);

            await _unitOfWork.StockAdjustmentDetails.AddAsync(stockAdjustment);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StockAdjustmentDetailListDto>(stockAdjustment);
        }

        public async Task<IEnumerable<StockAdjustmentDetailListDto>> GetAllAsync()
        {
            var stockAdjustments =
                await _unitOfWork.StockAdjustmentDetails.GetAllAsync();

            return _mapper.Map<IEnumerable<StockAdjustmentDetailListDto>>(
                stockAdjustments);
        }

        public async Task<StockAdjustmentDetailListDto> GetByIdAsync(int id)
        {
            var stockAdjustment =
                await _unitOfWork.StockAdjustmentDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "Stock Adjustment detail not found.");

            return _mapper.Map<StockAdjustmentDetailListDto>(stockAdjustment);
        }

        public async Task DeleteAsync(int id)
        {
            var stockAdjustment =
                await _unitOfWork.StockAdjustmentDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "Stock Adjustment detail not found.");

            _unitOfWork.StockAdjustmentDetails.Delete(stockAdjustment);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
