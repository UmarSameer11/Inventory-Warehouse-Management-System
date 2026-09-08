using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.MiddleWares.AuditMiddleware;
using WarehouseManagementSystemApi.Models.ApiPerformance;
using WarehouseManagementSystemApi.Models.AuditLog;
using WarehouseManagementSystemApi.Models.Auth;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Designations;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Models.ProductCategory;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Warehouses> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<UnitOfMeasures> UnitOfMeasures { get; set; }
        public DbSet<InventoryStocks> InventoryStocks { get; set; }
        public DbSet<Batches> Batches { get; set; }
        public DbSet<ApiPerformanceLog> ApiPerformanceLogs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<InventoryStocks>()
                .HasOne(x => x.Warehouse)
                .WithMany(x => x.InventoryStocks)
                .HasForeignKey(x => x.WarehouseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<InventoryStocks>()
                .HasOne(x => x.Product)
                .WithMany(x => x.InventoryStocks)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<InventoryStocks>()
                .HasOne(x => x.Batch)
                .WithMany(x => x.InventoryStocks)
                .HasForeignKey(x => x.BatchId)
                .OnDelete(DeleteBehavior.NoAction);


        }


        protected ApplicationDbContext()
        {
        }
    }
}
