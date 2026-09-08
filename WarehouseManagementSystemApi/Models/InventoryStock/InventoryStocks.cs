using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Models.InventoryStock
{
    public class InventoryStocks
    {
        [Key]
        public int InventoryStockId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int BatchId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReservedQuantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public Warehouses Warehouse { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public Batches Batch { get; set; } = null!;
    }
}
