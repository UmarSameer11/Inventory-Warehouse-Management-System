namespace WarehouseManagementSystemApi.DTOs.SalesReturn
{
    public class SalesReturnInspectionDto
    {
        public int SalesReturnId { get; set; }
        public int InspectedByEmployeeId { get; set; }
        public DateTime InspectionDate { get; set; }
        public List<SalesReturnInspectionDetailDto> Details { get; set; } = new();
    }
}
