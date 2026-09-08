using WarehouseManagementSystemApi.Models.Department;
using WarehouseManagementSystemApi.Models.Designations;

namespace WarehouseManagementSystemApi.DTOs.Employee
{
    public class EmployeeCreateDto
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
        public bool IsActive { get; set; }
    }
}
