namespace WarehouseManagementSystemApi.DTOs.StockTransfer
{
    public class StockTransferDetailListDto
    {
        public int StockTransferDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int BatchId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public decimal Quantity { get; set; }
    }
}
