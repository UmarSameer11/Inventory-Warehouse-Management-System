using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Warehouse;

namespace WarehouseManagementSystemApi.Validators.Warehouse
{
    public class WarehouseUpdateValidator
        : AbstractValidator<WarehouseUpdateDto>
    {
        public WarehouseUpdateValidator()
        {
            RuleFor(x => x.WarehouseId)
                .GreaterThan(0)
                .WithMessage("Warehouse ID must be greater than 0.");

            RuleFor(x => x.WarehouseCode)
                .NotEmpty()
                .WithMessage("Warehouse Code is required.")
                .MaximumLength(50)
                .WithMessage("Warehouse Code cannot exceed 50 characters.");

            RuleFor(x => x.WarehouseName)
                .NotEmpty()
                .WithMessage("Warehouse Name is required.")
                .MaximumLength(100)
                .WithMessage("Warehouse Name cannot exceed 100 characters.");

            RuleFor(x => x.Location)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.Location))
                .WithMessage("Location cannot exceed 200 characters.");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Employee.");
        }
    }
}
