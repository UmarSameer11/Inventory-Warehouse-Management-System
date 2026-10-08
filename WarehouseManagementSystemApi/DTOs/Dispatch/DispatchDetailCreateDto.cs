namespace WarehouseManagementSystemApi.DTOs.Dispatch
{
    public class DispatchDetailCreateDto
    {
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        public decimal Quantity { get; set; }
    }
}
