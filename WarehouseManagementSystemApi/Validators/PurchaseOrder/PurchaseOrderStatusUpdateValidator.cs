using FluentValidation;
using WarehouseManagementSystemApi.DTOs.PurchaseOrder;

namespace WarehouseManagementSystemApi.Validators.PurchaseOrder
{
    public class PurchaseOrderStatusUpdateValidator
        : AbstractValidator<PurchaseOrderStatusUpdateDto>
    {
        public PurchaseOrderStatusUpdateValidator()
        {
            RuleFor(x => x.PurchaseOrderId)
                .GreaterThan(0)
                .WithMessage("Purchase Order ID must be greater than 0.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Please select a valid Status.");
        }
    }
}
