namespace WarehouseManagementSystemApi.DTOs.Dispatch
{
    public class DispatchCreateDto
    {
        public int SalesOrderId { get; set; }
        public int WarehouseId { get; set; }
        public int VehicleId { get; set; }
        public int? DriverEmployeeId { get; set; }
        public DateTime DispatchDate { get; set; }
        public string? Remarks { get; set; }
        public List<DispatchDetailCreateDto> Details { get; set; } = new();
    }
}
