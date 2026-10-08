using FluentValidation;
using WarehouseManagementSystemApi.DTOs.SalesOrder;

namespace WarehouseManagementSystemApi.Validators.SalesOrder
{
    public class SalesOrderDetailCreateValidator
        : AbstractValidator<SalesOrderDetailCreateDto>
    {
        public SalesOrderDetailCreateValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Product.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.")
                .PrecisionScale(18, 3, true)
                .WithMessage("Quantity can have at most 3 decimal places.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit Price must be greater than 0.")
                .PrecisionScale(18, 2, true)
                .WithMessage("Unit Price can have at most 2 decimal places.");
        }
    }
}
