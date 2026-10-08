using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Dispatch;

namespace WarehouseManagementSystemApi.Validators.Dispatch
{
    public class DispatchCreateValidator
        : AbstractValidator<DispatchCreateDto>
    {
        public DispatchCreateValidator()
        {
            RuleFor(x => x.SalesOrderId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Sales Order.");

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Warehouse.");

            RuleFor(x => x.VehicleId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Vehicle.");

            RuleFor(x => x.DriverEmployeeId)
                .GreaterThan(0)
                .When(x => x.DriverEmployeeId.HasValue)
                .WithMessage("Please select a valid Driver.");

            RuleFor(x => x.DispatchDate)
                .NotEmpty()
                .WithMessage("Dispatch Date is required.");

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
                .SetValidator(new DispatchDetailCreateValidator());
        }
    }
}
