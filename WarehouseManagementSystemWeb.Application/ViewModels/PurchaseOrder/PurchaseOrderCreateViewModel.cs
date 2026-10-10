using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder
{
    public class PurchaseOrderCreateViewModel
    {
        public string PurchaseOrderNumber { get; set; } = null!;
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;

        public List<SelectListItem> Suppliers { get; set; } = new();
    }
}
