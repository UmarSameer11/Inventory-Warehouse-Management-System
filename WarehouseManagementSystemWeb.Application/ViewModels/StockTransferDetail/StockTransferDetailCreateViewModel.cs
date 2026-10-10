using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockTransferDetail
{
    public class StockTransferDetailCreateViewModel
    {
        public int StockTransferId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }

        public List<SelectListItem> StockTransfers { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();

        public List<SelectListItem> Batches { get; set; } = new();
    }
}
