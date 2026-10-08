using FluentValidation;
using WarehouseManagementSystemApi.DTOs.VehicleType;

namespace WarehouseManagementSystemApi.Validators.VehicleType
{
    public class VehicleTypeCreateValidator
        : AbstractValidator<VehicleTypeCreateDto>
    {
        public VehicleTypeCreateValidator()
        {
            RuleFor(x => x.TypeName)
                .NotEmpty()
                .WithMessage("Type Name is required.")
                .MaximumLength(100)
                .WithMessage("Type Name cannot exceed 100 characters.");

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
