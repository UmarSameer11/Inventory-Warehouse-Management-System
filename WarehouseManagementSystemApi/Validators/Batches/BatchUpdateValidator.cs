using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Batch;

namespace WarehouseManagementSystemApi.Validators.Batches
{
    public class BatchUpdateValidator
        : AbstractValidator<BatchUpdateDto>
    {
        public BatchUpdateValidator()
        {
            RuleFor(x => x.BatchId)
                .GreaterThan(0)
                .WithMessage("Batch ID must be greater than 0.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Product.");

            RuleFor(x => x.BatchNumber)
                .NotEmpty()
                .WithMessage("Batch Number is required.")
                .MaximumLength(50)
                .WithMessage("Batch Number cannot exceed 50 characters.");

            RuleFor(x => x.ManufacturingDate)
                .NotEmpty()
                .WithMessage("Manufacturing Date is required.")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Manufacturing Date cannot be in the future.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(x => x.ManufacturingDate)
                .When(x => x.ExpiryDate.HasValue)
                .WithMessage("Expiry Date must be after Manufacturing Date.");
        }
    }
}
