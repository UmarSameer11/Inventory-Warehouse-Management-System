using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.SalesReturnDetail
{
    public class SalesReturnDetailCreateViewModel
    {
        public int SalesReturnId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }
        public int ReturnReasonId { get; set; }
        public string? Condition { get; set; }

        public List<SelectListItem> SalesReturns { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();

        public List<SelectListItem> Batches { get; set; } = new();

        public List<SelectListItem> ReturnReasons { get; set; } = new();
    }
}
