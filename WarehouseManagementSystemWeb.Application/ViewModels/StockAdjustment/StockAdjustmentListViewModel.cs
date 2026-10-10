using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustment
{
    public class StockAdjustmentListViewModel
    {
        public int StockAdjustmentId { get; set; }
        public string AdjustmentNumber { get; set; } = null!;
        public int WarehouseId { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Reason { get; set; } = null!;
        public int EmployeeId { get; set; }
    }
}
