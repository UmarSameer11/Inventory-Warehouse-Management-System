using FluentValidation;
using WarehouseManagementSystemApi.DTOs.SalesOrder;

namespace WarehouseManagementSystemApi.Validators.SalesOrder
{
    public class SalesOrderStatusUpdateValidator
        : AbstractValidator<SalesOrderStatusUpdateDto>
    {
        public SalesOrderStatusUpdateValidator()
        {
            RuleFor(x => x.SalesOrderId)
                .GreaterThan(0)
                .WithMessage("Sales Order ID must be greater than 0.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Please select a valid Status.");
        }
    }
}
