using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Dispatch;

namespace WarehouseManagementSystemApi.Validators.Dispatch
{
    public class DispatchStatusUpdateValidator
        : AbstractValidator<DispatchStatusUpdateDto>
    {
        public DispatchStatusUpdateValidator()
        {
            RuleFor(x => x.DispatchId)
                .GreaterThan(0)
                .WithMessage("Dispatch ID must be greater than 0.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Please select a valid Status.");
        }
    }
}
