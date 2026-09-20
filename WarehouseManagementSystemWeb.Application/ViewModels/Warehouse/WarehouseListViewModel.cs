using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Warehouse
{
    public class WarehouseListViewModel
    {
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string? Location { get; set; }
        public string EmployeeName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}