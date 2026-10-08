namespace WarehouseManagementSystemApi.DTOs.VehicleAssignment
{
    public class VehicleAssignmentListDto
    {
        public int VehicleAssignmentId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; } = null!;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public DateTime AssignmentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsActive { get; set; }
    }
}
