using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Department;

namespace WarehouseManagementSystemApi.Validators.Department
{
    public class DepartmentCreateValidator
        : AbstractValidator<DepartmentCreateDto>
    {
        public DepartmentCreateValidator()
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty()
                .WithMessage("Department Name is required.")
                .MaximumLength(100)
                .WithMessage("Department Name cannot exceed 100 characters.");
        }
    }
}
