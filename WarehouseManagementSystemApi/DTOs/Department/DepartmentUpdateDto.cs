namespace WarehouseManagementSystemApi.DTOs.Department
{
    public class DepartmentUpdateDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
