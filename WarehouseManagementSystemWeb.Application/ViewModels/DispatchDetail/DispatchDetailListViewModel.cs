using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.DispatchDetail
{
    public class DispatchDetailListViewModel
    {
        public int DispatchDetailId { get; set; }
        public int DispatchId { get; set; }
        public int ProductId { get; set; }
        public int? BatchId { get; set; }
        public decimal Quantity { get; set; }
    }
}
