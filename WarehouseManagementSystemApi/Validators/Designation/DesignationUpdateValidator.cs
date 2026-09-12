using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Designation;

namespace WarehouseManagementSystemApi.Validators.Designation
{
    public class DesignationUpdateValidator
        : AbstractValidator<DesignationUpdateDto>
    {
        public DesignationUpdateValidator()
        {
            RuleFor(x => x.DesignationId)
                .GreaterThan(0)
                .WithMessage("Designation ID must be greater than 0.");

            RuleFor(x => x.DesignationName)
                .NotEmpty()
                .WithMessage("Designation Name is required.")
                .MaximumLength(100)
                .WithMessage("Designation Name cannot exceed 100 characters.");
        }
    }
}
