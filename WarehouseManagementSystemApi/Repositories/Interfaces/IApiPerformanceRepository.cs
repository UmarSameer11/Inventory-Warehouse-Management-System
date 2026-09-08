using WarehouseManagementSystemApi.Models.ApiPerformance;
using static WarehouseManagementSystemApi.Models.ApiPerformance.ApiPerformanceLog;

namespace WarehouseManagementSystemApi.Repositories.Interfaces
{
    public interface IApiPerformanceRepository : IGenericRepository<ApiPerformanceLog>
    {
        Task ApiPerformanceLogAddAsync(ApiPerformanceLog model);
    }
}
