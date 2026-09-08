using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Designations;

namespace WarehouseManagementSystemApi.Models.Employee
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? CNIC { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public Departments Department { get; set; } = null!;
        public Designation Designation { get; set; } = null!;
    }
}
