using AutoMapper;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;
using WarehouseManagementSystemApi.Models.PurchaseOrder;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class PurchaseOrderDetailService : IPurchaseOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseOrderDetailService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PurchaseOrderDetailListDto> CreateAsync(
            PurchaseOrderDetailCreateDto dto)
        {
            var purchaseOrder = _mapper.Map<PurchaseOrderDetails>(dto);

            await _unitOfWork.PurchaseOrderDetails.AddAsync(purchaseOrder);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PurchaseOrderDetailListDto>(purchaseOrder);
        }

        public async Task<IEnumerable<PurchaseOrderDetailListDto>> GetAllAsync()
        {
            var purchaseOrders =
                await _unitOfWork.PurchaseOrderDetails.GetAllAsync();

            return _mapper.Map<IEnumerable<PurchaseOrderDetailListDto>>(
                purchaseOrders);
        }

        public async Task<PurchaseOrderDetailListDto> GetByIdAsync(int id)
        {
            var purchaseOrder =
                await _unitOfWork.PurchaseOrderDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "PurchaseOrder detail not found.");

            return _mapper.Map<PurchaseOrderDetailListDto>(purchaseOrder);
        }

        public async Task UpdateAsync(
            int id,
            PurchaseOrderDetailListDto dto)
        {
            var purchaseOrder =
                await _unitOfWork.PurchaseOrderDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "PurchaseOrder detail not found.");

            _mapper.Map(dto, purchaseOrder);

            _unitOfWork.PurchaseOrderDetails.Update(purchaseOrder);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var purchaseOrder =
                await _unitOfWork.PurchaseOrderDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "PurchaseOrder detail not found.");

            _unitOfWork.PurchaseOrderDetails.Delete(purchaseOrder);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
