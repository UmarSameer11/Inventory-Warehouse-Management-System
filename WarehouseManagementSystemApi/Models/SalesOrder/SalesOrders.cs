using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Common.Enums;
using WarehouseManagementSystemApi.Models.Customer;
using WarehouseManagementSystemApi.Models.Dispatch;
using WarehouseManagementSystemApi.Models.Salesman;

namespace WarehouseManagementSystemApi.Models.SalesOrder
{
    public class SalesOrders
    {
        [Key]
        public int SalesOrderId { get; set; }
        [MaxLength(30)]
        public string SalesOrderNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public int SalesmanId { get; set; }
        public DateTime OrderDate { get; set; }
        public SalesOrderStatus Status { get; set; } = SalesOrderStatus.Draft;
        [MaxLength(500)]
        public string? Remarks { get; set; }
        public Customers Customer { get; set; } = null!;
        public Salesmen Salesman { get; set; } = null!;
        public ICollection<SalesOrderDetails> Details { get; set; } = new List<SalesOrderDetails>();
        public ICollection<Dispatches> Dispatches { get; set; } = new List<Dispatches>();
    }
}
