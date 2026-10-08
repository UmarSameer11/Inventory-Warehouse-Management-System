using FluentValidation;
using WarehouseManagementSystemApi.DTOs.StockMovement;

namespace WarehouseManagementSystemApi.Validators.StockMovement
{
    public class StockMovementFilterValidator
        : AbstractValidator<StockMovementFilterDto>
    {
        public StockMovementFilterValidator()
        {
            RuleFor(x => x.WarehouseId)
                .GreaterThan(0)
                .When(x => x.WarehouseId.HasValue)
                .WithMessage("Please select a valid Warehouse.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .When(x => x.ProductId.HasValue)
                .WithMessage("Please select a valid Product.");

            RuleFor(x => x.BatchId)
                .GreaterThan(0)
                .When(x => x.BatchId.HasValue)
                .WithMessage("Please select a valid Batch.");

            RuleFor(x => x.MovementType)
                .IsInEnum()
                .When(x => x.MovementType.HasValue)
                .WithMessage("Please select a valid Movement Type.");

            RuleFor(x => x.ToDate)
                .GreaterThanOrEqualTo(x => x.FromDate!.Value)
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue)
                .WithMessage("To Date cannot be before From Date.");

            RuleFor(x => x.ReferenceNumber)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.ReferenceNumber))
                .WithMessage("Reference Number cannot exceed 50 characters.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page Number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 200)
                .WithMessage("Page Size must be between 1 and 200.");
        }
    }
}
