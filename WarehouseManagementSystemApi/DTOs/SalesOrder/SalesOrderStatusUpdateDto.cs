using WarehouseManagementSystemApi.Common.Enums;

namespace WarehouseManagementSystemApi.DTOs.SalesOrder
{
    public class SalesOrderStatusUpdateDto
    {
        public int SalesOrderId { get; set; }
        public SalesOrderStatus Status { get; set; }
    }
}
