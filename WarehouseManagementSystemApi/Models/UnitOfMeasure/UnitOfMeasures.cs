using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Models.UnitOfMeasure
{
    
    public class UnitOfMeasures
    {
        [Key]
        public int UnitOfMeasureId { get; set; } 
        public string UnitName { get; set; } = null!; 
        public string? Symbol { get; set; } 
        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}
