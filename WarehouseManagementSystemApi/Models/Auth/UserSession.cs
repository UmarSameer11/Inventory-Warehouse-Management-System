namespace WarehouseManagementSystemApi.Models.Auth
{
    /// <summary>
    /// One login on one device. Every access token carries the session id, so revoking
    /// the session (logout, password change, admin action) kills the access token immediately.
    /// </summary>
    public class UserSession
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;

        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime LastActivityUtc { get; set; } = DateTime.UtcNow;

        /// <summary>Absolute end of the session, no matter how often it is refreshed.</summary>
        public DateTime ExpiresAtUtc { get; set; }

        public DateTime? RevokedAtUtc { get; set; }
        public string? RevokedReason { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public bool IsActive => RevokedAtUtc == null && DateTime.UtcNow < ExpiresAtUtc;
    }
}
