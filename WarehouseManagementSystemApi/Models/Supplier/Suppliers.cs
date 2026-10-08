using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.PurchaseOrder;

namespace WarehouseManagementSystemApi.Models.Supplier
{
    public class Suppliers
    {
        [Key]
        public int SupplierId { get; set; }
        [MaxLength(30)]
        public string SupplierCode { get; set; } = null!;
        [MaxLength(150)]
        public string SupplierName { get; set; } = null!;
        [MaxLength(100)]
        public string? ContactPerson { get; set; }
        [MaxLength(30)]
        public string? Phone { get; set; }
        [MaxLength(150)]
        public string? Email { get; set; }
        [MaxLength(300)]
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<PurchaseOrders> PurchaseOrders { get; set; } = new List<PurchaseOrders>();
    }
}
