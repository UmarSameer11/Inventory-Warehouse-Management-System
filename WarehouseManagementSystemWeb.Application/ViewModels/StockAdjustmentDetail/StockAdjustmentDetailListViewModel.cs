using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustmentDetail
{
    public class StockAdjustmentDetailListViewModel
    {
        public int StockAdjustmentDetailId { get; set; }
        public int StockAdjustmentId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal SystemQuantity { get; set; }
        public decimal PhysicalQuantity { get; set; }
        public decimal Difference { get; set; }
    }
}
