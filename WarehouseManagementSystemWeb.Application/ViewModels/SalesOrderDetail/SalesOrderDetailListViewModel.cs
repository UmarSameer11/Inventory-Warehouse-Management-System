using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.SalesOrderDetail
{
    public class SalesOrderDetailListViewModel
    {
        public int SalesOrderDetailId { get; set; }
        public int SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
