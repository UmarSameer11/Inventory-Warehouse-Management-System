using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Models.ProductCategory
{
    public class ProductCategories
    {
        [Key]
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}
