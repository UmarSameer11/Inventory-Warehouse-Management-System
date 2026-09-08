namespace WarehouseManagementSystemApi.DTOs.Batch
{
    public class BatchListDto
    {
        public int BatchId { get; set; }
        public string ProductName { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
        public DateTime ManufacturingDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
