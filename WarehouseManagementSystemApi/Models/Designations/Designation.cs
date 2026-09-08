using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Employee;

namespace WarehouseManagementSystemApi.Models.Designations
{
    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<Employees> Employees { get; set; }
            = new List<Employees>();
    }
}
