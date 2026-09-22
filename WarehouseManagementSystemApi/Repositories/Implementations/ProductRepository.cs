using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Repositories.Interfaces;

namespace WarehouseManagementSystemApi.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Product?>> GetProductListWithCategoryAndUnitAsync()
        {
            return await _context.Products
                .Include(u => u.UnitOfMeasure)
                .Include(c => c.ProductCategory)
                .ToListAsync();
        }

        public async Task<Product?> GetProductWithCategoryAndUnitByIdAsync(int id)
        {
            return await _context.Products
                .Where(a => a.ProductId == id)
                .Include(u => u.UnitOfMeasure)
                .Include(c => c.ProductCategory)
                .FirstOrDefaultAsync();
        }
    }
}
