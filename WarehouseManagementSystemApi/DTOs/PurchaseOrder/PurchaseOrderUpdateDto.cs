namespace WarehouseManagementSystemApi.DTOs.PurchaseOrder
{
    public class PurchaseOrderUpdateDto
    {
        public int PurchaseOrderId { get; set; }
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string? Remarks { get; set; }
        public List<PurchaseOrderDetailCreateDto> Details { get; set; } = new();
    }
}
