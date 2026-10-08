using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.PurchaseOrder;

namespace WarehouseManagementSystemApi.Models.GoodsReceipt
{
    public class GoodsReceiptDetails
    {
        [Key]
        public int GoodsReceiptDetailId { get; set; }
        public int GoodsReceiptId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public int ProductId { get; set; }
        /// <summary>Batch created/selected at the time of receiving.</summary>
        public int BatchId { get; set; }
        [Precision(18, 3)]
        public decimal QuantityReceived { get; set; }
        public GoodsReceipts GoodsReceipt { get; set; } = null!;
        public PurchaseOrderDetails PurchaseOrderDetail { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Batches Batch { get; set; } = null!;
    }
}
