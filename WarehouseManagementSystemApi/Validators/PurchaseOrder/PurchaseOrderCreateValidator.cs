using FluentValidation;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;

namespace WarehouseManagementSystemApi.Validators.PurchaseOrder
{
    public class PurchaseOrderCreateValidator
        : AbstractValidator<PurchaseOrderCreateDto>
    {
        public PurchaseOrderCreateValidator()
        {
            RuleFor(x => x.SupplierId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Supplier.");

            RuleFor(x => x.OrderDate)
                .NotEmpty()
                .WithMessage("Order Date is required.")
                .LessThan(_ => DateTime.Today.AddDays(1))
                .WithMessage("Order Date cannot be in the future.");

            RuleFor(x => x.ExpectedDeliveryDate)
                .GreaterThanOrEqualTo(x => x.OrderDate)
                .When(x => x.ExpectedDeliveryDate.HasValue)
                .WithMessage("Expected Delivery Date cannot be before Order Date.");

            RuleFor(x => x.Remarks)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Remarks))
                .WithMessage("Remarks cannot exceed 500 characters.");

            RuleFor(x => x.Details)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("At least one line item is required.")
                .Must(d => d.Select(i => i.ProductId).Distinct().Count() == d.Count)
                .WithMessage("The same product cannot be added more than once.");

            RuleForEach(x => x.Details)
                .SetValidator(new PurchaseOrderDetailCreateValidator());
        }
    }
}
