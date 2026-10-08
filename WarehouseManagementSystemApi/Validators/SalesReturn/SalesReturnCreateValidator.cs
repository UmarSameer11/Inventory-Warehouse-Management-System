using FluentValidation;
using WarehouseManagementSystemApi.DTOs.SalesReturn;

namespace WarehouseManagementSystemApi.Validators.SalesReturn
{
    public class SalesReturnCreateValidator
        : AbstractValidator<SalesReturnCreateDto>
    {
        public SalesReturnCreateValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Customer.");

            RuleFor(x => x.SalesOrderId)
                .GreaterThan(0)
                .When(x => x.SalesOrderId.HasValue)
                .WithMessage("Please select a valid Sales Order.");

            RuleFor(x => x.SalesmanId)
                .GreaterThan(0)
                .When(x => x.SalesmanId.HasValue)
                .WithMessage("Please select a valid Salesman.");

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Warehouse.");

            RuleFor(x => x.ReturnDate)
                .NotEmpty()
                .WithMessage("Return Date is required.")
                .LessThan(_ => DateTime.Today.AddDays(1))
                .WithMessage("Return Date cannot be in the future.");

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
                .SetValidator(new SalesReturnDetailCreateValidator());
        }
    }
}
