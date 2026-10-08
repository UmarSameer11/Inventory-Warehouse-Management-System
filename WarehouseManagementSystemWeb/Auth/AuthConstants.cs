namespace WarehouseManagementSystemWeb.Auth
{
    public static class AuthConstants
    {
        public const string AccessToken = "access_token";
        public const string RefreshToken = "refresh_token";
        public const string ExpiresAt = "expires_at";
        public const string RefreshExpiresAt = "refresh_expires_at";

        /// <summary>Claim that stores the API session id in the cookie principal.</summary>
        public const string SessionIdClaim = "session_id";

        public const string AdminRole = "Admin";
        public static readonly string[] AllRoles = { "Admin", "Manager", "Employee" };
    }
}
