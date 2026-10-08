namespace WarehouseManagementSystemApi.DTOs.SalesOrder
{
    public class SalesOrderListDto
    {
        public int SalesOrderId { get; set; }
        public string SalesOrderNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public int SalesmanId { get; set; }
        public string SalesmanName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Remarks { get; set; }
        public int ItemCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
