using FluentValidation;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;

namespace WarehouseManagementSystemApi.Validators.GoodsReceipt
{
    public class GoodsReceiptDetailCreateValidator
        : AbstractValidator<GoodsReceiptDetailCreateDto>
    {
        public GoodsReceiptDetailCreateValidator()
        {
            RuleFor(x => x.PurchaseOrderDetailId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Purchase Order line.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Product.");

            RuleFor(x => x.BatchNumber)
                .NotEmpty()
                .WithMessage("Batch Number is required.")
                .MaximumLength(50)
                .WithMessage("Batch Number cannot exceed 50 characters.");

            RuleFor(x => x.ManufacturingDate)
                .NotEmpty()
                .WithMessage("Manufacturing Date is required.")
                .LessThan(_ => DateTime.Today.AddDays(1))
                .WithMessage("Manufacturing Date cannot be in the future.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(x => x.ManufacturingDate)
                .When(x => x.ExpiryDate.HasValue)
                .WithMessage("Expiry Date must be after Manufacturing Date.");

            RuleFor(x => x.QuantityReceived)
                .GreaterThan(0)
                .WithMessage("Quantity Received must be greater than 0.")
                .PrecisionScale(18, 3, true)
                .WithMessage("Quantity Received can have at most 3 decimal places.");
        }
    }
}
