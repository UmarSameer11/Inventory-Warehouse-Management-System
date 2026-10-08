using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.MiddleWares.AuditMiddleware;
using WarehouseManagementSystemApi.Data.Configurations;
using WarehouseManagementSystemApi.Models.Customer;
using WarehouseManagementSystemApi.Models.Dispatch;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Models.PurchaseOrder;
using WarehouseManagementSystemApi.Models.ReturnReason;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.Models.SalesReturn;
using WarehouseManagementSystemApi.Models.Salesman;
using WarehouseManagementSystemApi.Models.StockAdjustment;
using WarehouseManagementSystemApi.Models.StockMovement;
using WarehouseManagementSystemApi.Models.StockTransfer;
using WarehouseManagementSystemApi.Models.Supplier;
using WarehouseManagementSystemApi.Models.Vehicle;
using WarehouseManagementSystemApi.Models.VehicleAssignment;
using WarehouseManagementSystemApi.Models.VehicleType;
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
        public DbSet<UserSession> UserSessions { get; set; }

        // ---- Organization
        public DbSet<Salesmen> Salesmen { get; set; }

        // ---- Procurement
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<PurchaseOrders> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public DbSet<GoodsReceipts> GoodsReceipts { get; set; }
        public DbSet<GoodsReceiptDetails> GoodsReceiptDetails { get; set; }

        // ---- Sales & Distribution
        public DbSet<Customers> Customers { get; set; }
        public DbSet<SalesOrders> SalesOrders { get; set; }
        public DbSet<SalesOrderDetails> SalesOrderDetails { get; set; }
        public DbSet<Dispatches> Dispatches { get; set; }
        public DbSet<DispatchDetails> DispatchDetails { get; set; }

        // ---- Returns
        public DbSet<ReturnReasons> ReturnReasons { get; set; }
        public DbSet<SalesReturns> SalesReturns { get; set; }
        public DbSet<SalesReturnDetails> SalesReturnDetails { get; set; }

        // ---- Fleet
        public DbSet<VehicleTypes> VehicleTypes { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<VehicleAssignments> VehicleAssignments { get; set; }

        // ---- Inventory operations
        public DbSet<StockMovements> StockMovements { get; set; }
        public DbSet<StockTransfers> StockTransfers { get; set; }
        public DbSet<StockTransferDetails> StockTransferDetails { get; set; }
        public DbSet<StockAdjustments> StockAdjustments { get; set; }
        public DbSet<StockAdjustmentDetails> StockAdjustmentDetails { get; set; }

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

            // ---- Auth -------------------------------------------------------------------

            // Existing users must stay active after the migration is applied.
            builder.Entity<AppUser>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Entity<UserSession>(e =>
            {
                e.ToTable("UserSessions");
                e.HasKey(s => s.Id);

                e.Property(s => s.DeviceInfo).HasMaxLength(500);
                e.Property(s => s.IpAddress).HasMaxLength(100);
                e.Property(s => s.RevokedReason).HasMaxLength(200);

                e.HasOne(s => s.User)
                    .WithMany()
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(s => new { s.UserId, s.RevokedAtUtc });
            });

            // RefreshToken -> Session is the only cascade path (User -> Session -> RefreshToken),
            // which avoids SQL Server's "multiple cascade paths" error.
            builder.Entity<RefreshToken>(e =>
            {
                e.Property(t => t.TokenHash).IsRequired().HasMaxLength(128);
                e.Property(t => t.ReplacedByTokenHash).HasMaxLength(128);
                e.Property(t => t.CreatedByIp).HasMaxLength(100);
                e.Property(t => t.RevokedReason).HasMaxLength(200);

                e.HasIndex(t => t.TokenHash).IsUnique();

                e.HasOne(t => t.Session)
                    .WithMany(s => s.RefreshTokens)
                    .HasForeignKey(t => t.SessionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ---- Procurement / Sales / Returns / Fleet / Inventory operations ----------
            builder.ConfigureOperationalEntities();
        }

        protected ApplicationDbContext()
        {
        }
    }
}
