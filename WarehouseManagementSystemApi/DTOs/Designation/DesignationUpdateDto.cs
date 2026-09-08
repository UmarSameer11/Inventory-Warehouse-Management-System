namespace WarehouseManagementSystemApi.DTOs.Designation
{
    public class DesignationUpdateDto
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
