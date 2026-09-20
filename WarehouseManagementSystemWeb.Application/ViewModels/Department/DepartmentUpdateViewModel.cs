
namespace WarehouseManagementSystemWeb.Application.ViewModels.Department
{
    public class DepartmentUpdateViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
