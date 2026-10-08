using FluentValidation;
using WarehouseManagementSystemApi.DTOs.StockTransfer;

namespace WarehouseManagementSystemApi.Validators.StockTransfer
{
    public class StockTransferCreateValidator
        : AbstractValidator<StockTransferCreateDto>
    {
        public StockTransferCreateValidator()
        {
            RuleFor(x => x.FromWarehouseId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Source Warehouse.");

            RuleFor(x => x.ToWarehouseId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Destination Warehouse.");

            RuleFor(x => x.ToWarehouseId)
                .NotEqual(x => x.FromWarehouseId)
                .WithMessage("Source and Destination Warehouse cannot be the same.");

            RuleFor(x => x.TransferDate)
                .NotEmpty()
                .WithMessage("Transfer Date is required.");

            RuleFor(x => x.Remarks)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Remarks))
                .WithMessage("Remarks cannot exceed 500 characters.");

            RuleFor(x => x.Details)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("At least one line item is required.")
                .Must(d => d.Select(i => new { i.ProductId, i.BatchId }).Distinct().Count() == d.Count)
                .WithMessage("The same product and batch cannot be added more than once.");

            RuleForEach(x => x.Details)
                .SetValidator(new StockTransferDetailCreateValidator());
        }
    }
}
