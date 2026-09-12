using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagementSystemWeb.Application.ViewModels.Common
{
    public class ApiResponseViewModel<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public string? TraceId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
