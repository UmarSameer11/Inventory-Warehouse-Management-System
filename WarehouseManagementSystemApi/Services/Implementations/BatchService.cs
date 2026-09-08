using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Batch;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class BatchService : IBatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BatchService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateBatchAsync(BatchCreateDto dto)
        {
            var batch = _mapper.Map<Batches>(dto);

            await _unitOfWork.Batches.AddAsync(batch);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBatchAsync(int id)
        {
            var batch = await _unitOfWork.Batches.GetByIdAsync(id);

            if (batch == null)
                throw new KeyNotFoundException("Batch not found.");

            _unitOfWork.Batches.Delete(batch);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BatchListDto?> GetBatchByIdAsync(int id)
        {
            var batch = await _unitOfWork.Batches.GetBatchWithProductNameById(id);

            if (batch == null)
                throw new KeyNotFoundException("Batch not found.");

            return _mapper.Map<BatchListDto>(batch);
        }

        public async Task<IEnumerable<BatchListDto>> GetBatchListAsync()
        {
            var batch = await _unitOfWork.Batches.GetBatchListWithProductNameAsync();
            if (!batch.Any())
            {
                throw new KeyNotFoundException("Batch not found");
            }

            return _mapper.Map<IEnumerable<BatchListDto>>(batch);
        }

        public async Task UpdateBatchAsync(int id, BatchUpdateDto dto)
        {
            var batch = await _unitOfWork.Batches.GetByIdAsync(id);

            if (batch == null)
                throw new KeyNotFoundException("Batch not found.");

            _mapper.Map(dto, batch);

            _unitOfWork.Batches.Update(batch);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
