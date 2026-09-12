using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Employee
{
    public class EmployeeCreateViewModel
    {
        public string EmployeeCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? CNIC { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public DateTime JoiningDate { get; set; }

        // Dropdowns ke liye
        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Designations { get; set; } = new();
    }
}
