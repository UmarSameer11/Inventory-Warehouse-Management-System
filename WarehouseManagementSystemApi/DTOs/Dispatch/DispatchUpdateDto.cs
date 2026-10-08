namespace WarehouseManagementSystemApi.DTOs.Dispatch
{
    public class DispatchUpdateDto
    {
        public int DispatchId { get; set; }
        public int SalesOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int VehicleId { get; set; }
        public int? DriverEmployeeId { get; set; }
        public DateTime DispatchDate { get; set; }
        public string? Remarks { get; set; }
        public List<DispatchDetailCreateDto> Details { get; set; } = new();
    }
}
