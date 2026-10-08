using FluentValidation;
using WarehouseManagementSystemApi.DTOs.SalesReturn;

namespace WarehouseManagementSystemApi.Validators.SalesReturn
{
    public class SalesReturnDetailCreateValidator
        : AbstractValidator<SalesReturnDetailCreateDto>
    {
        public SalesReturnDetailCreateValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Product.");

            RuleFor(x => x.BatchId)
                .GreaterThan(0)
                .When(x => x.BatchId.HasValue)
                .WithMessage("Please select a valid Batch.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.")
                .PrecisionScale(18, 3, true)
                .WithMessage("Quantity can have at most 3 decimal places.");

            RuleFor(x => x.ReturnReasonId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Return Reason.");

            RuleFor(x => x.Remarks)
                .MaximumLength(300)
                .When(x => !string.IsNullOrWhiteSpace(x.Remarks))
                .WithMessage("Remarks cannot exceed 300 characters.");
        }
    }
}
