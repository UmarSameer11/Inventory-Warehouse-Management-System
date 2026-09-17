using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Employee
{
    public class EmployeeUpdateViewModel
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? CNIC { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string DesignationName { get; set; } = null!;

        public DateTime JoiningDate { get; set; }

        // Dropdowns ke liye
        public List<SelectListItem> Departments { get; set; } = new();
        public List<SelectListItem> Designations { get; set; } = new();
    }
}
