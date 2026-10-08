using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Dispatch;
using WarehouseManagementSystemApi.Models.VehicleAssignment;
using WarehouseManagementSystemApi.Models.VehicleType;

namespace WarehouseManagementSystemApi.Models.Vehicle
{
    public class Vehicles
    {
        [Key]
        public int VehicleId { get; set; }
        [MaxLength(30)]
        public string VehicleNumber { get; set; } = null!;
        [MaxLength(50)]
        public string? RegistrationNumber { get; set; }
        public int VehicleTypeId { get; set; }
        /// <summary>Optional override of the type's default capacity.</summary>
        [Precision(18, 2)]
        public decimal? Capacity { get; set; }
        public bool IsActive { get; set; } = true;
        public VehicleTypes VehicleType { get; set; } = null!;
        public ICollection<VehicleAssignments> Assignments { get; set; } = new List<VehicleAssignments>();
        public ICollection<Dispatches> Dispatches { get; set; } = new List<Dispatches>();
    }
}
