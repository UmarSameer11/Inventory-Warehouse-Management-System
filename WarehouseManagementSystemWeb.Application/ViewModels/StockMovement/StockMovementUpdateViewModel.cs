using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WarehouseManagementSystemWeb.Application.ViewModels.StockMovement
{
    public class StockMovementUpdateViewModel
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

        public List<SelectListItem> Warehouses { get; set; } = new();

        public List<SelectListItem> Products { get; set; } = new();

        public List<SelectListItem> Batches { get; set; } = new();

        public List<SelectListItem> Employees { get; set; } = new();
    }
}
