
namespace WarehouseManagementSystemApi.DTOs.Employee
{
    public class EmployeeListDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!; 
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? CNIC { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string DesignationName { get; set; } = null!;

        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
    }
}
