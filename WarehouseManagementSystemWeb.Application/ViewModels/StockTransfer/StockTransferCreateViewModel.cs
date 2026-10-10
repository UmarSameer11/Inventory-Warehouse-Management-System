using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockTransfer
{
    public class StockTransferCreateViewModel
    {
        public string TransferNumber { get; set; } = null!;
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public DateTime TransferDate { get; set; }
        public string Status { get; set; } = null!;

        public List<SelectListItem> Warehouses { get; set; } = new();
    }
}
