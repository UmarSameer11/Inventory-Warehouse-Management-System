namespace WarehouseManagementSystemWeb.Application.ViewModels.Designation
{
    public class DesignationUpdateViewModel
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
