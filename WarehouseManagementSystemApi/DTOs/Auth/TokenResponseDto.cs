namespace WarehouseManagementSystemApi.DTOs.Auth
{
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
