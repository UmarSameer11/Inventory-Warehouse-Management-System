namespace WarehouseManagementSystemApi.Common
{
    public class ResponseWrappingMiddlwareModel<T>
    {
        /// This Genric model use for Response wrapping middleware (show the same api response for front end)
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string TraceId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
