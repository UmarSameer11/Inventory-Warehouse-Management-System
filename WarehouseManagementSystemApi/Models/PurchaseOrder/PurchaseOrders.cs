using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Common.Enums;
using WarehouseManagementSystemApi.Models.GoodsReceipt;
using WarehouseManagementSystemApi.Models.Supplier;

namespace WarehouseManagementSystemApi.Models.PurchaseOrder
{
    public class PurchaseOrders
    {
        [Key]
        public int PurchaseOrderId { get; set; }
        [MaxLength(30)]
        public string PurchaseOrderNumber { get; set; } = null!;
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;
        [MaxLength(500)]
        public string? Remarks { get; set; }
        public Suppliers Supplier { get; set; } = null!;
        public ICollection<PurchaseOrderDetails> Details { get; set; } = new List<PurchaseOrderDetails>();
        public ICollection<GoodsReceipts> GoodsReceipts { get; set; } = new List<GoodsReceipts>();
    }
}
