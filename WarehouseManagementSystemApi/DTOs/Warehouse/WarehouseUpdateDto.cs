namespace WarehouseManagementSystemApi.DTOs.Warehouse
{
    public class WarehouseUpdateDto
    {
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Location { get; set; }
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
}
