namespace WarehouseManagementSystemApi.DTOs.Auth
{
    public class TokenResponseDto
    {
        /// <summary>Access token (JWT). Send as: Authorization: Bearer {Token}</summary>
        public string Token { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public DateTime ExpiresAtUtc { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiresAtUtc { get; set; }

        public Guid SessionId { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        /// <summary>Primary role (kept for backward compatibility).</summary>
        public string Role { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
