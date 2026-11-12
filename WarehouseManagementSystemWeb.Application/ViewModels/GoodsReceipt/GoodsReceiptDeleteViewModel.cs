using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptDeleteViewModel
    {
        // GRN Header
        public int GoodsReceiptId { get; set; }

        public string GoodsReceiptNumber { get; set; } = null!;

        public int PurchaseOrderId { get; set; }

        public string? PurchaseOrderNumber { get; set; }

        public string? SupplierName { get; set; }

        public string? WarehouseName { get; set; }

        public string? ReceivedByEmployeeName { get; set; }

        public DateTime ReceiptDate { get; set; }

        public string? SupplierInvoiceNumber { get; set; }

        public string? Remarks { get; set; }

        public string? Status { get; set; }

        // GRN Details
        public List<GoodsReceiptDetailsDeleteViewModel> Details
        { get; set; } = new();

        public decimal GrandTotal =>
            Details.Sum(x => x.TotalAmount);

        public decimal TotalQuantity =>
            Details.Sum(x => x.QuantityReceived);
    }

    public class GoodsReceiptDetailsDeleteViewModel
    {
        public int GoodsReceiptDetailId { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? BatchNumber { get; set; }

        public decimal OrderedQuantity { get; set; }

        public decimal QuantityReceived { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalAmount =>
            QuantityReceived * UnitPrice;
    }
}
