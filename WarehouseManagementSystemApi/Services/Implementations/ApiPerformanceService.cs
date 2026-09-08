using WarehouseManagementSystemApi.Models.ApiPerformance;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class ApiPerformanceService : IApiPerformanceService
    {
        private readonly IApiPerformanceRepository _apiPerformanceRepository;

        public ApiPerformanceService(IApiPerformanceRepository apiPerformanceRepository)
        {
            _apiPerformanceRepository = apiPerformanceRepository;
        }
        public async Task ApiPerformanceAddAsync(ApiPerformanceLog model)
        {
            await _apiPerformanceRepository.ApiPerformanceLogAddAsync(model);
        }
    }
}
