namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserName { get; }
        string? Role { get; }
        string? Method { get; }
        string? Path { get; }
        string? IPAddress { get; }
        string? UserAgent { get; }
        string? TraceId { get; }
        string? CorrelationId { get; }
    }
}
