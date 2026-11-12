
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemWeb.Application.ViewModels.GoodsReceipt
{
    public class GoodsReceiptUpdateViewModel
    {
        // Header Properties
        

        public int GoodsReceiptId { get; set; }

        [Required]
        public string GoodsReceiptNumber { get; set; } = null!;

        [Required(ErrorMessage = "Please select Purchase Order.")]
        public int PurchaseOrderId { get; set; }
        public string? PurchaseOrderNumber { get; set; } = null!;
        [Required(ErrorMessage = "Please select Warehouse.")]
        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; } = null!;
        public int ReceivedByEmployeeId { get; set; }

        public string? ReceivedByEmployeeName { get; set; } = null!;

        [Required]
        public DateTime ReceiptDate { get; set; }

        public string? SupplierInvoiceNumber { get; set; }
        public string SupplierName { get; set; } = null!;
        public string? Remarks { get; set; }

        [Required]
        public string Status { get; set; } = null!;


        // Dropdown Properties

        public List<SelectListItem> PurchaseOrders { get; set; } = new();

        public List<SelectListItem> Warehouses { get; set; } = new();


        // GRN Product Details

        public List<GoodsReceiptUpdateDetailViewModel> Details { get; set; }
            = new();
    }


    // Individual Product Detail

    public class GoodsReceiptUpdateDetailViewModel
    {
        public int PurchaseOrderDetailId { get; set; }
        public int GoodsReceiptDetailId { get; set; }
        public int ProductId { get; set; }

        public string? ProductName { get; set; } = null!;

        public int BatchId { get; set; }

        public string? BatchNumber { get; set; } = null!;

        public decimal OrderedQuantity { get; set; }

        [Range(typeof(decimal), "0.001", "999999999",
            ErrorMessage = "Received quantity must be greater than zero.")]
        public decimal QuantityReceived { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalAmount => QuantityReceived * UnitPrice;
    }
}
