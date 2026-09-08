namespace WarehouseManagementSystemApi.DTOs.Department
{
    public class DepartmentListDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
