using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WarehouseManagementSystemWeb.Application.ViewModels.InventoryStock
{
    public class InventoryStockUpdateViewModel : InventoryStockFormViewModel, IValidatableObject
    {
        public int InventoryStockId { get; set; }

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

        // Display only (Detail / Delete pages). Nullable, otherwise MVC treats them as
        // implicitly [Required] and the Update form (which does not post them) would never validate.
        public string? WarehouseName { get; set; }
        public string? ProductName { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime LastUpdated { get; set; }

        [JsonIgnore]
        public decimal AvailableQuantity => Quantity - ReservedQuantity;

        [JsonIgnore]
        public bool HasLastUpdated => LastUpdated.Year > 1900;

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
