using System.ComponentModel.DataAnnotations;

namespace WarehouseManagementSystemWeb.Application.ViewModels.UnitOfMeasure
{
    public class UnitOfMeasureUpdateViewModel
    {
        public int UnitOfMeasureId { get; set; }

        [Required(ErrorMessage = "Unit Name is required.")]
        [StringLength(50, ErrorMessage = "Unit Name cannot exceed 50 characters.")]
        public string UnitName { get; set; } = null!;

        [StringLength(10, ErrorMessage = "Symbol cannot exceed 10 characters.")]
        public string? Symbol { get; set; }
    }
}