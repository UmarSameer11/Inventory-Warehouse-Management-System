namespace WarehouseManagementSystemApi.DTOs.VehicleAssignment
{
    public class VehicleAssignmentCreateDto
    {
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AssignmentDate { get; set; }
    }
}
