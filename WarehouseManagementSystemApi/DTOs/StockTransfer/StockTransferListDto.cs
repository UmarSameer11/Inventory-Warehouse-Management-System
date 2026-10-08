namespace WarehouseManagementSystemApi.DTOs.StockTransfer
{
    public class StockTransferListDto
    {
        public int StockTransferId { get; set; }
        public string TransferNumber { get; set; } = null!;
        public int FromWarehouseId { get; set; }
        public string FromWarehouseName { get; set; } = null!;
        public int ToWarehouseId { get; set; }
        public string ToWarehouseName { get; set; } = null!;
        public DateTime TransferDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Remarks { get; set; }
        public decimal TotalQuantity { get; set; }
    }
}
