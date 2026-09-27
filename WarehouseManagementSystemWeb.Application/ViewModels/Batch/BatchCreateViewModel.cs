using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Batch
{
    public class BatchCreateViewModel : IValidatableObject
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Product.")]
        public int ProductId { get; set; }

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
