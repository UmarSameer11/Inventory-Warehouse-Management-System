namespace WarehouseManagementSystemApi.MiddleWares.AuditMiddleware
{
    public class AuditContext
    {
        public string? UserId { get; init; }
        public string? UserName { get; init; }
        public string? Role { get; init; }
        public string? Method { get; init; }
        public string? Path { get; init; }
        public string? IPAddress { get; init; }
        public string? UserAgent { get; init; }
        public string? TraceId { get; init; }
    }
}
