namespace WarehouseManagementSystemApi.DTOs.VehicleAssignment
{
    public class VehicleAssignmentUpdateDto
    {
        public int VehicleAssignmentId { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsActive { get; set; }
    }
}
