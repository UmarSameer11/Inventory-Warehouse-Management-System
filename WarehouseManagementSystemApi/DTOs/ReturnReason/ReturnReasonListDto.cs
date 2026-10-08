namespace WarehouseManagementSystemApi.DTOs.ReturnReason
{
    public class ReturnReasonListDto
    {
        public int ReturnReasonId { get; set; }
        public string ReasonName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
