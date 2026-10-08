namespace WarehouseManagementSystemApi.DTOs.GoodsReceipt
{
    public class GoodsReceiptDetailCreateDto
    {
        public int PurchaseOrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public DateTime ManufacturingDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal QuantityReceived { get; set; }
    }
}
