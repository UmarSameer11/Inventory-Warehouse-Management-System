using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.Warehouse;
using WarehouseManagementSystemApi.Repositories.Interfaces;

namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class WarehouseRepository : GenericRepository<Warehouses>, IWarehouseRepository
    {
        private readonly ApplicationDbContext _context;
        public WarehouseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Warehouses?> GetWarehouseWithEmployeeByIdAsync(int id)
        {
            return await _context.Warehouses
                .Where(w => w.WarehouseId == id)
                .Include(w => w.Employee)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Warehouses?>> GetWarehouseWithEmployeeListAsync()
        {
            return await _context.Warehouses
                .Include(w => w.Employee)
                .ToListAsync();
        }
    }
}
