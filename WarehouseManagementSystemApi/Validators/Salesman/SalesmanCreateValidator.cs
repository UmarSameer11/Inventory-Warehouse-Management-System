using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Salesman;

namespace WarehouseManagementSystemApi.Validators.Salesman
{
    public class SalesmanCreateValidator
        : AbstractValidator<SalesmanCreateDto>
    {
        public SalesmanCreateValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Employee.");

            RuleFor(x => x.SalesmanCode)
                .NotEmpty()
                .WithMessage("Salesman Code is required.")
                .MaximumLength(30)
                .WithMessage("Salesman Code cannot exceed 30 characters.");

            RuleFor(x => x.SalesArea)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.SalesArea))
                .WithMessage("Sales Area cannot exceed 100 characters.");
        }
    }
}
