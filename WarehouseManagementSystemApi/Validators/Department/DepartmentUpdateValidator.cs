using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Department;

namespace WarehouseManagementSystemApi.Validators.Department
{
    public class DepartmentUpdateValidator
        : AbstractValidator<DepartmentUpdateDto>
    {
        public DepartmentUpdateValidator()
        {
            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage("Department ID must be greater than 0.");

            RuleFor(x => x.DepartmentName)
                .NotEmpty()
                .WithMessage("Department Name is required.")
                .MaximumLength(100)
                .WithMessage("Department Name cannot exceed 100 characters.");
        }
    }
}
