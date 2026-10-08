using FluentValidation;
using WarehouseManagementSystemApi.DTOs.SalesOrder;

namespace WarehouseManagementSystemApi.Validators.SalesOrder
{
    public class SalesOrderCreateValidator
        : AbstractValidator<SalesOrderCreateDto>
    {
        public SalesOrderCreateValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Customer.");

            RuleFor(x => x.SalesmanId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Salesman.");

            RuleFor(x => x.OrderDate)
                .NotEmpty()
                .WithMessage("Order Date is required.")
                .LessThan(_ => DateTime.Today.AddDays(1))
                .WithMessage("Order Date cannot be in the future.");

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
                .SetValidator(new SalesOrderDetailCreateValidator());
        }
    }
}
