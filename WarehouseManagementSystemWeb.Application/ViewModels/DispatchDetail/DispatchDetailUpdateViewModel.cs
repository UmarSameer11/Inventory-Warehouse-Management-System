using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.DispatchDetail
{
    public class DispatchDetailUpdateViewModel
    {
        public int DispatchDetailId { get; set; }
        public int DispatchId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }

        public List<SelectListItem> Dispatches { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();

        public List<SelectListItem> Batches { get; set; } = new();
    }
}
