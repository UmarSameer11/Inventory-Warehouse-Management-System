using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.DTOs.Auth;
using WarehouseManagementSystemApi.Models.Auth;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        // How long a refresh token stays valid.
        private const int RefreshTokenExpiryDays = 7;

        public AuthService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            ApplicationDbContext context,
            ITokenService tokenService,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task RegisterAsync(RegistrationDto dto)
        {
            // Role is already validated by RegistrationDtoValidator (allow-list),
            // but we double check existence in Identity's RoleManager too.
            if (!await _roleManager.RoleExistsAsync(dto.Role))
            {
                throw new ArgumentException($"Role '{dto.Role}' does not exist.");
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            var user = new AppUser
            {
                Name = dto.Name,
                Email = dto.Email,
                UserName = dto.Email
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                throw new ArgumentException($"Could not create user: {errors}");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);
            if (!roleResult.Succeeded)
            {
                // Roll back the user we just created so we never leave
                // a "user exists but has no role" account behind.
                await _userManager.DeleteAsync(user);

                var errors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                throw new ArgumentException($"Could not assign role: {errors}");
            }

            _logger.LogInformation("New {Role} account created for {Email}", dto.Role, dto.Email);
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                _logger.LogWarning("Failed login attempt for {Email}", dto.Email);
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            //if (user == null)
            //{
            //    throw new Exception("Ali user not found.");
            //}

            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //var result = await _userManager.ResetPasswordAsync(
            //    user,
            //    token,
            //    "Ali@123456");

            //if (!result.Succeeded)
            //{
            //    throw new Exception(
            //        string.Join("; ", result.Errors.Select(x => x.Description)));
            //}

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (string.IsNullOrEmpty(role))
            {
                throw new InvalidOperationException("This user has no role assigned. Contact an administrator.");
            }

            return await IssueTokensAsync(user, role);
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var storedToken = await _context.Set<RefreshToken>()
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);

            if (storedToken == null || !storedToken.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }

            var roles = await _userManager.GetRolesAsync(storedToken.User);
            var role = roles.FirstOrDefault();
            if (string.IsNullOrEmpty(role))
            {
                throw new InvalidOperationException("This user has no role assigned. Contact an administrator.");
            }

            // Rotate: revoke the old refresh token, issue a brand new pair.
            storedToken.RevokedAtUtc = DateTime.UtcNow;
            var tokens = await IssueTokensAsync(storedToken.User, role);

            return tokens;
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _context.Set<RefreshToken>()
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedToken == null)
            {
                return; // already gone - nothing to do, no need to leak that info
            }

            storedToken.RevokedAtUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        private async Task<TokenResponseDto> IssueTokensAsync(AppUser user, string role)
        {
            var (accessToken, expiresAtUtc) = _tokenService.GenerateAccessToken(user, role);
            var refreshTokenValue = _tokenService.GenerateRefreshToken();

            _context.Set<RefreshToken>().Add(new RefreshToken
            {
                Token = refreshTokenValue,
                UserId = user.Id,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays)
            });

            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                Token = accessToken,
                ExpiresAtUtc = expiresAtUtc,
                RefreshToken = refreshTokenValue,
                UserName = user.UserName,
                Role = role
            };
        }
    }
}
