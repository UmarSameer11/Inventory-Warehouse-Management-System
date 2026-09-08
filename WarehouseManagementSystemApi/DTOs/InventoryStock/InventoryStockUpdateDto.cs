namespace WarehouseManagementSystemApi.DTOs.InventoryStock
{
    public class InventoryStockUpdateDto
    {
        public int InventoryStockId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReservedQuantity { get; set; }
    }
}
