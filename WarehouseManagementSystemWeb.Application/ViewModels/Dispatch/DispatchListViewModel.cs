using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Dispatch
{
    public class DispatchListViewModel
    {
        public int DispatchId { get; set; }
        public string DispatchNumber { get; set; } = null!;
        public int SalesOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int VehicleId { get; set; }
        public DateTime DispatchDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
