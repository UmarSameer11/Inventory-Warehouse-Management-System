namespace WarehouseManagementSystemApi.DTOs.Designation
{
    public class DesignationListDto
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
