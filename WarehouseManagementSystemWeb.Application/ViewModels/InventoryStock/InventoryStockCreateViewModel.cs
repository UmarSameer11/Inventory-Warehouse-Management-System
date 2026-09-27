using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemWeb.Application.ViewModels.InventoryStock
{
    public class InventoryStockCreateViewModel : InventoryStockFormViewModel, IValidatableObject
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Warehouse.")]
        public int WarehouseId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Product.")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Batch.")]
        public int BatchId { get; set; }

        [Range(0, 999999999999, ErrorMessage = "Quantity cannot be negative.")]
        public decimal Quantity { get; set; }

        [Range(0, 999999999999, ErrorMessage = "Reserved Quantity cannot be negative.")]
        public decimal ReservedQuantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReservedQuantity > Quantity)
            {
                yield return new ValidationResult(
                    "Reserved Quantity cannot be greater than Quantity.",
                    new[] { nameof(ReservedQuantity) });
            }
        }
    }
}
