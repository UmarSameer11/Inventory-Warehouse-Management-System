namespace WarehouseManagementSystemApi.DTOs.Warehouse
{
    public class WarehouseListDto
    {
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Location { get; set; }
        public string EmployeeName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
