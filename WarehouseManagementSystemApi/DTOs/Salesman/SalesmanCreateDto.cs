namespace WarehouseManagementSystemApi.DTOs.Salesman
{
    public class SalesmanCreateDto
    {
        public int EmployeeId { get; set; }
        public string SalesmanCode { get; set; } = null!;
        public string? SalesArea { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
