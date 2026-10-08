namespace WarehouseManagementSystemApi.DTOs.GoodsReceipt
{
    public class GoodsReceiptCreateDto
    {
        public int PurchaseOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int ReceivedByEmployeeId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string? SupplierInvoiceNumber { get; set; }
        public string? Remarks { get; set; }
        public List<GoodsReceiptDetailCreateDto> Details { get; set; } = new();
    }
}
