using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Employee;
using WarehouseManagementSystemApi.Models.InventoryStock;

namespace WarehouseManagementSystemApi.Models.Warehouse
{
    public class Warehouses
    {
        [Key]
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Location { get; set; }
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }
        public Employees Employee { get; set; }
        public ICollection<InventoryStocks> InventoryStocks { get; set; }
            = new List<InventoryStocks>();
    }
}
