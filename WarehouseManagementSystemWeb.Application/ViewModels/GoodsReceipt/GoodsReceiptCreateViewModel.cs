
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceiptDetail;

namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptCreateViewModel
    {
        public string GoodsReceiptNumber { get; set; }

        public int PurchaseOrderId { get; set; }

        public int ReceivedByEmployeeId { get; set; }

        public int WarehouseId { get; set; }

        public DateTime ReceiptDate { get; set; } = DateTime.Today;

        public string Status { get; set; } = "Pending";

        // Multiple GRN detail rows
        public List<GoodsReceiptDetailCreateViewModel> Details { get; set; }
            = new()
            {
                new GoodsReceiptDetailCreateViewModel()
            };

        // Dropdowns
        public List<SelectListItem> PurchaseOrders { get; set; } = new();

        public List<SelectListItem> Warehouses { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();

        public List<SelectListItem> Batches { get; set; } = new();
    }
}

