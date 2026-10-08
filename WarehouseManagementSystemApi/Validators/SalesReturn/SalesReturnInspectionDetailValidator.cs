using FluentValidation;
using WarehouseManagementSystemApi.DTOs.SalesReturn;

namespace WarehouseManagementSystemApi.Validators.SalesReturn
{
    public class SalesReturnInspectionDetailValidator
        : AbstractValidator<SalesReturnInspectionDetailDto>
    {
        public SalesReturnInspectionDetailValidator()
        {
            RuleFor(x => x.SalesReturnDetailId)
                .GreaterThan(0)
                .WithMessage("Sales Return Detail ID must be greater than 0.");

            RuleFor(x => x.GoodQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Good Quantity cannot be negative.")
                .PrecisionScale(18, 3, true)
                .WithMessage("Good Quantity can have at most 3 decimal places.");

            RuleFor(x => x.DamagedQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Damaged Quantity cannot be negative.")
                .PrecisionScale(18, 3, true)
                .WithMessage("Damaged Quantity can have at most 3 decimal places.");

            RuleFor(x => x.DamagedQuantity)
                .Must((x, _) => x.GoodQuantity + x.DamagedQuantity > 0)
                .WithMessage("Good and Damaged quantity cannot both be zero.");
        }
    }
}
