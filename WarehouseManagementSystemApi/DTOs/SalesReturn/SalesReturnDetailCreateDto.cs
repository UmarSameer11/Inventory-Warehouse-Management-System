namespace WarehouseManagementSystemApi.DTOs.SalesReturn
{
    public class SalesReturnDetailCreateDto
    {
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }
        public int ReturnReasonId { get; set; }
        public string? Remarks { get; set; }
    }
}
