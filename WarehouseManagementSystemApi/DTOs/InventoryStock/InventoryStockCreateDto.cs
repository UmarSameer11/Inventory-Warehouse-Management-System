namespace WarehouseManagementSystemApi.DTOs.InventoryStock
{
    public class InventoryStockCreateDto
    {
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReservedQuantity { get; set; }
    }
}
