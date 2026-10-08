using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.SalesOrder;
using WarehouseManagementSystemApi.Models.SalesReturn;

namespace WarehouseManagementSystemApi.Models.Salesman
{
    public class Salesmen
    {
        [Key]
        public int SalesmanId { get; set; }
        public int EmployeeId { get; set; }
        [MaxLength(30)]
        public string SalesmanCode { get; set; } = null!;
        [MaxLength(100)]
        public string? SalesArea { get; set; }
        public bool IsActive { get; set; } = true;
        public Employees Employee { get; set; } = null!;
        public ICollection<SalesOrders> SalesOrders { get; set; } = new List<SalesOrders>();
        public ICollection<SalesReturns> SalesReturns { get; set; } = new List<SalesReturns>();
    }
}
