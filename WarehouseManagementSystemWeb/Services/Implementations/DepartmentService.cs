using WarehouseManagementSystemWeb.Application.ViewModels.Department;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IApiService _service;

        public DepartmentService(IApiService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<DepartmentViewModel?>> GetAllAsync()
        {
           
        }
    }
}
