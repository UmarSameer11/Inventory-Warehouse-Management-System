using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WarehouseManagementSystemWeb.Auth;
using WarehouseManagementSystemWeb.Models.Auth;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class AuthApiClient : IAuthApiClient
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        private readonly HttpClient _http;
        private readonly ILogger<AuthApiClient> _logger;

        public AuthApiClient(HttpClient http, ILogger<AuthApiClient> logger)
        {
            _http = http;
            _logger = logger;
        }

        // ---- anonymous endpoints (no Bearer header is attached) ----

        public Task<AuthResult<AuthTokenResponse>> LoginAsync(string email, string password, CancellationToken ct = default) =>
            SendAsync<AuthTokenResponse>(HttpMethod.Post, "/api/Account/Login",
                new { email, password }, anonymous: true, ct);

        public Task<AuthResult<AuthTokenResponse>> RefreshAsync(string refreshToken, CancellationToken ct = default) =>
            SendAsync<AuthTokenResponse>(HttpMethod.Post, "/api/Account/RefreshToken",
                new { refreshToken }, anonymous: true, ct);

        // ---- endpoints that need the logged-in user's access token ----

        public Task<AuthResult<bool>> LogoutAsync(CancellationToken ct = default) =>
            SendVoidAsync(HttpMethod.Post, "/api/Account/Logout", null, ct);

        public Task<AuthResult<bool>> LogoutAllAsync(CancellationToken ct = default) =>
            SendVoidAsync(HttpMethod.Post, "/api/Account/LogoutAll", null, ct);

        public Task<AuthResult<UserProfileModel>> GetProfileAsync(CancellationToken ct = default) =>
            SendAsync<UserProfileModel>(HttpMethod.Get, "/api/Account/Me", null, anonymous: false, ct);

        public Task<AuthResult<List<SessionModel>>> GetSessionsAsync(CancellationToken ct = default) =>
            SendAsync<List<SessionModel>>(HttpMethod.Get, "/api/Account/Sessions", null, anonymous: false, ct);

        public Task<AuthResult<bool>> RevokeSessionAsync(Guid sessionId, CancellationToken ct = default) =>
            SendVoidAsync(HttpMethod.Delete, $"/api/Account/Sessions/{sessionId}", null, ct);

        public Task<AuthResult<bool>> ChangePasswordAsync(ChangePasswordViewModel model, CancellationToken ct = default) =>
            SendVoidAsync(HttpMethod.Post, "/api/Account/ChangePassword", new
            {
                currentPassword = model.CurrentPassword,
                newPassword = model.NewPassword,
                confirmNewPassword = model.ConfirmNewPassword
            }, ct);

        public async Task<AuthResult<UserProfileModel>> RegisterAsync(RegisterUserViewModel model, CancellationToken ct = default)
        {
            var result = await SendAsync<RegisterApiResponse>(HttpMethod.Post, "/api/Account/Registration", new
            {
                name = model.Name,
                email = model.Email,
                password = model.Password,
                confirmPassword = model.ConfirmPassword,
                role = model.Role
            }, anonymous: false, ct);

            return result.Success
                ? AuthResult<UserProfileModel>.Ok(result.Data?.User, result.StatusCode)
                : AuthResult<UserProfileModel>.Fail(result.ErrorMessage!, result.StatusCode);
        }

        // ------------------------------------------------------------------ plumbing

        private async Task<AuthResult<bool>> SendVoidAsync(HttpMethod method, string url, object? body, CancellationToken ct)
        {
            var result = await SendAsync<JsonElement?>(method, url, body, anonymous: false, ct);

            return result.Success
                ? AuthResult<bool>.Ok(true, result.StatusCode)
                : AuthResult<bool>.Fail(result.ErrorMessage!, result.StatusCode);
        }

        private async Task<AuthResult<T>> SendAsync<T>(
            HttpMethod method, string url, object? body, bool anonymous, CancellationToken ct)
        {
            try
            {
                using var request = new HttpRequestMessage(method, url);

                if (body != null)
                {
                    request.Content = JsonContent.Create(body, options: JsonOptions);
                }

                if (anonymous)
                {
                    request.Options.Set(BearerTokenHandler.Anonymous, true);
                }

                using var response = await _http.SendAsync(request, ct);
                var text = await response.Content.ReadAsStringAsync(ct);
                var status = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    T? data = default;

                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        // The API's response-wrapping middleware puts the payload into "Data".
                        data = JsonSerializer.Deserialize<ApiEnvelope<T>>(text, JsonOptions) is { } envelope
                            ? envelope.Data
                            : default;
                    }

                    return AuthResult<T>.Ok(data, status);
                }

                return AuthResult<T>.Fail(ReadErrorMessage(text, response.StatusCode), status);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "API call {Method} {Url} failed", method, url);
                return AuthResult<T>.Fail("Cannot reach the server. Please try again in a moment.", 503);
            }
            catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
            {
                _logger.LogError(ex, "API call {Method} {Url} timed out", method, url);
                return AuthResult<T>.Fail("The server took too long to respond. Please try again.", 504);
            }
        }

        private static string ReadErrorMessage(string body, HttpStatusCode status)
        {
            // The API answers with { statusCode, message, detail, ... } (its ErrorResponse).
            if (!string.IsNullOrWhiteSpace(body))
            {
                try
                {
                    var error = JsonSerializer.Deserialize<ApiErrorModel>(body, JsonOptions);
                    if (!string.IsNullOrWhiteSpace(error?.Message))
                    {
                        return error!.Message!;
                    }
                }
                catch (JsonException)
                {
                    // not JSON - fall through to the generic text
                }
            }

            return status switch
            {
                HttpStatusCode.Unauthorized => "Invalid email or password.",
                HttpStatusCode.Forbidden => "You do not have permission to perform this action.",
                HttpStatusCode.Locked => "This account is temporarily locked. Please try again later.",
                HttpStatusCode.TooManyRequests => "Too many attempts. Please wait a moment and try again.",
                _ => $"The request failed ({(int)status})."
            };
        }

        private sealed class RegisterApiResponse
        {
            public string? Message { get; set; }
            public UserProfileModel? User { get; set; }
        }
    }
}
