using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Models.Dispatch
{
    public class DispatchDetails
    {
        [Key]
        public int DispatchDetailId { get; set; }
        public int DispatchId { get; set; }
        public int ProductId { get; set; }
        /// <summary>Required: InventoryStock is tracked per batch, so deduction needs the batch.</summary>
        public int BatchId { get; set; }
        [Precision(18, 3)]
        public decimal Quantity { get; set; }
        public Dispatches Dispatch { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Batches Batch { get; set; } = null!;
    }
}
