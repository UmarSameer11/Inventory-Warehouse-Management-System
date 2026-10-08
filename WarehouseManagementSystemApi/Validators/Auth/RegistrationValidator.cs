using FluentValidation;
using WarehouseManagementSystemApi.Common.Constant;
using WarehouseManagementSystemApi.DTOs.Auth;

namespace WarehouseManagementSystemApi.Validators.Auth
{
    public class RegistrationValidator
        : AbstractValidator<RegistrationDto>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.")
                .MaximumLength(256)
                .WithMessage("Email cannot exceed 256 characters.");

            RuleFor(x => x.Password)
                .MustBeStrongPassword();

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required.")
                .Equal(x => x.Password)
                .WithMessage("Password and Confirm Password do not match.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("Role is required.")
                .Must(Roles.IsValid)
                .WithMessage($"Role must be one of: {string.Join(", ", Roles.All)}.");
        }
    }
}
