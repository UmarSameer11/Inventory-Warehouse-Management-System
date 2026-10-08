namespace WarehouseManagementSystemApi.DTOs.Customer
{
    public class CustomerUpdateDto
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
