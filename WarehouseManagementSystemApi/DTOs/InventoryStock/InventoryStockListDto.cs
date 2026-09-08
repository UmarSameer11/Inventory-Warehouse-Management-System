namespace WarehouseManagementSystemApi.DTOs.InventoryStock
{
    public class InventoryStockListDto
    {
        public int InventoryStockId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal ReservedQuantity { get; set; }
        public decimal AvailableQuantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
