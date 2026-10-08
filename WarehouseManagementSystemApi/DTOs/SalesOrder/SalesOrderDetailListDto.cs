namespace WarehouseManagementSystemApi.DTOs.SalesOrder
{
    public class SalesOrderDetailListDto
    {
        public int SalesOrderDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        public decimal DispatchedQuantity { get; set; }
    }
}
