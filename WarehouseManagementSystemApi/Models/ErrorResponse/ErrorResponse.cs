namespace WarehouseManagementSystemApi.Models.ErrorResponse
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public DateTime Time { get; set; }
        public string? Detail { get; set; }
        public string? Path { get; set; }
        public string? CorrelationId { get; set; }
    }
}
