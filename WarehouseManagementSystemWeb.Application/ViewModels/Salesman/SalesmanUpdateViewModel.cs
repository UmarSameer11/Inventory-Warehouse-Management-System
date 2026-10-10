using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Salesman
{
    public class SalesmanUpdateViewModel
    {
        public int SalesmanId { get; set; }
        public int EmployeeId { get; set; }
        public string SalesmanCode { get; set; } = null!;
        public string? SalesArea { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem> Employees { get; set; } = new();
    }
}
