using AutoMapper;
using WarehouseManagementSystemApi.DTOs.InventoryStock;
using WarehouseManagementSystemApi.DTOs.Warehouse;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Models.Warehouse;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class InventoryStockService : IInventoryStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventoryStockService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateInventoryStockAsync(InventoryStockCreateDto dto)
        {
            var inventoryStock = _mapper.Map<InventoryStocks>(dto);

            await _unitOfWork.InventoryStocks.AddAsync(inventoryStock);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteInventoryStockAsync(int id)
        {
            var inventoryStock = await _unitOfWork.InventoryStocks.GetByIdAsync(id);

            if (inventoryStock == null)
                throw new KeyNotFoundException("InventoryStock not found.");

            _unitOfWork.InventoryStocks.Delete(inventoryStock);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<InventoryStockListDto?> GetInventoryStockByIdAsync(int id)
        {
            var inventoryStock = await _unitOfWork.InventoryStocks.GetInventoryStockWithProdAndWarehouseByIdAsync(id);

            if (inventoryStock == null)
                throw new KeyNotFoundException("InventoryStock not found.");

            return _mapper.Map<InventoryStockListDto>(inventoryStock);
        }

        public async Task<IEnumerable<InventoryStockListDto>> GetInventoryStockListAsync()
        {
            var inventoryStock = await _unitOfWork.InventoryStocks.GetInventoryStockWithProdAndWarehouseListAsync();
            if (!inventoryStock.Any())
            {
                throw new KeyNotFoundException("InventoryStock not found");
            }

            return _mapper.Map<IEnumerable<InventoryStockListDto>>(inventoryStock);
        }

        public async Task UpdateInventoryStockAsync(int id, InventoryStockUpdateDto dto)
        {
            var inventoryStock = await _unitOfWork.InventoryStocks.GetByIdAsync(id);

            if (inventoryStock == null)
                throw new KeyNotFoundException("InventoryStock not found.");

            _mapper.Map(dto, inventoryStock);

            _unitOfWork.InventoryStocks.Update(inventoryStock);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
