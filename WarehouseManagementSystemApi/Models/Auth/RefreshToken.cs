namespace WarehouseManagementSystemApi.Models.Auth
{
    /// <summary>
    /// Only the SHA-256 hash of the refresh token is stored. The raw value exists only in the
    /// response sent to the client, so a leaked database does not leak usable tokens.
    /// </summary>
    public class RefreshToken
    {
        public int Id { get; set; }

        public Guid SessionId { get; set; }
        public UserSession Session { get; set; } = null!;

        public string TokenHash { get; set; } = string.Empty;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAtUtc { get; set; }

        public DateTime? RevokedAtUtc { get; set; }
        public string? RevokedReason { get; set; }

        /// <summary>Hash of the token that replaced this one (set on rotation). Used for reuse/theft detection.</summary>
        public string? ReplacedByTokenHash { get; set; }

        public string? CreatedByIp { get; set; }

        public bool IsActive => RevokedAtUtc == null && DateTime.UtcNow < ExpiresAtUtc;
    }
}
