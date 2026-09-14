using System.Text.Json;
using WarehouseManagementSystemWeb.Services.Interfaces;

namespace WarehouseManagementSystemWeb.Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient; 
        private static readonly JsonSerializerOptions JsonOptions = new() 
        {
            PropertyNameCaseInsensitive = true 
        }; 
        public ApiService(HttpClient httpClient) 
        { 
            _httpClient = httpClient; 
        }

        public async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default) 
        { 
            using var response = await _httpClient.GetAsync(endpoint, cancellationToken); 
            await EnsureSuccessAsync(response); 
            return await ReadResponseAsync<T>(response, cancellationToken); 
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default) 
        {
            using var response = await _httpClient.PostAsJsonAsync(endpoint, request, JsonOptions, cancellationToken); 
            await EnsureSuccessAsync(response); 
            return await ReadResponseAsync<TResponse>(response, cancellationToken); 
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default) 
        {
            using var response = await _httpClient.PutAsJsonAsync(endpoint, request, JsonOptions, cancellationToken); 
            await EnsureSuccessAsync(response); 
            return await ReadResponseAsync<TResponse>(response, cancellationToken); 
        }

        public async Task<bool> DeleteAsync(string endpoint, CancellationToken cancellationToken = default) 
        {
            using var response = await _httpClient.DeleteAsync(endpoint, cancellationToken); 
            await EnsureSuccessAsync(response); return true;
        }


        private static async Task EnsureSuccessAsync(HttpResponseMessage response) 
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            var errorContent = await response.Content.ReadAsStringAsync(); 
            var statusCode = (int)response.StatusCode;
            var message = string.IsNullOrWhiteSpace(errorContent) ? $"API request failed with status code {statusCode}." : errorContent; 
            throw new HttpRequestException($"API request failed. " + $"StatusCode: {statusCode} ({response.StatusCode}). " + $"Response: {message}"); 
        }

        private static async Task<T?> ReadResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken) 
        {
            if (response.Content == null) 
            {
                return default; 
            }
            var content = await response.Content.ReadAsStringAsync(cancellationToken); 
            if (string.IsNullOrWhiteSpace(content)) 
            {
                return default;
            } 
            return JsonSerializer.Deserialize<T>(content, JsonOptions);
        }
    }
}
    

