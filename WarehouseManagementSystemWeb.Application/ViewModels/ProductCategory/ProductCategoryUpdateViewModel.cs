using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemWeb.Application.ViewModels.ProductCategory
{
    public class ProductCategoryUpdateViewModel
    {
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100, ErrorMessage = "Category Name cannot exceed 100 characters.")]
        public string CategoryName { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}