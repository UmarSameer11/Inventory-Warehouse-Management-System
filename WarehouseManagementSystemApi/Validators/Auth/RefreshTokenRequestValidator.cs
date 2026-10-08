using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Auth;

namespace WarehouseManagementSystemApi.Validators.Auth
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is required.")
                .MaximumLength(512)
                .WithMessage("Refresh token is invalid.");
        }
    }
}
