namespace WarehouseManagementSystemApi.DTOs.ProductCategory
{
    public class ProductCategoryUpdateDto
    {
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
