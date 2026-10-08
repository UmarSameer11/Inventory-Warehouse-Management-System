namespace WarehouseManagementSystemApi.DTOs.SalesOrder
{
    public class SalesOrderDetailCreateDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
