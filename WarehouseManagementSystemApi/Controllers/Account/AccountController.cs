using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WarehouseManagementSystemApi.Common.Constant;
using WarehouseManagementSystemApi.DTOs.Auth;
using WarehouseManagementSystemApi.Extensions;
using WarehouseManagementSystemApi.Filters;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ValidationFilter))]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // ---------------------------------------------------------------- Public endpoints

        [HttpPost("Login")]
        [AllowAnonymous]
        [EnableRateLimiting("LoginPolicy")]
        public async Task<ActionResult<TokenResponseDto>> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        [EnableRateLimiting("LoginPolicy")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            var result = await _authService.RefreshTokenAsync(dto);
            return Ok(result);
        }

        // ---------------------------------------------------------------- Registration (Admin only)

        [HttpPost("Registration")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Registration([FromBody] RegistrationDto dto)
        {
            var user = await _authService.RegisterAsync(dto);
            return Ok(new { Message = "Registration successful.", User = user });
        }

        // ---------------------------------------------------------------- Logged-in user

        /// <summary>Logs out the current device (ends the session this token belongs to).</summary>
        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync(User.GetUserId()!, User.GetSessionId() ?? Guid.Empty);
            return Ok(new { Message = "Logged out successfully." });
        }

        /// <summary>Logs out from every device.</summary>
        [HttpPost("LogoutAll")]
        [Authorize]
        public async Task<IActionResult> LogoutAll()
        {
            await _authService.LogoutAllAsync(User.GetUserId()!);
            return Ok(new { Message = "Logged out from all devices." });
        }

        [HttpGet("Me")]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> Me()
        {
            var profile = await _authService.GetProfileAsync(User.GetUserId()!);
            return Ok(profile);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            await _authService.ChangePasswordAsync(User.GetUserId()!, User.GetSessionId(), dto);
            return Ok(new { Message = "Password changed. Other devices have been logged out." });
        }

        /// <summary>Lists the devices/sessions that are currently logged in.</summary>
        [HttpGet("Sessions")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<SessionDto>>> Sessions()
        {
            var sessions = await _authService.GetActiveSessionsAsync(User.GetUserId()!, User.GetSessionId());
            return Ok(sessions);
        }

        /// <summary>Logs out one specific device.</summary>
        [HttpDelete("Sessions/{sessionId:guid}")]
        [Authorize]
        public async Task<IActionResult> RevokeSession(Guid sessionId)
        {
            await _authService.RevokeSessionAsync(User.GetUserId()!, sessionId);
            return Ok(new { Message = "Session revoked." });
        }

        // ---------------------------------------------------------------- Admin

        [HttpPost("Users/{userId}/Deactivate")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeactivateUser(string userId)
        {
            await _authService.SetUserActiveAsync(User.GetUserId()!, userId, false);
            return Ok(new { Message = "User deactivated and logged out from all devices." });
        }

        [HttpPost("Users/{userId}/Activate")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> ActivateUser(string userId)
        {
            await _authService.SetUserActiveAsync(User.GetUserId()!, userId, true);
            return Ok(new { Message = "User activated." });
        }
    }
}
