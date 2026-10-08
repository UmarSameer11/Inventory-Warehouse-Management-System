using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Vehicle;

namespace WarehouseManagementSystemApi.Validators.Vehicle
{
    public class VehicleCreateValidator
        : AbstractValidator<VehicleCreateDto>
    {
        public VehicleCreateValidator()
        {
            RuleFor(x => x.VehicleNumber)
                .NotEmpty()
                .WithMessage("Vehicle Number is required.")
                .MaximumLength(30)
                .WithMessage("Vehicle Number cannot exceed 30 characters.")
                .Matches(@"^[A-Za-z0-9\- ]+$")
                .WithMessage("Vehicle Number can contain only letters, digits, spaces and hyphens.");

            RuleFor(x => x.RegistrationNumber)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.RegistrationNumber))
                .WithMessage("Registration Number cannot exceed 50 characters.");

            RuleFor(x => x.VehicleTypeId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Vehicle Type.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .When(x => x.Capacity.HasValue)
                .WithMessage("Capacity must be greater than 0.")
                .PrecisionScale(18, 2, true)
                .When(x => x.Capacity.HasValue)
                .WithMessage("Capacity can have at most 2 decimal places.");
        }
    }
}
