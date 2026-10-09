using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WarehouseManagementSystemApi.Common.Constant;
using WarehouseManagementSystemApi.Common.Exceptions;
using WarehouseManagementSystemApi.Common.Settings;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.DTOs.Auth;
using WarehouseManagementSystemApi.Models.Auth;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private const string InvalidCredentialsMessage = "Invalid email or password.";
        private const string InvalidRefreshTokenMessage = "Invalid or expired refresh token.";

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly ICurrentUserService _currentUser;
        private readonly JwtSettings _jwt;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            ApplicationDbContext context,
            ITokenService tokenService,
            ICurrentUserService currentUser,
            IOptions<JwtSettings> jwtOptions,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tokenService = tokenService;
            _currentUser = currentUser;
            _jwt = jwtOptions.Value;
            _logger = logger;
        }

        // ------------------------------------------------------------------ Registration

        public async Task<UserProfileDto> RegisterAsync(RegistrationDto dto)
        {
            // Resolve the canonical role name from the allow-list ("admin" -> "Admin").
            var roleName = Roles.All.FirstOrDefault(r =>
                r.Equals(dto.Role?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (roleName is null || !await _roleManager.RoleExistsAsync(roleName))
            {
                throw new ArgumentException($"Role '{dto.Role}' does not exist.");
            }

            var email = dto.Email.Trim();

            if (await _userManager.FindByEmailAsync(email) != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            var user = new AppUser
            {
                Name = dto.Name.Trim(),
                Email = email,
                UserName = email,
                IsActive = true
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded)
            {
                throw new ArgumentException($"Could not create user: {JoinErrors(createResult)}");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleResult.Succeeded)
            {
                // Never leave a "user exists but has no role" account behind.
                await _userManager.DeleteAsync(user);
                throw new ArgumentException($"Could not assign role: {JoinErrors(roleResult)}");
            }

            _logger.LogInformation("New {Role} account created for {Email} by {AdminId}",
                roleName, email, _currentUser.UserId);

            return await BuildProfileAsync(user);
        }

        // ------------------------------------------------------------------ Login

        public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email.Trim());

            if (user == null)
            {
                _logger.LogWarning("Failed login attempt for unknown email {Email} from {Ip}",
                    dto.Email, _currentUser.IPAddress);
                throw new UnauthorizedAccessException(InvalidCredentialsMessage);
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                throw await BuildLockedOutExceptionAsync(user);
            }

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                // Counts the failure; Identity locks the account after MaxFailedAccessAttempts.
                await _userManager.AccessFailedAsync(user);

                _logger.LogWarning("Failed login attempt for {Email} from {Ip}",
                    dto.Email, _currentUser.IPAddress);

                if (await _userManager.IsLockedOutAsync(user))
                {
                    _logger.LogWarning("Account {Email} has been locked out", dto.Email);
                    throw await BuildLockedOutExceptionAsync(user);
                }

                throw new UnauthorizedAccessException(InvalidCredentialsMessage);
            }

            await _userManager.ResetAccessFailedCountAsync(user);

            if (!user.IsActive)
            {
                throw new ForbiddenAccessException("This account has been deactivated. Contact an administrator.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                throw new InvalidOperationException("This user has no role assigned. Contact an administrator.");
            }

            await EnforceSessionLimitAsync(user.Id);

            var now = DateTime.UtcNow;
            var session = new UserSession
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                DeviceInfo = Truncate(_currentUser.UserAgent, 500),
                IpAddress = Truncate(_currentUser.IPAddress, 100),
                CreatedAtUtc = now,
                LastActivityUtc = now,
                ExpiresAtUtc = now.AddDays(_jwt.SessionExpiryDays)
            };
            _context.UserSessions.Add(session);

            // 'user' is tracked by the same scoped DbContext, so this is saved together with the session.
            user.LastLoginAtUtc = now;

            var response = await IssueTokensAsync(user, roles, session, rotatingFrom: null);

            _logger.LogInformation("User {Email} logged in. Session {SessionId}", user.Email, session.Id);
            return response;
        }

        // ------------------------------------------------------------------ Refresh (rotation + reuse detection)

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RefreshToken))
            {
                throw new UnauthorizedAccessException(InvalidRefreshTokenMessage);
            }

            var hash = _tokenService.HashToken(dto.RefreshToken);

            var stored = await _context.RefreshTokens
                .Include(rt => rt.Session)
                    .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(rt => rt.TokenHash == hash);

            if (stored == null)
            {
                throw new UnauthorizedAccessException(InvalidRefreshTokenMessage);
            }

            var session = stored.Session;

            if (stored.RevokedAtUtc != null)
            {
                // An already-rotated token is being used again. Either the client is buggy or the
                // token was stolen. We cannot tell which, so the whole session is ended.
                if (stored.ReplacedByTokenHash != null && session.RevokedAtUtc == null)
                {
                    _logger.LogWarning(
                        "Refresh token reuse detected for user {UserId}, session {SessionId}. Session revoked.",
                        session.UserId, session.Id);

                    await RevokeSessionsAsync(new[] { session }, "Refresh token reuse detected");
                    await _context.SaveChangesAsync();
                }

                throw new UnauthorizedAccessException(InvalidRefreshTokenMessage);
            }

            if (stored.ExpiresAtUtc <= DateTime.UtcNow || !session.IsActive)
            {
                throw new UnauthorizedAccessException("Session expired. Please log in again.");
            }

            var user = session.User;

            if (!user.IsActive)
            {
                await RevokeSessionsAsync(new[] { session }, "Account deactivated");
                await _context.SaveChangesAsync();
                throw new ForbiddenAccessException("This account has been deactivated. Contact an administrator.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                throw new InvalidOperationException("This user has no role assigned. Contact an administrator.");
            }

            session.LastActivityUtc = DateTime.UtcNow;
            session.IpAddress = Truncate(_currentUser.IPAddress, 100) ?? session.IpAddress;

            return await IssueTokensAsync(user, roles, session, rotatingFrom: stored);
        }

        // ------------------------------------------------------------------ Logout & sessions

        public async Task LogoutAsync(string userId, Guid sessionId)
        {
            var session = await _context.UserSessions
                .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);

            if (session == null || session.RevokedAtUtc != null)
            {
                return; // already logged out - nothing to do
            }

            await RevokeSessionsAsync(new[] { session }, "User logged out");
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} logged out. Session {SessionId}", userId, sessionId);
        }

        public async Task LogoutAllAsync(string userId)
        {
            var sessions = await GetActiveSessionEntitiesAsync(userId);

            await RevokeSessionsAsync(sessions, "User logged out from all devices");
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} logged out from all devices ({Count} sessions)", userId, sessions.Count);
        }

        public async Task<IReadOnlyList<SessionDto>> GetActiveSessionsAsync(string userId, Guid? currentSessionId)
        {
            var now = DateTime.UtcNow;

            return await _context.UserSessions
                .AsNoTracking()
                .Where(s => s.UserId == userId && s.RevokedAtUtc == null && s.ExpiresAtUtc > now)
                .OrderByDescending(s => s.LastActivityUtc)
                .Select(s => new SessionDto
                {
                    Id = s.Id,
                    DeviceInfo = s.DeviceInfo,
                    IpAddress = s.IpAddress,
                    CreatedAtUtc = s.CreatedAtUtc,
                    LastActivityUtc = s.LastActivityUtc,
                    ExpiresAtUtc = s.ExpiresAtUtc,
                    IsCurrent = currentSessionId != null && s.Id == currentSessionId
                })
                .ToListAsync();
        }

        public async Task RevokeSessionAsync(string userId, Guid sessionId)
        {
            var session = await _context.UserSessions
                .FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId && s.RevokedAtUtc == null);

            if (session == null)
            {
                throw new KeyNotFoundException("Session not found.");
            }

            await RevokeSessionsAsync(new[] { session }, "Revoked by user");
            await _context.SaveChangesAsync();
        }

        // ------------------------------------------------------------------ Profile / password / admin

        public async Task<UserProfileDto> GetProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                       ?? throw new KeyNotFoundException("User not found.");

            return await BuildProfileAsync(user);
        }

        public async Task ChangePasswordAsync(string userId, Guid? currentSessionId, ChangePasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId)
                       ?? throw new KeyNotFoundException("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
            {
                throw new ArgumentException(JoinErrors(result));
            }

            // Every other device must log in again with the new password. This device stays logged in.
            var others = (await GetActiveSessionEntitiesAsync(userId))
                .Where(s => s.Id != currentSessionId)
                .ToList();

            await RevokeSessionsAsync(others, "Password changed");
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserId} changed password; {Count} other sessions revoked", userId, others.Count);
        }

        public async Task SetUserActiveAsync(string adminUserId, string targetUserId, bool isActive)
        {
            if (!isActive && adminUserId == targetUserId)
            {
                throw new InvalidOperationException("You cannot deactivate your own account.");
            }

            var user = await _userManager.FindByIdAsync(targetUserId)
                       ?? throw new KeyNotFoundException("User not found.");
            
            user.IsActive = isActive;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ArgumentException(JoinErrors(result));
            }

            if (!isActive)
            {
                var sessions = await GetActiveSessionEntitiesAsync(targetUserId);
                await RevokeSessionsAsync(sessions, "Account deactivated by administrator");
                await _context.SaveChangesAsync();
            }

            _logger.LogInformation("Admin {AdminId} set IsActive={IsActive} for user {UserId}",
                adminUserId, isActive, targetUserId);
        }

        // ------------------------------------------------------------------ Helpers

        private async Task<TokenResponseDto> IssueTokensAsync(
            AppUser user,
            IList<string> roles,
            UserSession session,
            RefreshToken? rotatingFrom)
        {
            var now = DateTime.UtcNow;

            var refreshTokenValue = _tokenService.GenerateRefreshToken();
            var refreshTokenHash = _tokenService.HashToken(refreshTokenValue);

            // Sliding refresh-token lifetime, but never longer than the session itself.
            var refreshExpiry = now.AddDays(_jwt.RefreshTokenExpiryDays);
            if (refreshExpiry > session.ExpiresAtUtc)
            {
                refreshExpiry = session.ExpiresAtUtc;
            }

            if (rotatingFrom != null)
            {
                rotatingFrom.RevokedAtUtc = now;
                rotatingFrom.RevokedReason = "Rotated";
                rotatingFrom.ReplacedByTokenHash = refreshTokenHash;
            }

            _context.RefreshTokens.Add(new RefreshToken
            {
                SessionId = session.Id,
                TokenHash = refreshTokenHash,
                CreatedAtUtc = now,
                ExpiresAtUtc = refreshExpiry,
                CreatedByIp = Truncate(_currentUser.IPAddress, 100)
            });

            // Rotation + new token + session changes are saved in ONE SaveChanges (atomic).
            await _context.SaveChangesAsync();

            var (accessToken, accessExpiry) = _tokenService.GenerateAccessToken(user, roles, session.Id);

            return new TokenResponseDto
            {
                Token = accessToken,
                ExpiresAtUtc = accessExpiry,
                RefreshToken = refreshTokenValue,
                RefreshTokenExpiresAtUtc = refreshExpiry,
                SessionId = session.Id,
                UserId = user.Id,
                UserName = user.UserName ?? string.Empty,
                Name = user.Name ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Role = roles.First(),
                Roles = roles.ToList()
            };
        }

        /// <summary>Marks sessions and their still-active refresh tokens as revoked. Does NOT save.</summary>
        private async Task RevokeSessionsAsync(IEnumerable<UserSession> sessions, string reason)
        {
            var list = sessions.ToList();
            if (list.Count == 0)
            {
                return;
            }

            var now = DateTime.UtcNow;
            var ids = list.Select(s => s.Id).ToList();

            foreach (var session in list)
            {
                session.RevokedAtUtc ??= now;
                session.RevokedReason ??= reason;
            }

            var tokens = await _context.RefreshTokens
                .Where(t => ids.Contains(t.SessionId) && t.RevokedAtUtc == null)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.RevokedAtUtc = now;
                token.RevokedReason = reason;
            }
        }

        private Task<List<UserSession>> GetActiveSessionEntitiesAsync(string userId)
        {
            var now = DateTime.UtcNow;

            return _context.UserSessions
                .Where(s => s.UserId == userId && s.RevokedAtUtc == null && s.ExpiresAtUtc > now)
                .ToListAsync();
        }

        /// <summary>Keeps at most MaxActiveSessionsPerUser sessions: the least recently used ones are revoked.</summary>
        private async Task EnforceSessionLimitAsync(string userId)
        {
            var max = Math.Max(1, _jwt.MaxActiveSessionsPerUser);

            var active = (await GetActiveSessionEntitiesAsync(userId))
                .OrderBy(s => s.LastActivityUtc)
                .ToList();

            var excess = active.Count - (max - 1); // leave room for the session we are about to create
            if (excess > 0)
            {
                await RevokeSessionsAsync(active.Take(excess), "Session limit reached");
            }
        }

        private async Task<UserProfileDto> BuildProfileAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name ?? string.Empty,
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty,
                Roles = roles.ToList(),
                IsActive = user.IsActive,
                LastLoginAtUtc = user.LastLoginAtUtc
            };
        }

        private async Task<AccountLockedException> BuildLockedOutExceptionAsync(AppUser user)
        {
            var end = await _userManager.GetLockoutEndDateAsync(user);
            var minutes = end.HasValue
                ? Math.Max(1, (int)Math.Ceiling((end.Value - DateTimeOffset.UtcNow).TotalMinutes))
                : 15;

            return new AccountLockedException(
                $"Account is temporarily locked because of too many failed login attempts. Try again in {minutes} minute(s).");
        }

        private static string JoinErrors(IdentityResult result) =>
            string.Join("; ", result.Errors.Select(e => e.Description));

        private static string? Truncate(string? value, int maxLength) =>
            string.IsNullOrEmpty(value) ? value : (value.Length <= maxLength ? value : value[..maxLength]);
    }
}
