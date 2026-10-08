using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.Warehouse;

namespace WarehouseManagementSystemApi.Models.StockAdjustment
{
    public class StockAdjustments
    {
        [Key]
        public int StockAdjustmentId { get; set; }
        [MaxLength(30)]
        public string AdjustmentNumber { get; set; } = null!;
        public int WarehouseId { get; set; }
        public DateTime AdjustmentDate { get; set; }
        [MaxLength(300)]
        public string Reason { get; set; } = null!;
        public int EmployeeId { get; set; }
        public Warehouses Warehouse { get; set; } = null!;
        public Employees Employee { get; set; } = null!;
        public ICollection<StockAdjustmentDetails> Details { get; set; } = new List<StockAdjustmentDetails>();
    }
}
