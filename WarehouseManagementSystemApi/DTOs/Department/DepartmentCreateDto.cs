namespace WarehouseManagementSystemApi.DTOs.Department
{
    public class DepartmentCreateDto
    {
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
