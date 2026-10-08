namespace WarehouseManagementSystemApi.DTOs.Auth
{
    public class RefreshTokenRequestDto
    {
        /// <summary>Not required any more (kept so existing clients do not break). Only RefreshToken is used.</summary>
        public string? AccessToken { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
    }
}
