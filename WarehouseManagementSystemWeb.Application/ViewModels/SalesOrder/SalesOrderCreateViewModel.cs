using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.SalesOrder
{
    public class SalesOrderCreateViewModel
    {
        public string SalesOrderNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public int SalesmanId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;

        public List<SelectListItem> Customers { get; set; } = new();

        public List<SelectListItem> Salesmen { get; set; } = new();
    }
}
