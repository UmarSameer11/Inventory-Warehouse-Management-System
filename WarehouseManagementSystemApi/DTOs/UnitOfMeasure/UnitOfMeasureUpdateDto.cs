namespace WarehouseManagementSystemApi.DTOs.UnitOfMeasure
{
    public class UnitOfMeasureUpdateDto
    {
        public int UnitOfMeasureId { get; set; }
        public string UnitName { get; set; } = null!;
        public string? Symbol { get; set; }
    }
}
