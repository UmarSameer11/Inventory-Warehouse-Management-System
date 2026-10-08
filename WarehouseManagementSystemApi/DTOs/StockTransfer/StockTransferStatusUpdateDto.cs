using WarehouseManagementSystemApi.Common.Enums;

namespace WarehouseManagementSystemApi.DTOs.StockTransfer
{
    public class StockTransferStatusUpdateDto
    {
        public int StockTransferId { get; set; }
        public StockTransferStatus Status { get; set; }
    }
}
