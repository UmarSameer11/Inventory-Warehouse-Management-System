using WarehouseManagementSystemApi.Common.Enums;

namespace WarehouseManagementSystemApi.DTOs.Dispatch
{
    public class DispatchStatusUpdateDto
    {
        public int DispatchId { get; set; }
        public DispatchStatus Status { get; set; }
    }
}
