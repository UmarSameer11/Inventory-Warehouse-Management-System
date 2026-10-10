using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockMovement
{
    public class StockMovementListViewModel
    {
        public long StockMovementId { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }
        public string MovementType { get; set; } = null!;
        public DateTime MovementDate { get; set; }
        public string? ReferenceNumber { get; set; }
        public int? EmployeeId { get; set; }
    }
}
