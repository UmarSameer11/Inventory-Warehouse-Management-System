using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.VehicleType
{
    public class VehicleTypeListViewModel
    {
        public int VehicleTypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public decimal? Capacity { get; set; }
    }
}
