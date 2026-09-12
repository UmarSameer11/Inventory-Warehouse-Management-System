using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Employee
{
    public class EmployeeListViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public string DepartmentName { get; set; } = null!;
        public string DesignationName { get; set; } = null!;
    }
}
