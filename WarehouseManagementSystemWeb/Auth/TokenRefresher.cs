using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using WarehouseManagementSystemWeb.Models.Auth;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Auth
{
    public interface ITokenRefresher
    {
        Task<AuthResult<AuthTokenResponse>> RefreshAsync(string refreshToken, CancellationToken ct);
    }

    /// <summary>
    /// Exchanges a refresh token for a new token pair.
    ///
    /// The API rotates refresh tokens and treats a second use of an old one as theft (it ends the whole session).
    /// A page load fires many parallel requests (HTML, css, js, images) and each of them sees the same
    /// expired access token, so without protection they would all try to refresh at once and kill the session.
    /// This class lets ONE request call the API; the others wait and reuse the same result.
    /// (In-memory: fine for a single web server. For several servers use a shared cache or sticky sessions.)
    /// </summary>
    public class TokenRefresher : ITokenRefresher
    {
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> Gates = new();

        private static readonly TimeSpan SuccessCacheTime = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan FailureCacheTime = TimeSpan.FromSeconds(5);

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMemoryCache _cache;

        public TokenRefresher(IServiceScopeFactory scopeFactory, IMemoryCache cache)
        {
            _scopeFactory = scopeFactory;
            _cache = cache;
        }

        public async Task<AuthResult<AuthTokenResponse>> RefreshAsync(string refreshToken, CancellationToken ct)
        {
            var key = "wms-refresh:" + Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));

            if (_cache.TryGetValue(key, out AuthResult<AuthTokenResponse>? cached) && cached != null)
            {
                return cached;
            }

            var gate = Gates.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
            await gate.WaitAsync(ct);

            try
            {
                if (_cache.TryGetValue(key, out cached) && cached != null)
                {
                    return cached;
                }

                using var scope = _scopeFactory.CreateScope();
                var client = scope.ServiceProvider.GetRequiredService<IAuthApiClient>();

                var result = await client.RefreshAsync(refreshToken, ct);

                // Only definitive answers are cached. A network error (503/504) is retried on the next request.
                if (result.StatusCode is not (503 or 504))
                {
                    _cache.Set(key, result, result.Success ? SuccessCacheTime : FailureCacheTime);
                }

                return result;
            }
            finally
            {
                gate.Release();
                Gates.TryRemove(key, out _);
            }
        }
    }
}
