using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Salesman
{
    public class SalesmanCreateViewModel
    {
        public int EmployeeId { get; set; }
        public string SalesmanCode { get; set; } = null!;
        public string? SalesArea { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem> Employees { get; set; } = new();
    }
}
