using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.PurchaseOrder;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Models.GoodsReceipt
{
    public class GoodsReceipts
    {
        [Key]
        public int GoodsReceiptId { get; set; }
        [MaxLength(30)]
        public string GoodsReceiptNumber { get; set; } = null!;
        public int PurchaseOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int ReceivedByEmployeeId { get; set; }
        public DateTime ReceivedDate { get; set; }
        [MaxLength(50)]
        public string? SupplierInvoiceNumber { get; set; }
        [MaxLength(500)]
        public string? Remarks { get; set; }
        public PurchaseOrders PurchaseOrder { get; set; } = null!;
        public Warehouses Warehouse { get; set; } = null!;
        public Employees ReceivedByEmployee { get; set; } = null!;
        public ICollection<GoodsReceiptDetails> Details { get; set; } = new List<GoodsReceiptDetails>();
    }
}
