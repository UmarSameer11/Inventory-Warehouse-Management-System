namespace WarehouseManagementSystemApi.DTOs.Product
{
    public class ProductListDto
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string UnitName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public decimal ReorderLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
