using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WarehouseManagementSystemApi.Models.Batch;
using WarehouseManagementSystemApi.Models.InventoryStock;
using WarehouseManagementSystemApi.Models.ProductCategory;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;

namespace WarehouseManagementSystemApi.Models.Products

{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int ProductCategoryId { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ReorderLevel { get; set; }
        public bool IsActive { get; set; }
        public ProductCategories ProductCategory { get; set; } = null!;
        public UnitOfMeasures UnitOfMeasure { get; set; } = null!;
        public ICollection<Batches> Batches { get; set; }
            = new List<Batches>();
        public ICollection<InventoryStocks> InventoryStocks { get; set; }
            = new List<InventoryStocks>();
    }
}
