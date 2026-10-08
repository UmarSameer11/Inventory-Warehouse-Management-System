namespace WarehouseManagementSystemApi.DTOs.Vehicle
{
    public class VehicleListDto
    {
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; } = null!;
        public string? RegistrationNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; } = null!;
        public decimal? Capacity { get; set; }
        public string? CurrentDriverName { get; set; }
        public bool IsActive { get; set; }
    }
}
