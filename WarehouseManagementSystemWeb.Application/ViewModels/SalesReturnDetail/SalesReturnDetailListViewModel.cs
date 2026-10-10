using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.SalesReturnDetail
{
    public class SalesReturnDetailListViewModel
    {
        public int SalesReturnDetailId { get; set; }
        public int SalesReturnId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }
        public int ReturnReasonId { get; set; }
        public string? Condition { get; set; }
    }
}
