using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Models.PurchaseOrder
{
    public class PurchaseOrderDetails
    {
        [Key]
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        [Precision(18, 3)]
        public decimal Quantity { get; set; }
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
        public PurchaseOrders PurchaseOrder { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public ICollection<GoodsReceiptDetails> GoodsReceiptDetails { get; set; } = new List<GoodsReceiptDetails>();
    }
}
