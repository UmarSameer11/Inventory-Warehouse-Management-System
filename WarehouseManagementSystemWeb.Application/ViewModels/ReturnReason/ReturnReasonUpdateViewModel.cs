using System;
using System.Collections.Generic;

namespace WarehouseManagementSystemWeb.Application.ViewModels.ReturnReason
{
    public class ReturnReasonUpdateViewModel
    {
        public int ReturnReasonId { get; set; }
        public string ReasonName { get; set; } = null!;
    }
}
