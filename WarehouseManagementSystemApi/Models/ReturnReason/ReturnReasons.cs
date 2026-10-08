using System.ComponentModel.DataAnnotations;
using WarehouseManagementSystemApi.Models.SalesReturn;

namespace WarehouseManagementSystemApi.Models.ReturnReason
{
    public class ReturnReasons
    {
        [Key]
        public int ReturnReasonId { get; set; }
        [MaxLength(100)]
        public string ReasonName { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public ICollection<SalesReturnDetails> SalesReturnDetails { get; set; } = new List<SalesReturnDetails>();
    }
}
