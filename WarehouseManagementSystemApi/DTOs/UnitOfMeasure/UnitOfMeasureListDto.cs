namespace WarehouseManagementSystemApi.DTOs.UnitOfMeasure
{
    public class UnitOfMeasureListDto
    {
        public int UnitOfMeasureId { get; set; }
        public string UnitName { get; set; } = null!;
        public string? Symbol { get; set; }
    }
}
