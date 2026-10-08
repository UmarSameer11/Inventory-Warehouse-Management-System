namespace WarehouseManagementSystemApi.DTOs.GoodsReceipt
{
    public class GoodsReceiptDetailListDto
    {
        public int GoodsReceiptDetailId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int BatchId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public DateTime ManufacturingDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal QuantityReceived { get; set; }
    }
}
