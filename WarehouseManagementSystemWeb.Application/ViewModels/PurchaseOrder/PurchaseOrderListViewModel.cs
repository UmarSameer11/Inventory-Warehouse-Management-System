using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder
{

        public class PurchaseOrderListViewModel
        {
            public int PurchaseOrderId { get; set; }

            public string PurchaseOrderNumber { get; set; } = string.Empty;

            public int SupplierId { get; set; }

            public string SupplierName { get; set; } = string.Empty;

            public DateTime OrderDate { get; set; }

            public DateTime? ExpectedDeliveryDate { get; set; }

            public string Status { get; set; } = string.Empty;

            public decimal GrandTotal { get; set; }

            public int TotalProducts { get; set; }
        }
    }


