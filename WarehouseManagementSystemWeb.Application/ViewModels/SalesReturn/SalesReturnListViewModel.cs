using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.SalesReturn
{
    public class SalesReturnListViewModel
    {
        public int SalesReturnId { get; set; }
        public string ReturnNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public int? SalesOrderId { get; set; }
        public int? SalesmanId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Remarks { get; set; }
    }
}
