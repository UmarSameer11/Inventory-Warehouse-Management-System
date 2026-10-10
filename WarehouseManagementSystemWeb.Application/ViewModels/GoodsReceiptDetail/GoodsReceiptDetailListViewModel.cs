using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceiptDetail
{
    public class GoodsReceiptDetailListViewModel
    {
        public int GoodsReceiptDetailId { get; set; }
        public int GoodsReceiptId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
