using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;


using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder
{
    public class PurchaseOrderCreateViewModel
    {
        public int PurchaseOrderId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Today;

        public DateTime? ExpectedDeliveryDate { get; set; }

        public string? Remarks { get; set; }

        public List<PurchaseOrderDetailCreateViewModel> Details { get; set; }
            = new();

        // Dropdown lookup
        public IEnumerable<SelectListItem> Suppliers { get; set; }
            = new List<SelectListItem>();
        public IEnumerable<SelectListItem> ProductList { get; set; }
            = new List<SelectListItem>();
    }

    public class PurchaseOrderDetailCreateViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Range(0.001, double.MaxValue)]
        public decimal Quantity { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }
  
        //public string PurchaseOrderNumber { get; set; } = null!;
        //public int SupplierId { get; set; }
        //public DateTime OrderDate { get; set; }
        //public string Status { get; set; } = null!;

        //public List<SelectListItem> Suppliers { get; set; } = new();
    }
}
