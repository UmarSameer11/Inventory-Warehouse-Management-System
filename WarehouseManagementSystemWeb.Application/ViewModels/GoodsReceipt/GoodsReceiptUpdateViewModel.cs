using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptUpdateViewModel
    {
        public int GoodsReceiptId { get; set; }
        public string ReceiptNumber { get; set; } = null!;
        public int PurchaseOrderId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string Status { get; set; } = null!;

        public List<SelectListItem> PurchaseOrders { get; set; } = new();

        public List<SelectListItem> Warehouses { get; set; } = new();
    }
}
