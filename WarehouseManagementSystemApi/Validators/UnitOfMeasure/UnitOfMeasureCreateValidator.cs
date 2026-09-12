using FluentValidation;
using WarehouseManagementSystemApi.DTOs.UnitOfMeasure;

namespace WarehouseManagementSystemApi.Validators.UnitOfMeasure
{
    public class UnitOfMeasureCreateValidator
        : AbstractValidator<UnitOfMeasureCreateDto>
    {
        public UnitOfMeasureCreateValidator()
        {
            RuleFor(x => x.UnitName)
                .NotEmpty()
                .WithMessage("Unit Name is required.")
                .MaximumLength(50)
                .WithMessage("Unit Name cannot exceed 50 characters.");

            RuleFor(x => x.Symbol)
                .MaximumLength(10)
                .When(x => !string.IsNullOrWhiteSpace(x.Symbol))
                .WithMessage("Symbol cannot exceed 10 characters.");
        }
    }
}
