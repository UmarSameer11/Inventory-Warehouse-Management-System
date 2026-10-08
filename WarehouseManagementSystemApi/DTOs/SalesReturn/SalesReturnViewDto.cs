namespace WarehouseManagementSystemApi.DTOs.SalesReturn
{
    public class SalesReturnViewDto
    {
        public int SalesReturnId { get; set; }
        public string ReturnNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? SalesOrderNumber { get; set; }
        public string? SalesmanName { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; } = null!;
        public string? InspectedByName { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string? Remarks { get; set; }
        public decimal TotalQuantity { get; set; }
        public List<SalesReturnDetailListDto> Details { get; set; } = new();
    }
}
