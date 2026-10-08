using WarehouseManagementSystemWeb.Models.Auth;

namespace WarehouseManagementSystemWeb.Services.Interfaces
{
    /// <summary>Talks to /api/Account on the API. Never throws for HTTP errors - returns AuthResult instead.</summary>
    public interface IAuthApiClient
    {
        Task<AuthResult<AuthTokenResponse>> LoginAsync(string email, string password, CancellationToken ct = default);
        Task<AuthResult<AuthTokenResponse>> RefreshAsync(string refreshToken, CancellationToken ct = default);
        Task<AuthResult<bool>> LogoutAsync(CancellationToken ct = default);
        Task<AuthResult<bool>> LogoutAllAsync(CancellationToken ct = default);
        Task<AuthResult<UserProfileModel>> GetProfileAsync(CancellationToken ct = default);
        Task<AuthResult<List<SessionModel>>> GetSessionsAsync(CancellationToken ct = default);
        Task<AuthResult<bool>> RevokeSessionAsync(Guid sessionId, CancellationToken ct = default);
        Task<AuthResult<bool>> ChangePasswordAsync(ChangePasswordViewModel model, CancellationToken ct = default);
        Task<AuthResult<UserProfileModel>> RegisterAsync(RegisterUserViewModel model, CancellationToken ct = default);
    }
}
