using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Common.Enums;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.Models.Vehicle;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Models.Dispatch
{
    public class Dispatches
    {
        [Key]
        public int DispatchId { get; set; }
        [MaxLength(30)]
        public string DispatchNumber { get; set; } = null!;
        public int SalesOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int VehicleId { get; set; }
        /// <summary>Employee who actually drove for this dispatch (history stays even if vehicle assignment changes).</summary>
        public int? DriverEmployeeId { get; set; }
        public DateTime DispatchDate { get; set; }
        public DispatchStatus Status { get; set; } = DispatchStatus.Pending;
        [MaxLength(500)]
        public string? Remarks { get; set; }
        public SalesOrders SalesOrder { get; set; } = null!;
        public Warehouses Warehouse { get; set; } = null!;
        public Vehicles Vehicle { get; set; } = null!;
        public Employees? DriverEmployee { get; set; }
        public ICollection<DispatchDetails> Details { get; set; } = new List<DispatchDetails>();
    }
}
