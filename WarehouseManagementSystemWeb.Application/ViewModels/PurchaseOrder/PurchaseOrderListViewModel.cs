using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder
{
    public class PurchaseOrderListViewModel
    {
        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderNumber { get; set; } = null!;
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
