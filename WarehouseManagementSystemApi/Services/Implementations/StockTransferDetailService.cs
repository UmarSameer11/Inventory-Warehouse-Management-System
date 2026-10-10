using AutoMapper;
using WarehouseManagementSystemApi.DTOs.StockTransfer;
using WarehouseManagementSystemApi.Models.StockTransfer;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class StockTransferDetailService : IStockTransferDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StockTransferDetailService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StockTransferDetailListDto> CreateAsync(
            StockTransferDetailCreateDto dto)
        {
            var stockTransfer = _mapper.Map<StockTransferDetails>(dto);

            await _unitOfWork.StockTransferDetails.AddAsync(stockTransfer);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StockTransferDetailListDto>(stockTransfer);
        }

        public async Task<IEnumerable<StockTransferDetailListDto>> GetAllAsync()
        {
            var stockTransfers =
                await _unitOfWork.StockTransferDetails.GetAllAsync();

            return _mapper.Map<IEnumerable<StockTransferDetailListDto>>(
                stockTransfers);
        }

        public async Task<StockTransferDetailListDto> GetByIdAsync(int id)
        {
            var stockTransfer =
                await _unitOfWork.StockTransferDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "Stock Transfer detail not found.");

            return _mapper.Map<StockTransferDetailListDto>(stockTransfer);
        }

        public async Task UpdateAsync(
            int id,
            StockTransferDetailListDto dto)
        {
            var stockTransfer =
                await _unitOfWork.StockTransferDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "Stock Transfer detail not found.");

            _mapper.Map(dto, stockTransfer);

            _unitOfWork.StockTransferDetails.Update(stockTransfer);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stockTransfer =
                await _unitOfWork.StockTransferDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "Stock Transfer detail not found.");

            _unitOfWork.StockTransferDetails.Delete(stockTransfer);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
