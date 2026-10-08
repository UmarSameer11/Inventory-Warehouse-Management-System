using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.Models.SalesReturn;

namespace WarehouseManagementSystemApi.Models.Customer
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        [MaxLength(30)]
        public string CustomerCode { get; set; } = null!;
        [MaxLength(150)]
        public string CustomerName { get; set; } = null!;
        [MaxLength(100)]
        public string? ContactPerson { get; set; }
        [MaxLength(30)]
        public string? Phone { get; set; }
        [MaxLength(300)]
        public string? Address { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<SalesOrders> SalesOrders { get; set; } = new List<SalesOrders>();
        public ICollection<SalesReturns> SalesReturns { get; set; } = new List<SalesReturns>();
    }
}
