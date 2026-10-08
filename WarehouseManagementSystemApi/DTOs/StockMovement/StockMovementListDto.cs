namespace WarehouseManagementSystemApi.DTOs.StockMovement
{
    public class StockMovementListDto
    {
        public long StockMovementId { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? BatchId { get; set; }
        public string? BatchNumber { get; set; }
        public decimal Quantity { get; set; }
        public string MovementType { get; set; } = null!;
        public DateTime MovementDate { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? Remarks { get; set; }
        public string? EmployeeName { get; set; }
    }
}
