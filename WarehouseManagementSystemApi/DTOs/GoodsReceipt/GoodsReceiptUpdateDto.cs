namespace WarehouseManagementSystemApi.DTOs.GoodsReceipt
{
    public class GoodsReceiptUpdateDto
    {
        public int PurchaseOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int ReceivedByEmployeeId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string? SupplierInvoiceNumber { get; set; } = "1234";
        public string? Remarks { get; set; } = "GRN";
        public List<GoodsReceiptDetailUpdateDto> Details { get; set; } = new();
    }
}
