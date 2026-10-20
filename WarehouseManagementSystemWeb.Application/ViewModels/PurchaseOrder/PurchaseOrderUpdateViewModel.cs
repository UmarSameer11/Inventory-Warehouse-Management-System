
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder
{
    public class PurchaseOrderUpdateViewModel
    {
        [Required]
        public int PurchaseOrderId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        [Required]
        public string PurchaseOrderNumber { get; set; } = null!;

        public DateTime? ExpectedDeliveryDate { get; set; }

        public string? Remarks { get; set; }

        public List<PurchaseOrderDetailUpdateViewModel> Details { get; set; }
            = new();

        // Dropdown lookups
        public IEnumerable<SelectListItem> Suppliers { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> ProductList { get; set; }
            = new List<SelectListItem>();
    }

    public class PurchaseOrderDetailUpdateViewModel
    {
        // Existing detail record ki ID
        public int PurchaseOrderDetailId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(typeof(decimal), "0.001", "999999999999")]
        public decimal Quantity { get; set; }

        [Range(typeof(decimal), "0.01", "999999999999")]
        public decimal UnitPrice { get; set; }
    }
}
