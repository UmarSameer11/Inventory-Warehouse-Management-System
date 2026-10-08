namespace WarehouseManagementSystemApi.DTOs.StockAdjustment
{
    public class StockAdjustmentDetailListDto
    {
        public int StockAdjustmentDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int BatchId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public decimal SystemQuantity { get; set; }
        public decimal PhysicalQuantity { get; set; }
        public decimal Difference { get; set; }
    }
}
