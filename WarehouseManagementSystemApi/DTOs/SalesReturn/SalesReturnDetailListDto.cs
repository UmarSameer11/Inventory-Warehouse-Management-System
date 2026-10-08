namespace WarehouseManagementSystemApi.DTOs.SalesReturn
{
    public class SalesReturnDetailListDto
    {
        public int SalesReturnDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? BatchId { get; set; }
        public string? BatchNumber { get; set; }
        public decimal Quantity { get; set; }
        public decimal GoodQuantity { get; set; }
        public decimal DamagedQuantity { get; set; }
        public int ReturnReasonId { get; set; }
        public string ReasonName { get; set; } = null!;
        public string? Remarks { get; set; }
    }
}
