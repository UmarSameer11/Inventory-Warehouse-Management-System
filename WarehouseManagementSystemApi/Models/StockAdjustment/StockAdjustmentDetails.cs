using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Models.StockAdjustment
{
    public class StockAdjustmentDetails
    {
        [Key]
        public int StockAdjustmentDetailId { get; set; }
        public int StockAdjustmentId { get; set; }
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        [Precision(18, 3)]
        public decimal SystemQuantity { get; set; }
        [Precision(18, 3)]
        public decimal PhysicalQuantity { get; set; }
        /// <summary>Computed by SQL Server (Physical - System). Do not set from code.</summary>
        [Precision(18, 3)]
        public decimal Difference { get; private set; }
        public StockAdjustments StockAdjustment { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Batches Batch { get; set; } = null!;
    }
}
