using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustment
{
    public class StockAdjustmentCreateViewModel
    {
        public string AdjustmentNumber { get; set; } = null!;
        public int WarehouseId { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Reason { get; set; } = null!;
        public int EmployeeId { get; set; }

        public List<SelectListItem> Warehouses { get; set; } = new();

        public List<SelectListItem> Employees { get; set; } = new();
    }
}
