using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Common.Enums;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Customer;
using WarehouseManagementSystemApi.Models.Dispatch;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Models.Products;
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
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Data.Configurations
{
    /// <summary>
    /// Fluent configuration for Procurement, Sales, Returns, Fleet and Inventory-operation entities.
    /// Call from ApplicationDbContext.OnModelCreating.
    /// </summary>
    public static class OperationalModelBuilderExtensions
    {
        public static void ConfigureOperationalEntities(this ModelBuilder b)
        {
            // 1) Safe default for every FK of the new tables: NO cascade delete.
            //    (Avoids SQL Server "multiple cascade paths" errors and accidental data loss.)
            var newTypes = new[]
            {
                typeof(Salesmen), typeof(Suppliers), typeof(PurchaseOrders), typeof(PurchaseOrderDetails),
                typeof(GoodsReceipts), typeof(GoodsReceiptDetails), typeof(Customers), typeof(SalesOrders),
                typeof(SalesOrderDetails), typeof(Dispatches), typeof(DispatchDetails), typeof(ReturnReasons),
                typeof(SalesReturns), typeof(SalesReturnDetails), typeof(VehicleTypes), typeof(Vehicles),
                typeof(VehicleAssignments), typeof(StockMovements), typeof(StockTransfers),
                typeof(StockTransferDetails), typeof(StockAdjustments), typeof(StockAdjustmentDetails)
            };

            foreach (var entityType in b.Model.GetEntityTypes().Where(t => newTypes.Contains(t.ClrType)))
                foreach (var fk in entityType.GetForeignKeys())
                    fk.DeleteBehavior = DeleteBehavior.Restrict;

            // 2) Header -> Detail: deleting a header removes its lines.
            b.Entity<PurchaseOrderDetails>().HasOne(x => x.PurchaseOrder).WithMany(x => x.Details)
                .HasForeignKey(x => x.PurchaseOrderId).OnDelete(DeleteBehavior.Cascade);
            b.Entity<GoodsReceiptDetails>().HasOne(x => x.GoodsReceipt).WithMany(x => x.Details)
                .HasForeignKey(x => x.GoodsReceiptId).OnDelete(DeleteBehavior.Cascade);
            b.Entity<SalesOrderDetails>().HasOne(x => x.SalesOrder).WithMany(x => x.Details)
                .HasForeignKey(x => x.SalesOrderId).OnDelete(DeleteBehavior.Cascade);
            b.Entity<DispatchDetails>().HasOne(x => x.Dispatch).WithMany(x => x.Details)
                .HasForeignKey(x => x.DispatchId).OnDelete(DeleteBehavior.Cascade);
            b.Entity<SalesReturnDetails>().HasOne(x => x.SalesReturn).WithMany(x => x.Details)
                .HasForeignKey(x => x.SalesReturnId).OnDelete(DeleteBehavior.Cascade);
            b.Entity<StockTransferDetails>().HasOne(x => x.StockTransfer).WithMany(x => x.Details)
                .HasForeignKey(x => x.StockTransferId).OnDelete(DeleteBehavior.Cascade);
            b.Entity<StockAdjustmentDetails>().HasOne(x => x.StockAdjustment).WithMany(x => x.Details)
                .HasForeignKey(x => x.StockAdjustmentId).OnDelete(DeleteBehavior.Cascade);

            // 3) Relationships whose other side has no collection navigation.
            b.Entity<Salesmen>().HasOne(x => x.Employee).WithOne()
                .HasForeignKey<Salesmen>(x => x.EmployeeId);                       // Employee 1 --- 0..1 Salesman
            b.Entity<GoodsReceipts>().HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId);
            b.Entity<GoodsReceipts>().HasOne(x => x.ReceivedByEmployee).WithMany().HasForeignKey(x => x.ReceivedByEmployeeId);
            b.Entity<GoodsReceiptDetails>().HasOne(x => x.PurchaseOrderDetail).WithMany(x => x.GoodsReceiptDetails)
                .HasForeignKey(x => x.PurchaseOrderDetailId);
            b.Entity<GoodsReceiptDetails>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            b.Entity<GoodsReceiptDetails>().HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId);
            b.Entity<PurchaseOrderDetails>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            b.Entity<SalesOrderDetails>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            b.Entity<Dispatches>().HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId);
            b.Entity<Dispatches>().HasOne(x => x.DriverEmployee).WithMany().HasForeignKey(x => x.DriverEmployeeId);
            b.Entity<DispatchDetails>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            b.Entity<DispatchDetails>().HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId);
            b.Entity<SalesReturns>().HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId);
            b.Entity<SalesReturns>().HasOne(x => x.SalesOrder).WithMany().HasForeignKey(x => x.SalesOrderId);
            b.Entity<SalesReturns>().HasOne(x => x.InspectedByEmployee).WithMany().HasForeignKey(x => x.InspectedByEmployeeId);
            b.Entity<SalesReturnDetails>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            b.Entity<SalesReturnDetails>().HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId);
            b.Entity<VehicleAssignments>().HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId);
            b.Entity<StockMovements>().HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId);
            b.Entity<StockMovements>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            b.Entity<StockMovements>().HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId);
            b.Entity<StockMovements>().HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId);
            b.Entity<StockTransfers>().HasOne(x => x.FromWarehouse).WithMany().HasForeignKey(x => x.FromWarehouseId);
            b.Entity<StockTransfers>().HasOne(x => x.ToWarehouse).WithMany().HasForeignKey(x => x.ToWarehouseId);
            b.Entity<StockTransferDetails>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            b.Entity<StockTransferDetails>().HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId);
            b.Entity<StockAdjustments>().HasOne(x => x.Warehouse).WithMany().HasForeignKey(x => x.WarehouseId);
            b.Entity<StockAdjustments>().HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId);
            b.Entity<StockAdjustmentDetails>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            b.Entity<StockAdjustmentDetails>().HasOne(x => x.Batch).WithMany().HasForeignKey(x => x.BatchId);

            // 4) Unique business keys.
            b.Entity<Salesmen>().HasIndex(x => x.SalesmanCode).IsUnique();
            b.Entity<Suppliers>().HasIndex(x => x.SupplierCode).IsUnique();
            b.Entity<Customers>().HasIndex(x => x.CustomerCode).IsUnique();
            b.Entity<PurchaseOrders>().HasIndex(x => x.PurchaseOrderNumber).IsUnique();
            b.Entity<GoodsReceipts>().HasIndex(x => x.GoodsReceiptNumber).IsUnique();
            b.Entity<SalesOrders>().HasIndex(x => x.SalesOrderNumber).IsUnique();
            b.Entity<Dispatches>().HasIndex(x => x.DispatchNumber).IsUnique();
            b.Entity<SalesReturns>().HasIndex(x => x.ReturnNumber).IsUnique();
            b.Entity<StockTransfers>().HasIndex(x => x.TransferNumber).IsUnique();
            b.Entity<StockAdjustments>().HasIndex(x => x.AdjustmentNumber).IsUnique();
            b.Entity<Vehicles>().HasIndex(x => x.VehicleNumber).IsUnique();
            b.Entity<VehicleTypes>().HasIndex(x => x.TypeName).IsUnique();
            b.Entity<ReturnReasons>().HasIndex(x => x.ReasonName).IsUnique();

            // 5) Query-performance indexes for the ledger.
            b.Entity<StockMovements>().HasIndex(x => new { x.WarehouseId, x.ProductId, x.BatchId });
            b.Entity<StockMovements>().HasIndex(x => x.MovementDate);
            b.Entity<StockMovements>().HasIndex(x => x.ReferenceNumber);

            // 6) Enums stored as readable strings.
            b.Entity<PurchaseOrders>().Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
            b.Entity<SalesOrders>().Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
            b.Entity<Dispatches>().Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
            b.Entity<StockTransfers>().Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
            b.Entity<SalesReturns>().Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
            b.Entity<StockMovements>().Property(x => x.MovementType).HasConversion<string>().HasMaxLength(30);

            // 7) Computed column: Difference = Physical - System.
            b.Entity<StockAdjustmentDetails>().Property(x => x.Difference)
                .HasComputedColumnSql("[PhysicalQuantity] - [SystemQuantity]", stored: true);
        }
    }
}
