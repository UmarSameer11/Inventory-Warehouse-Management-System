using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Employee;

namespace WarehouseManagementSystemApi.Models.Department
{
    public class Departments
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<Employees> Employees { get; set; }
            = new List<Employees>();
    }
}
