namespace WarehouseManagementSystemApi.DTOs.SalesReturn
{
    public class SalesReturnCreateDto
    {
        public int CustomerId { get; set; }
        public int? SalesOrderId { get; set; }
        public int? SalesmanId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Remarks { get; set; }
        public List<SalesReturnDetailCreateDto> Details { get; set; } = new();
    }
}
