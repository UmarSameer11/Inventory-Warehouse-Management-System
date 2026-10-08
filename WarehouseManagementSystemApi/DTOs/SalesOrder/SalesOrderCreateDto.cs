namespace WarehouseManagementSystemApi.DTOs.SalesOrder
{
    public class SalesOrderCreateDto
    {
        public int CustomerId { get; set; }
        public int SalesmanId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Remarks { get; set; }
        public List<SalesOrderDetailCreateDto> Details { get; set; } = new();
    }
}
