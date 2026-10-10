using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrderDetail
{
    public class PurchaseOrderDetailCreateViewModel
    {
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public List<SelectListItem> PurchaseOrders { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();
    }
}
