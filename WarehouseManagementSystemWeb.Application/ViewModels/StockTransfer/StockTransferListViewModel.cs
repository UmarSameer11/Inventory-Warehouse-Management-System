using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockTransfer
{
    public class StockTransferListViewModel
    {
        public int StockTransferId { get; set; }
        public string TransferNumber { get; set; } = null!;
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public DateTime TransferDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
