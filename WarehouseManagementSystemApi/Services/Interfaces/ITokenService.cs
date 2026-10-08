using WarehouseManagementSystemApi.Models.Auth;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface ITokenService
    {
        (string token, DateTime expiresAtUtc) GenerateAccessToken(AppUser user, IEnumerable<string> roles, Guid sessionId);

        /// <summary>Creates a cryptographically random refresh token (raw value, goes to the client).</summary>
        string GenerateRefreshToken();

        /// <summary>SHA-256 hash of a refresh token (this is what is stored in the database).</summary>
        string HashToken(string token);
    }
}
