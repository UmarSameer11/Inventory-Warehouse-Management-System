using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.VehicleAssignment
{
    public class VehicleAssignmentUpdateViewModel
    {
        public int VehicleAssignmentId { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem> Vehicles { get; set; } = new();

        public List<SelectListItem> Employees { get; set; } = new();
    }
}
