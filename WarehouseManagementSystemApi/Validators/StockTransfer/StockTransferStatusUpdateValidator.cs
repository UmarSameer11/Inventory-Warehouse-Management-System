using FluentValidation;
using WarehouseManagementSystemApi.DTOs.StockTransfer;

namespace WarehouseManagementSystemApi.Validators.StockTransfer
{
    public class StockTransferStatusUpdateValidator
        : AbstractValidator<StockTransferStatusUpdateDto>
    {
        public StockTransferStatusUpdateValidator()
        {
            RuleFor(x => x.StockTransferId)
                .GreaterThan(0)
                .WithMessage("Stock Transfer ID must be greater than 0.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Please select a valid Status.");
        }
    }
}
