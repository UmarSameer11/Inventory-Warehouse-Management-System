using WarehouseManagementSystemApi.DTOs.Auth;

namespace WarehouseManagementSystemApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto dto);
        Task<TokenResponseDto> LoginAsync(LoginDto dto);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
