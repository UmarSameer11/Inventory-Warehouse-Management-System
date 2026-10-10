using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockTransferDetail
{
    public class StockTransferDetailListViewModel
    {
        public int StockTransferDetailId { get; set; }
        public int StockTransferId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }
    }
}
