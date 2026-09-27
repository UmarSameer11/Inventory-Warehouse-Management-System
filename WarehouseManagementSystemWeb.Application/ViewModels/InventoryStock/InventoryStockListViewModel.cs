namespace WarehouseManagementSystemWeb.Application.ViewModels.InventoryStock
{
    public class InventoryStockListViewModel
    {
        public int InventoryStockId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string BatchNumber { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal ReservedQuantity { get; set; }
        public DateTime LastUpdated { get; set; }

        // The API does not compute this (always 0), so it is calculated here.
        public decimal AvailableQuantity => Quantity - ReservedQuantity;

        // API does not set LastUpdated yet, so it can be DateTime.MinValue.
        public bool HasLastUpdated => LastUpdated.Year > 1900;
    }
}
