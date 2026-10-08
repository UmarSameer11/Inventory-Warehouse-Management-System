using FluentValidation;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;

namespace WarehouseManagementSystemApi.Validators.StockAdjustment
{
    public class StockAdjustmentDetailCreateValidator
        : AbstractValidator<StockAdjustmentDetailCreateDto>
    {
        public StockAdjustmentDetailCreateValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Product.");

            RuleFor(x => x.BatchId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Batch.");

            RuleFor(x => x.PhysicalQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Physical Quantity cannot be negative.")
                .PrecisionScale(18, 3, true)
                .WithMessage("Physical Quantity can have at most 3 decimal places.");
        }
    }
}
