namespace WarehouseManagementSystemApi.Common.Settings
{
    /// <summary>
    /// Strongly typed binding of the "JwtSettings" section in appsettings.json.
    /// </summary>
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";

        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;

        /// <summary>HMAC signing key. Minimum 32 characters. Keep it in User Secrets / env vars, never in git.</summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>Lifetime of the (short lived) access token.</summary>
        public int AccessTokenExpiryMinutes { get; set; } = 15;

        /// <summary>Lifetime of one refresh token (sliding - renewed on every refresh).</summary>
        public int RefreshTokenExpiryDays { get; set; } = 7;

        /// <summary>Absolute lifetime of a login session. After this the user must log in again.</summary>
        public int SessionExpiryDays { get; set; } = 30;

        /// <summary>When a user logs in on more devices than this, the least recently used session is revoked.</summary>
        public int MaxActiveSessionsPerUser { get; set; } = 5;
    }
}
