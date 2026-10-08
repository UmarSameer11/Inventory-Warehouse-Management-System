using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Common.Enums;
using WarehouseManagementSystemApi.Models.Customer;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.Models.Salesman;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Models.SalesReturn
{
    public class SalesReturns
    {
        [Key]
        public int SalesReturnId { get; set; }
        [MaxLength(30)]
        public string ReturnNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public int? SalesOrderId { get; set; }
        public int? SalesmanId { get; set; }
        /// <summary>Warehouse that physically receives the returned goods.</summary>
        public int WarehouseId { get; set; }
        public DateTime ReturnDate { get; set; }
        public SalesReturnStatus Status { get; set; } = SalesReturnStatus.Pending;
        public int? InspectedByEmployeeId { get; set; }
        public DateTime? InspectionDate { get; set; }
        [MaxLength(500)]
        public string? Remarks { get; set; }
        public Customers Customer { get; set; } = null!;
        public SalesOrders? SalesOrder { get; set; }
        public Salesmen? Salesman { get; set; }
        public Warehouses Warehouse { get; set; } = null!;
        public Employees? InspectedByEmployee { get; set; }
        public ICollection<SalesReturnDetails> Details { get; set; } = new List<SalesReturnDetails>();
    }
}
