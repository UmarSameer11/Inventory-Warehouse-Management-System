using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceiptDetail
{
    public class GoodsReceiptDetailCreateViewModel
    {
        public int GoodsReceiptId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public List<SelectListItem> GoodsReceipts { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();

        public List<SelectListItem> Batches { get; set; } = new();
    }
}
