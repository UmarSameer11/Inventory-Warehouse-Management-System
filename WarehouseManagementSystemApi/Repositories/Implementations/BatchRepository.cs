using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Repositories.Interfaces;

namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class BatchRepository : GenericRepository<Batches>, IBatchRepository
    {
        private readonly ApplicationDbContext _context;
        public BatchRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Batches?>> GetBatchListWithProductNameAsync()
        {
             return await _context.Batches
                .Include(b => b.Product)
                .ToListAsync();
        }

        public async Task<Batches?> GetBatchWithProductNameById(int id)
        {
            return await _context.Batches
                .Where(b => b.ProductId == id)
                .Include(b => b.Product)
                .FirstOrDefaultAsync();
        }
    }
}
