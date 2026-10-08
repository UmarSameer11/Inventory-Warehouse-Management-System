using FluentValidation;
using WarehouseManagementSystemApi.DTOs.StockTransfer;

namespace WarehouseManagementSystemApi.Validators.StockTransfer
{
    public class StockTransferDetailCreateValidator
        : AbstractValidator<StockTransferDetailCreateDto>
    {
        public StockTransferDetailCreateValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Product.");

            RuleFor(x => x.BatchId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Batch.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.")
                .PrecisionScale(18, 3, true)
                .WithMessage("Quantity can have at most 3 decimal places.");
        }
    }
}
