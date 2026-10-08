namespace WarehouseManagementSystemApi.DTOs.Dispatch
{
    public class DispatchViewDto
    {
        public int DispatchId { get; set; }
        public string DispatchNumber { get; set; } = null!;
        public int SalesOrderId { get; set; }
        public string SalesOrderNumber { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; } = null!;
        public string? DriverName { get; set; }
        public DateTime DispatchDate { get; set; }
        public string Status { get; set; } = null!;
        public string? Remarks { get; set; }
        public decimal TotalQuantity { get; set; }
        public List<DispatchDetailListDto> Details { get; set; } = new();
    }
}
