using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Common.Enums;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Models.StockTransfer
{
    public class StockTransfers
    {
        [Key]
        public int StockTransferId { get; set; }
        [MaxLength(30)]
        public string TransferNumber { get; set; } = null!;
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public DateTime TransferDate { get; set; }
        public StockTransferStatus Status { get; set; } = StockTransferStatus.Draft;
        [MaxLength(500)]
        public string? Remarks { get; set; }
        public Warehouses FromWarehouse { get; set; } = null!;
        public Warehouses ToWarehouse { get; set; } = null!;
        public ICollection<StockTransferDetails> Details { get; set; } = new List<StockTransferDetails>();
    }
}
