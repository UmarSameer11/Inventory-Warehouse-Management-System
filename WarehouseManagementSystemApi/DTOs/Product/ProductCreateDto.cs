namespace WarehouseManagementSystemApi.DTOs.Product
{
    public class ProductCreateDto
    {
        public string ProductCode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int ProductCategoryId { get; set; }
        public int UnitOfMeasureId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ReorderLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
