using FluentValidation;
using WarehouseManagementSystemApi.DTOs.ProductCategory;

namespace WarehouseManagementSystemApi.Validators.ProductCategory
{
    public class ProductCategoryUpdateValidator
        : AbstractValidator<ProductCategoryUpdateDto>
    {
        public ProductCategoryUpdateValidator()
        {
            RuleFor(x => x.ProductCategoryId)
                .GreaterThan(0)
                .WithMessage("Product Category ID must be greater than 0.");

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
