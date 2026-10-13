namespace WarehouseManagementSystemApi.DTOs.GoodsReceipt
{
    public class GoodsReceiptCreateDto
    {
        public int PurchaseOrderId { get; set; }
        public string GoodsReceiptNumber { get; set; } 
        public int WarehouseId { get; set; }
        public int ReceivedByEmployeeId { get; set; } 
        public DateTime ReceivedDate { get; set; }
        public string? SupplierInvoiceNumber { get; set; } = "1234";
        public string? Remarks { get; set; } = "GRN";
        public List<GoodsReceiptDetailCreateDto> Details { get; set; } = new();
    } 
}
