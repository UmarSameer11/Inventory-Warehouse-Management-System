using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WarehouseManagementSystemApi.Common.Constant;
using WarehouseManagementSystemApi.Common.Settings;
using WarehouseManagementSystemApi.Models.Auth;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwt;

        public TokenService(IOptions<JwtSettings> jwtOptions)
        {
            _jwt = jwtOptions.Value;
        }

        public (string token, DateTime expiresAtUtc) GenerateAccessToken(AppUser user, IEnumerable<string> roles, Guid sessionId)
        {
            var now = DateTime.UtcNow;
            var expiresAtUtc = now.AddMinutes(_jwt.AccessTokenExpiryMinutes);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(AuthClaims.SessionId, sessionId.ToString()),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.Name, user.Name ?? user.UserName ?? string.Empty)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                notBefore: now,
                expires: expiresAtUtc,
                signingCredentials: credentials);

            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAtUtc);
        }

        public string GenerateRefreshToken()
        {
            return WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(64));
        }

        public string HashToken(string token)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
        }
    }
}
