using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Repositories.Interfaces;

namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class EmployeeRepository : GenericRepository<Employees>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employees?>> GetEmployeeWithDepartmentAndDesignationAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .ToListAsync();
        }

        public async Task<Employees?> GetEmployeeWithDepartmentAndDesignationByIdAsync(int id)
        {
            return await _context.Employees
                .Where(e => e.EmployeeId == id)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync();
        }
    }
}
