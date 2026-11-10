using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptListViewModel
    {
        public int GoodsReceiptId { get; set; }
        //public string ReceiptNumber { get; set; } = null!;
        public string GoodsReceiptNumber { get; set; } = null!;
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; } = null!;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public DateTime ReceiptDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
