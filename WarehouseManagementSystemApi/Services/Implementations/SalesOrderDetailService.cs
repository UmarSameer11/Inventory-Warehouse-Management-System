using AutoMapper;
using WarehouseManagementSystemApi.DTOs.SalesOrder;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SalesOrderDetailService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SalesOrderDetailListDto> CreateAsync(
            SalesOrderDetailCreateDto dto)
        {
            var salesOrder = _mapper.Map<SalesOrderDetails>(dto);

            await _unitOfWork.SalesOrderDetails.AddAsync(salesOrder);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SalesOrderDetailListDto>(salesOrder);
        }

        public async Task<IEnumerable<SalesOrderDetailListDto>> GetAllAsync()
        {
            var salesOrders = await _unitOfWork.SalesOrderDetails.GetAllAsync();

            return _mapper.Map<IEnumerable<SalesOrderDetailListDto>>(salesOrders);
        }

        public async Task<SalesOrderDetailListDto> GetByIdAsync(int id)
        {
            var salesOrder = await _unitOfWork.SalesOrderDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "SalesOrder detail not found.");

            return _mapper.Map<SalesOrderDetailListDto>(salesOrder);
        }

        public async Task UpdateAsync(
            int id,
            SalesOrderDetailListDto dto)
        {
            var salesOrder = await _unitOfWork.SalesOrderDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "SalesOrder detail not found.");

            _mapper.Map(dto, salesOrder);

            _unitOfWork.SalesOrderDetails.Update(salesOrder);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var salesOrder = await _unitOfWork.SalesOrderDetails.GetByIdAsync(id)
                ?? throw new KeyNotFoundException(
                    "SalesOrder detail not found.");

            _unitOfWork.SalesOrderDetails.Delete(salesOrder);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
