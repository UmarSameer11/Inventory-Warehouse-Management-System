using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Warehouse
{
    public class WarehouseCreateViewModel
    {
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Location { get; set; }
        public int? ManagerEmployeeId { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem> Employees { get; set; } = new();
    }
}
