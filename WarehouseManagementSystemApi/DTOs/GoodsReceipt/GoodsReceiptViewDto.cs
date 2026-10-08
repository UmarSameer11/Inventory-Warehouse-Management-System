namespace WarehouseManagementSystemApi.DTOs.GoodsReceipt
{
    public class GoodsReceiptViewDto
    {
        public int GoodsReceiptId { get; set; }
        public string GoodsReceiptNumber { get; set; } = null!;
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; } = null!;
        public string SupplierName { get; set; } = null!;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public string ReceivedByName { get; set; } = null!;
        public DateTime ReceivedDate { get; set; }
        public string? SupplierInvoiceNumber { get; set; }
        public string? Remarks { get; set; }
        public decimal TotalQuantity { get; set; }
        public List<GoodsReceiptDetailListDto> Details { get; set; } = new();
    }
}
