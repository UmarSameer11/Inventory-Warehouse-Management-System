using WarehouseManagementSystemApi.DTOs.Auth;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserProfileDto> RegisterAsync(RegistrationDto dto);
        Task<TokenResponseDto> LoginAsync(LoginDto dto);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto);

        /// <summary>Ends one session (the current one on a normal logout).</summary>
        Task LogoutAsync(string userId, Guid sessionId);

        /// <summary>Ends every session of the user ("log out from all devices").</summary>
        Task LogoutAllAsync(string userId);

        Task<IReadOnlyList<SessionDto>> GetActiveSessionsAsync(string userId, Guid? currentSessionId);
        Task RevokeSessionAsync(string userId, Guid sessionId);

        Task<UserProfileDto> GetProfileAsync(string userId);
        Task ChangePasswordAsync(string userId, Guid? currentSessionId, ChangePasswordDto dto);

        /// <summary>Admin: activate / deactivate an account. Deactivating also ends all its sessions.</summary>
        Task SetUserActiveAsync(string adminUserId, string targetUserId, bool isActive);
    }
}
