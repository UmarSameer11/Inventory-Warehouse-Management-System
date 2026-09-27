using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Batch
{
    public class BatchUpdateViewModel : IValidatableObject
    {
        public int BatchId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Product.")]
        public int ProductId { get; set; }

        // Display only (Detail / Delete pages). Nullable, otherwise MVC treats it as
        // implicitly [Required] and the Update form (which does not post it) would never validate.
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Batch Number is required.")]
        [StringLength(50, ErrorMessage = "Batch Number cannot exceed 50 characters.")]
        public string BatchNumber { get; set; } = null!;

        [Required(ErrorMessage = "Manufacturing Date is required.")]
        [DataType(DataType.Date)]
        public DateTime ManufacturingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        // Dropdown (not sent to the API)
        [JsonIgnore]
        public List<SelectListItem> Products { get; set; } = new();

        [JsonIgnore]
        public bool IsExpired =>
            ExpiryDate.HasValue && ExpiryDate.Value.Date < DateTime.Today;

        [JsonIgnore]
        public bool IsExpiringSoon =>
            ExpiryDate.HasValue
            && !IsExpired
            && ExpiryDate.Value.Date <= DateTime.Today.AddDays(30);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ManufacturingDate.Date > DateTime.Today)
            {
                yield return new ValidationResult(
                    "Manufacturing Date cannot be in the future.",
                    new[] { nameof(ManufacturingDate) });
            }

            if (ExpiryDate.HasValue && ExpiryDate.Value.Date <= ManufacturingDate.Date)
            {
                yield return new ValidationResult(
                    "Expiry Date must be after Manufacturing Date.",
                    new[] { nameof(ExpiryDate) });
            }
        }
    }
}
