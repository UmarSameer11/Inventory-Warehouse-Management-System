using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.ApiPerformance;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using static WarehouseManagementSystemApi.Models.ApiPerformance.ApiPerformanceLog;

namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class ApiPerformanceRepository : GenericRepository<ApiPerformanceLog>, IApiPerformanceRepository
    {
        private readonly ApplicationDbContext _context;
        public ApiPerformanceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task ApiPerformanceLogAddAsync(ApiPerformanceLog model)
        {
            await _context.ApiPerformanceLogs.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
