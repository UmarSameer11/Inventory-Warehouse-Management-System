using FluentValidation;
using WarehouseManagementSystemApi.DTOs.GoodsReceipt;

namespace WarehouseManagementSystemApi.Validators.GoodsReceipt
{
    public class GoodsReceiptCreateValidator
        : AbstractValidator<GoodsReceiptCreateDto>
    {
        public GoodsReceiptCreateValidator()
        {
            RuleFor(x => x.PurchaseOrderId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Purchase Order.");

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Warehouse.");

            RuleFor(x => x.ReceivedByEmployeeId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Employee.");

            RuleFor(x => x.ReceivedDate)
                .NotEmpty()
                .WithMessage("Received Date is required.")
                .LessThan(_ => DateTime.Today.AddDays(1))
                .WithMessage("Received Date cannot be in the future.");

            RuleFor(x => x.SupplierInvoiceNumber)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.SupplierInvoiceNumber))
                .WithMessage("Supplier Invoice Number cannot exceed 50 characters.");

            RuleFor(x => x.Remarks)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Remarks))
                .WithMessage("Remarks cannot exceed 500 characters.");

            RuleFor(x => x.Details)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("At least one line item is required.")
                .Must(d => d.Select(i => new { i.PurchaseOrderDetailId, i.BatchNumber }).Distinct().Count() == d.Count)
                .WithMessage("The same Purchase Order line and Batch Number cannot be added more than once.");

            RuleForEach(x => x.Details)
                .SetValidator(new GoodsReceiptDetailCreateValidator());
        }
    }
}
