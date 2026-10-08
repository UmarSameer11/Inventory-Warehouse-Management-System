using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.Vehicle;

namespace WarehouseManagementSystemApi.Models.VehicleAssignment
{
    public class VehicleAssignments
    {
        [Key]
        public int VehicleAssignmentId { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsActive { get; set; } = true;
        public Vehicles Vehicle { get; set; } = null!;
        public Employees Employee { get; set; } = null!;
    }
}
