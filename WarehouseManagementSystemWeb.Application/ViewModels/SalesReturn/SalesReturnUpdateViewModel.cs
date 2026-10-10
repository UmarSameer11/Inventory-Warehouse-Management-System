using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.SalesReturn
{
    public class SalesReturnUpdateViewModel
    {
        public int SalesReturnId { get; set; }
        public string ReturnNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public int? SalesOrderId { get; set; }
        public int? SalesmanId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Remarks { get; set; }

        public List<SelectListItem> Customers { get; set; } = new();

        public List<SelectListItem> SalesOrders { get; set; } = new();

        public List<SelectListItem> Salesmen { get; set; } = new();
    }
}
