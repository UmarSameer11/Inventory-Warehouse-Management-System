namespace WarehouseManagementSystemApi.DTOs.StockAdjustment
{
    public class StockAdjustmentListDto
    {
        public int StockAdjustmentId { get; set; }
        public string AdjustmentNumber { get; set; } = null!;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public DateTime AdjustmentDate { get; set; }
        public string Reason { get; set; } = null!;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public int ItemCount { get; set; }
    }
}
