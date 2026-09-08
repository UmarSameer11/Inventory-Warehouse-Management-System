namespace WarehouseManagementSystemApi.DTOs.Batch
{
    public class BatchUpdateDto
    {
        public int BatchId { get; set; }
        public int ProductId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public DateTime ManufacturingDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
