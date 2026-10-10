using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Vehicle
{
    public class VehicleListViewModel
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; } = null!;
        public string? RegistrationNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public decimal? Capacity { get; set; }
        public int? DriverEmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
}
