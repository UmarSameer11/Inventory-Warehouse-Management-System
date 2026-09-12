using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Designation;

namespace WarehouseManagementSystemApi.Validators.Designation
{
    public class DesignationCreateValidator
        : AbstractValidator<DesignationCreateDto>
    {
        public DesignationCreateValidator()
        {
            RuleFor(x => x.DesignationName)
                .NotEmpty()
                .WithMessage("Designation Name is required.")
                .MaximumLength(100)
                .WithMessage("Designation Name cannot exceed 100 characters.");
        }
    }
}
