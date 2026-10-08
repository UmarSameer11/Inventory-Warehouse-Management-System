namespace WarehouseManagementSystemApi.DTOs.VehicleType
{
    public class VehicleTypeCreateDto
    {
        public string TypeName { get; set; } = null!;
        public decimal? Capacity { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
