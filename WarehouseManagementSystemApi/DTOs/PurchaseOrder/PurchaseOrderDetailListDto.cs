namespace WarehouseManagementSystemApi.DTOs.PurchaseOrder
{
    public class PurchaseOrderDetailListDto
    {
        public int PurchaseOrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string UnitName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal ReceivedQuantity { get; set; }
    }
}
