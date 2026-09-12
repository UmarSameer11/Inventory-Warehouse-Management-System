using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Employee;

namespace WarehouseManagementSystemApi.Validators.Employee
{
    public class EmployeeUpdateValidator
        : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("Employee ID must be greater than 0.");

            RuleFor(x => x.EmployeeCode)
                .NotEmpty()
                .WithMessage("Employee Code is required.")
                .MaximumLength(20)
                .WithMessage("Employee Code cannot exceed 20 characters.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required.")
                .MaximumLength(50)
                .WithMessage("First Name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required.")
                .MaximumLength(50)
                .WithMessage("Last Name cannot exceed 50 characters.");

            RuleFor(x => x.CNIC)
                .Matches(@"^\d{5}-\d{7}-\d{1}$")
                .When(x => !string.IsNullOrWhiteSpace(x.CNIC))
                .WithMessage("CNIC must be in format 35202-1234567-1.");

            RuleFor(x => x.Phone)
                .Matches(@"^03\d{9}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone))
                .WithMessage("Phone number must be in format 03XXXXXXXXX.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Department.");

            RuleFor(x => x.DesignationId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Designation.");

            RuleFor(x => x.JoiningDate)
                .NotEmpty()
                .WithMessage("Joining Date is required.")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Joining Date cannot be in the future.");
        }
    }
}
