namespace WarehouseManagementSystemApi.DTOs.Vehicle
{
    public class VehicleCreateDto
    {
        public string VehicleNumber { get; set; } = null!;
        public string? RegistrationNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public decimal? Capacity { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
