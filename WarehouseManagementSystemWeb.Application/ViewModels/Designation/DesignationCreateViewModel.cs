using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Designation
{
    public class DesignationCreateViewModel
    {
        public string DesignationName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
