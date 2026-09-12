using FluentValidation;
using WarehouseManagementSystemApi.DTOs.ProductCategory;

namespace WarehouseManagementSystemApi.Validators.ProductCategory
{
    public class ProductCategoryCreateValidator
        : AbstractValidator<ProductCategoryCreateDto>
    {
        public ProductCategoryCreateValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .WithMessage("Category Name is required.")
                .MaximumLength(100)
                .WithMessage("Category Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
