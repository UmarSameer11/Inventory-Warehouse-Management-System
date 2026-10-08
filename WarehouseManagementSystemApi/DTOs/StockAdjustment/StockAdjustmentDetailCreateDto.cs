namespace WarehouseManagementSystemApi.DTOs.StockAdjustment
{
    public class StockAdjustmentDetailCreateDto
    {
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        public decimal PhysicalQuantity { get; set; }
    }
}
