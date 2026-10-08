using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.ReturnReason;

namespace WarehouseManagementSystemApi.Models.SalesReturn
{
    public class SalesReturnDetails
    {
        [Key]
        public int SalesReturnDetailId { get; set; }
        public int SalesReturnId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        /// <summary>Total quantity brought back by the customer.</summary>
        [Precision(18, 3)]
        public decimal Quantity { get; set; }
        /// <summary>Filled during inspection: goes back to sellable stock.</summary>
        [Precision(18, 3)]
        public decimal GoodQuantity { get; set; }
        /// <summary>Filled during inspection: written off as damage. Good + Damaged must equal Quantity.</summary>
        [Precision(18, 3)]
        public decimal DamagedQuantity { get; set; }
        public int ReturnReasonId { get; set; }
        [MaxLength(300)]
        public string? Remarks { get; set; }
        public SalesReturns SalesReturn { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Batches? Batch { get; set; }
        public ReturnReasons ReturnReason { get; set; } = null!;
    }
}
