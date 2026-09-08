namespace WarehouseManagementSystemApi.DTOs.Designation
{
    public class DesignationCreateDto
    {
        public string DesignationName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
