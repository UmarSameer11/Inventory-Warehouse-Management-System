using FluentValidation;
using WarehouseManagementSystemApi.DTOs.InventoryStock;

namespace WarehouseManagementSystemApi.Validators.InventoryStock
{
    public class InventoryStockUpdateValidator
        : AbstractValidator<InventoryStockUpdateDto>
    {
        public InventoryStockUpdateValidator()
        {
            RuleFor(x => x.InventoryStockId)
                .GreaterThan(0)
                .WithMessage("Inventory Stock ID must be greater than 0.");

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Warehouse.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Product.");

            RuleFor(x => x.BatchId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Batch.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative.");

            RuleFor(x => x.ReservedQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Reserved Quantity cannot be negative.");

            RuleFor(x => x)
                .Must(x => x.ReservedQuantity <= x.Quantity)
                .WithMessage("Reserved Quantity cannot be greater than Quantity.");
        }
    }
}
