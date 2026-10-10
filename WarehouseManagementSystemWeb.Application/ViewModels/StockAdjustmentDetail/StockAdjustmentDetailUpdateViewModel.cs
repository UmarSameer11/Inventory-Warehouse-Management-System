using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockAdjustmentDetail
{
    public class StockAdjustmentDetailUpdateViewModel
    {
        public int StockAdjustmentDetailId { get; set; }
        public int StockAdjustmentId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal SystemQuantity { get; set; }
        public decimal PhysicalQuantity { get; set; }
        public decimal Difference { get; set; }

        public List<SelectListItem> StockAdjustments { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();

        public List<SelectListItem> Batches { get; set; } = new();
    }
}
