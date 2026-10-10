using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Salesman
{
    public class SalesmanListViewModel
    {
        public int SalesmanId { get; set; }
        public int EmployeeId { get; set; }
        public string SalesmanCode { get; set; } = null!;
        public string? SalesArea { get; set; }
        public bool IsActive { get; set; }
    }
}
