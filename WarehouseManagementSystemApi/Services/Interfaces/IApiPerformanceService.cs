using WarehouseManagementSystemApi.Models.ApiPerformance;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IApiPerformanceService
    {
        Task ApiPerformanceAddAsync(ApiPerformanceLog model);
    }
}
