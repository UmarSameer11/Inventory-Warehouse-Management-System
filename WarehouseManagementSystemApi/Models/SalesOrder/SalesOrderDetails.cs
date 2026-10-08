using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Products;

namespace WarehouseManagementSystemApi.Models.SalesOrder
{
    public class SalesOrderDetails
    {
        [Key]
        public int SalesOrderDetailId { get; set; }
        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }
        [Precision(18, 3)]
        public decimal Quantity { get; set; }
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
        public SalesOrders SalesOrder { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
