using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder
{
        public class PurchaseOrderForGrnViewModel
        {
            public int PurchaseOrderId { get; set; }

            public string PurchaseOrderNumber { get; set; } = string.Empty;

            public List<PurchaseOrderGrnDetailViewModel> Details { get; set; }
                = new();
        }

        public class PurchaseOrderGrnDetailViewModel
        {
            public int PurchaseOrderDetailId { get; set; }

            public int ProductId { get; set; }

            public string ProductName { get; set; } = string.Empty;

            public decimal OrderedQuantity { get; set; }

            public decimal PreviouslyReceivedQuantity { get; set; }

            public decimal RemainingQuantity { get; set; }

            public decimal UnitPrice { get; set; }
        }
    }
