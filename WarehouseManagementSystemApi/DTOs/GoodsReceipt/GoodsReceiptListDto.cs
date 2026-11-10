
namespace WarehouseManagementSystemApi.Models.GoodsReceipt
{
    public class GoodsReceiptListDto
    {
        public int GoodsReceiptId { get; set; }

        public string GoodsReceiptNumber { get; set; } = null!;

        public int PurchaseOrderId { get; set; }

        public string PurchaseOrderNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string SupplierName { get; set; } = null!;

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; } = null!;

        public int ReceivedByEmployeeId { get; set; }

        public string ReceivedByEmployeeName { get; set; } = null!;

        public DateTime ReceivedDate { get; set; }

        public string? SupplierInvoiceNumber { get; set; }

        public string? Remarks { get; set; }

        public List<GoodsReceiptDetailListDto> Details { get; set; }
            = new List<GoodsReceiptDetailListDto>();
    }

    // GRN Child Details
    public class GoodsReceiptDetailListDto
    {
        public int GoodsReceiptDetailId { get; set; }

        public int PurchaseOrderDetailId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }

        public int BatchId { get; set; }

        public string BatchNumber { get; set; } = null!;
        public decimal OrderedQuantity { get; set; }

        public decimal QuantityReceived { get; set; }
    }
}
