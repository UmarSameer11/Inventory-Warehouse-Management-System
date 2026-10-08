namespace WarehouseManagementSystemApi.DTOs.ReturnReason
{
    public class ReturnReasonCreateDto
    {
        public string ReasonName { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
