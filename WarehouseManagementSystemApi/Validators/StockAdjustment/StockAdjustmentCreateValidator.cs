using FluentValidation;
using WarehouseManagementSystemApi.DTOs.StockAdjustment;

namespace WarehouseManagementSystemApi.Validators.StockAdjustment
{
    public class StockAdjustmentCreateValidator
        : AbstractValidator<StockAdjustmentCreateDto>
    {
        public StockAdjustmentCreateValidator()
        {
            RuleFor(x => x.WarehouseId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Warehouse.");

            RuleFor(x => x.AdjustmentDate)
                .NotEmpty()
                .WithMessage("Adjustment Date is required.")
                .LessThan(_ => DateTime.Today.AddDays(1))
                .WithMessage("Adjustment Date cannot be in the future.");

            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Reason is required.")
                .MaximumLength(300)
                .WithMessage("Reason cannot exceed 300 characters.");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Employee.");

            RuleFor(x => x.Details)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("At least one line item is required.")
                .Must(d => d.Select(i => new { i.ProductId, i.BatchId }).Distinct().Count() == d.Count)
                .WithMessage("The same product and batch cannot be added more than once.");

            RuleForEach(x => x.Details)
                .SetValidator(new StockAdjustmentDetailCreateValidator());
        }
    }
}
