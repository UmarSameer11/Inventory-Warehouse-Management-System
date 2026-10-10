using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Warehouse
{
    public class WarehouseListViewModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Location { get; set; }
        public int? ManagerEmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
}
