namespace WarehouseManagementSystemApi.DTOs.StockTransfer
{
    public class StockTransferCreateDto
    {
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public DateTime TransferDate { get; set; }
        public string? Remarks { get; set; }
        public List<StockTransferDetailCreateDto> Details { get; set; } = new();
    }
}
