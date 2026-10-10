using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Vehicle
{
    public class VehicleCreateViewModel
    {
        public string VehicleNumber { get; set; } = null!;
        public string? RegistrationNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public decimal? Capacity { get; set; }
        public int? DriverEmployeeId { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem> VehicleTypes { get; set; } = new();

        public List<SelectListItem> Employees { get; set; } = new();
    }
}
