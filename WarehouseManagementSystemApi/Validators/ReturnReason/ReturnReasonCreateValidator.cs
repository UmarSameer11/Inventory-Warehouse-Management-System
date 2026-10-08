using FluentValidation;
using WarehouseManagementSystemApi.DTOs.ReturnReason;

namespace WarehouseManagementSystemApi.Validators.ReturnReason
{
    public class ReturnReasonCreateValidator
        : AbstractValidator<ReturnReasonCreateDto>
    {
        public ReturnReasonCreateValidator()
        {
            RuleFor(x => x.ReasonName)
                .NotEmpty()
                .WithMessage("Reason Name is required.")
                .MaximumLength(100)
                .WithMessage("Reason Name cannot exceed 100 characters.");
        }
    }
}
