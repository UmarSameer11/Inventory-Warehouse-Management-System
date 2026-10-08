namespace WarehouseManagementSystemApi.DTOs.PurchaseOrder
{
    public class PurchaseOrderListDto
    {
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; } = null!;
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Remarks { get; set; }
        public int ItemCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
