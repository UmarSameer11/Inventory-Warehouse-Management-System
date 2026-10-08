namespace WarehouseManagementSystemApi.DTOs.Auth
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime LastActivityUtc { get; set; }
        public DateTime ExpiresAtUtc { get; set; }

        /// <summary>True for the session that made this request.</summary>
        public bool IsCurrent { get; set; }
    }
}
