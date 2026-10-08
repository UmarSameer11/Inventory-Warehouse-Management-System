namespace WarehouseManagementSystemApi.DTOs.Salesman
{
    public class SalesmanUpdateDto
    {
        public int SalesmanId { get; set; }
        public int EmployeeId { get; set; }
        public string SalesmanCode { get; set; } = null!;
        public string? SalesArea { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
