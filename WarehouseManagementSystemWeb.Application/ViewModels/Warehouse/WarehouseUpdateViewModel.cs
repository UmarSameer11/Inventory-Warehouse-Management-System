using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Warehouse
{
    public class WarehouseUpdateViewModel
    {
        public int WarehouseId { get; set; }

        [Required(ErrorMessage = "Warehouse Code is required.")]
        [StringLength(50, ErrorMessage = "Warehouse Code cannot exceed 50 characters.")]
        public string WarehouseCode { get; set; } = null!;

        [Required(ErrorMessage = "Warehouse Name is required.")]
        [StringLength(100, ErrorMessage = "Warehouse Name cannot exceed 100 characters.")]
        public string WarehouseName { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string? Location { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Employee.")]
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = null!;

        public bool IsActive { get; set; }

        public List<SelectListItem> Employees { get; set; } = new();
    }
}