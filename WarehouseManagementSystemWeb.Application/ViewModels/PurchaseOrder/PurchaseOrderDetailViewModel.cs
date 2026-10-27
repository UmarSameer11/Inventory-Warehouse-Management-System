using System;
using System.Collections.Generic;
using WarehouseManagementSystemApi.Common.Enums;

namespace WarehouseManagementSystemWeb.Application.ViewModels.PurchaseOrder
{
    public class PurchaseOrderDetailViewModel
    {
        public int PurchaseOrderId { get; set; }

        public string PurchaseOrderNumber { get; set; } = string.Empty;

        public int SupplierId { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public string? Remarks { get; set; }

        public PurchaseOrderStatus Status { get; set; } 

        public List<PurchaseOrderProductDetailViewModel> Details { get; set; }
            = new();
    }

    public class PurchaseOrderProductDetailViewModel
    {
        public int PurchaseOrderDetailId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal => Quantity * UnitPrice;
    }
}