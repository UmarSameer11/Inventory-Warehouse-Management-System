using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Models.Batch
{
    public class Batches
    {
        [Key]
        public int BatchId { get; set; } 
        public int ProductId { get; set; } 
        public string BatchNumber { get; set; } = null!; 
        public DateTime ManufacturingDate { get; set; } 
        public DateTime? ExpiryDate { get; set; } 
        public Product Product { get; set; } = null!; 
        public ICollection<InventoryStocks> InventoryStocks { get; set; }
            = new List<InventoryStocks>();
    }
}
