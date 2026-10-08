using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.Vehicle;

namespace WarehouseManagementSystemApi.Models.VehicleType
{
    public class VehicleTypes
    {
        [Key]
        public int VehicleTypeId { get; set; }
        [MaxLength(100)]
        public string TypeName { get; set; } = null!;
        [Precision(18, 2)]
        public decimal? Capacity { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Vehicles> Vehicles { get; set; } = new List<Vehicles>();
    }
}
