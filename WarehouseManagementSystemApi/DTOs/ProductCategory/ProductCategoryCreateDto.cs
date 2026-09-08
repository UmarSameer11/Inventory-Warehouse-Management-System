namespace WarehouseManagementSystemApi.DTOs.ProductCategory
{
    public class ProductCategoryCreateDto
    {
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
