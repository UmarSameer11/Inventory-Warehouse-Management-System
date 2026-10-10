using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.SalesOrderDetail
{
    public class SalesOrderDetailUpdateViewModel
    {
        public int SalesOrderDetailId { get; set; }
        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public List<SelectListItem> SalesOrders { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();
    }
}
