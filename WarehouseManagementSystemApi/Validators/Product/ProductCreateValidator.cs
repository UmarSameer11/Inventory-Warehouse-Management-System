using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Product;

namespace WarehouseManagementSystemApi.Validators.Product
{
    public class ProductCreateValidator
        : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator()
        {
            RuleFor(x => x.ProductCode)
                .NotEmpty()
                .WithMessage("Product Code is required.")
                .MaximumLength(50)
                .WithMessage("Product Code cannot exceed 50 characters.");

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Product Name is required.")
                .MaximumLength(100)
                .WithMessage("Product Name cannot exceed 100 characters.");

            RuleFor(x => x.ProductCategoryId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Product Category.");

            RuleFor(x => x.UnitOfMeasureId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Unit of Measure.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Unit Price cannot be negative.");

            RuleFor(x => x.ReorderLevel)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Reorder Level cannot be negative.");
        }
    }
}
