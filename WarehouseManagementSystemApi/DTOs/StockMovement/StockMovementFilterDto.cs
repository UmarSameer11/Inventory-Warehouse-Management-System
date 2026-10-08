using WarehouseManagementSystemApi.Common.Enums;

namespace WarehouseManagementSystemApi.DTOs.StockMovement
{
    public class StockMovementFilterDto
    {
        public int? WarehouseId { get; set; }
        public int? ProductId { get; set; }
        public int? BatchId { get; set; }
        public StockMovementType? MovementType { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ReferenceNumber { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
