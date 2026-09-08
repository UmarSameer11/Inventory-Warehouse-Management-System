using WarehouseManagementSystemApi.Models.Auth;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface ITokenService
    {
        (string token, DateTime expiresAtUtc) GenerateAccessToken(AppUser user, string role);
        string GenerateRefreshToken();
    }
}
