namespace WarehouseManagementSystemApi.DTOs.StockAdjustment
{
    public class StockAdjustmentCreateDto
    {
        public int WarehouseId { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Reason { get; set; } = null!;
        public int EmployeeId { get; set; }
        public List<StockAdjustmentDetailCreateDto> Details { get; set; } = new();
    }
}
