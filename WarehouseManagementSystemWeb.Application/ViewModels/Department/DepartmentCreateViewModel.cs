using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Department
{
    public class DepartmentCreateViewModel
    {
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
