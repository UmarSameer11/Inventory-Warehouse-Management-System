using FluentValidation;
using WarehouseManagementSystemApi.DTOs.ReturnReason;

namespace WarehouseManagementSystemApi.Validators.ReturnReason
{
    public class ReturnReasonUpdateValidator
        : AbstractValidator<ReturnReasonUpdateDto>
    {
        public ReturnReasonUpdateValidator()
        {
            RuleFor(x => x.ReturnReasonId)
                .GreaterThan(0)
                .WithMessage("Return Reason ID must be greater than 0.");

            RuleFor(x => x.ReasonName)
                .NotEmpty()
                .WithMessage("Reason Name is required.")
                .MaximumLength(100)
                .WithMessage("Reason Name cannot exceed 100 characters.");
        }
    }
}
