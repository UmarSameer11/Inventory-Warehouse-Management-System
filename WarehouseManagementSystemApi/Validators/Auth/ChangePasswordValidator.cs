using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Auth;

namespace WarehouseManagementSystemApi.Validators.Auth
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .MustBeStrongPassword()
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("New password must be different from the current password.");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .WithMessage("Confirm new password is required.")
                .Equal(x => x.NewPassword)
                .WithMessage("New password and confirm password do not match.");
        }
    }
}
