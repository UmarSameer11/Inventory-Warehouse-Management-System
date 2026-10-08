using FluentValidation;
using WarehouseManagementSystemApi.DTOs.SalesReturn;

namespace WarehouseManagementSystemApi.Validators.SalesReturn
{
    public class SalesReturnInspectionValidator
        : AbstractValidator<SalesReturnInspectionDto>
    {
        public SalesReturnInspectionValidator()
        {
            RuleFor(x => x.SalesReturnId)
                .GreaterThan(0)
                .WithMessage("Sales Return ID must be greater than 0.");

            RuleFor(x => x.InspectedByEmployeeId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Employee.");

            RuleFor(x => x.InspectionDate)
                .NotEmpty()
                .WithMessage("Inspection Date is required.")
                .LessThan(_ => DateTime.Today.AddDays(1))
                .WithMessage("Inspection Date cannot be in the future.");

            RuleFor(x => x.Details)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("At least one inspected line is required.")
                .Must(d => d.Select(i => i.SalesReturnDetailId).Distinct().Count() == d.Count)
                .WithMessage("The same return line cannot be inspected more than once.");

            RuleForEach(x => x.Details)
                .SetValidator(new SalesReturnInspectionDetailValidator());
        }
    }
}
