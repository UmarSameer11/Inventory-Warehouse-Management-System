using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WarehouseManagementSystemApi.Common.Constant;
using WarehouseManagementSystemApi.DTOs.Auth;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("Registration")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Registration([FromBody] RegistrationDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok(new { Message = "Registration successful." });
        }

        [HttpPost("Login")]
        [EnableRateLimiting("LoginPolicy")]
        public async Task<ActionResult<TokenResponseDto>> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        [EnableRateLimiting("LoginPolicy")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            var result = await _authService.RefreshTokenAsync(dto);
            return Ok(result);
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto dto)
        {
            await _authService.RevokeRefreshTokenAsync(dto.RefreshToken);
            return Ok(new { Message = "Logged out successfully." });
        }
    }
}
