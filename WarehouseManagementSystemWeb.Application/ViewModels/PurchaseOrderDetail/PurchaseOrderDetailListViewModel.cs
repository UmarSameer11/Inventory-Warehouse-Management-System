using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrderDetail
{
    public class PurchaseOrderDetailListViewModel
    {
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
