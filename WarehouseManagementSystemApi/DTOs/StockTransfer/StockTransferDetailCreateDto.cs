namespace WarehouseManagementSystemApi.DTOs.StockTransfer
{
    public class StockTransferDetailCreateDto
    {
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        public decimal Quantity { get; set; }
    }
}
