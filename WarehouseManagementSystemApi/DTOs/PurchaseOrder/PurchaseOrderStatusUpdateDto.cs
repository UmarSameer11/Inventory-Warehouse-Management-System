using WarehouseManagementSystemApi.Common.Enums;

namespace WarehouseManagementSystemApi.DTOs.PurchaseOrder
{
    public class PurchaseOrderStatusUpdateDto
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrderStatus Status { get; set; }
    }
}
