using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptDetailViewModel
    {
        public int GoodsReceiptId { get; set; }

        public string GoodsReceiptNumber { get; set; } = null!;

        public int PurchaseOrderId { get; set; }

        public string PurchaseOrderNumber { get; set; } = null!; 

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; } = null!;

        public int ReceivedByEmployeeId { get; set; }

        public string ReceivedByEmployeeName { get; set; } = null!;

        public DateTime ReceiptDate { get; set; }

        public string? SupplierInvoiceNumber { get; set; }

        public string? Remarks { get; set; }

        public string Status { get; set; } = null!;

        // Detail Properties
        public List<GoodsReceiptDetailItemViewModel> Details { get; set; }
            = new();
        public List<SelectListItem> PurchaseOrders { get; set; } = new();

        public List<SelectListItem> Warehouses { get; set; } = new();
    }
      

    // Individual GRN Product Row
    public class GoodsReceiptDetailItemViewModel
    {
        public int PurchaseOrderDetailId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int BatchId { get; set; }

        public string BatchNumber { get; set; } = null!;

        public decimal OrderedQuantity { get; set; }

        public decimal QuantityReceived { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalAmount => QuantityReceived * UnitPrice;
    }
}
