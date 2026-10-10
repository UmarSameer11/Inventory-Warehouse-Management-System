using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Dispatch
{
    public class DispatchUpdateViewModel
    {
        public int DispatchId { get; set; }
        public string DispatchNumber { get; set; } = null!;
        public int SalesOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int VehicleId { get; set; }
        public DateTime DispatchDate { get; set; }
        public string Status { get; set; } = null!;

        public List<SelectListItem> SalesOrders { get; set; } = new();

        public List<SelectListItem> Warehouses { get; set; } = new();

        public List<SelectListItem> Vehicles { get; set; } = new();
    }
}
