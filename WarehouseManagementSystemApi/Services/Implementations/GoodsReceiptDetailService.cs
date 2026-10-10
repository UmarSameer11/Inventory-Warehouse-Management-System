using AutoMapper;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class GoodsReceiptDetailService : IGoodsReceiptDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GoodsReceiptDetailService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GoodsReceiptDetailListDto> CreateAsync(
            GoodsReceiptCreateDto dto)
        {
            var goodsReceipt = _mapper.Map<GoodsReceiptDetails>(dto);

            await _unitOfWork.GoodsReceiptDetails.AddAsync(goodsReceipt);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GoodsReceiptDetailListDto>(goodsReceipt);
        }

        public async Task<IEnumerable<GoodsReceiptDetailListDto>> GetAllAsync()
        {
            var goodsReceipts =
                await _unitOfWork.GoodsReceiptDetails.GetAllAsync();

            return _mapper.Map<IEnumerable<GoodsReceiptDetailListDto>>(
                goodsReceipts);
        }

        public async Task<GoodsReceiptDetailListDto> GetByIdAsync(int id)
        {
            var goodsReceipt =
                await _unitOfWork.GoodsReceiptDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "GoodsReceipt detail not found.");

            return _mapper.Map<GoodsReceiptDetailListDto>(goodsReceipt);
        }

        public async Task DeleteAsync(int id)
        {
            var goodsReceipt =
                await _unitOfWork.GoodsReceiptDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "GoodsReceipt detail not found.");

            _unitOfWork.GoodsReceiptDetails.Delete(goodsReceipt);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
