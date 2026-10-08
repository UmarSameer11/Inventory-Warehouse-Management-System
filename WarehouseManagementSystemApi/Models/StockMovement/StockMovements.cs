using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Common.Enums;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Models.StockMovement
{
    /// <summary>Immutable inventory ledger. Never update or delete rows; post a correcting movement instead.</summary>
    public class StockMovements
    {
        [Key]
        public long StockMovementId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        /// <summary>Signed: positive = stock in, negative = stock out.</summary>
        [Precision(18, 3)]
        public decimal Quantity { get; set; }
        public StockMovementType MovementType { get; set; }
        public DateTime MovementDate { get; set; }
        [MaxLength(50)]
        public string? ReferenceNumber { get; set; }
        [MaxLength(300)]
        public string? Remarks { get; set; }
        public int? EmployeeId { get; set; }
        public Warehouses Warehouse { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Batches? Batch { get; set; }
        public Employees? Employee { get; set; }
    }
}
