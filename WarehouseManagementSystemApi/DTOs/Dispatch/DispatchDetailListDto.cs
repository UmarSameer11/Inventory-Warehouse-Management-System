namespace WarehouseManagementSystemApi.DTOs.Dispatch
{
    public class DispatchDetailListDto
    {
        public int DispatchDetailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int BatchId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
        public decimal Quantity { get; set; }
    }
}
