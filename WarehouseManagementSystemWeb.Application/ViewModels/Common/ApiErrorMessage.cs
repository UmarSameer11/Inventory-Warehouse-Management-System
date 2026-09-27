using System.Net;
using System.Text.Json;

namespace WarehouseManagementSystemWeb.Common
{
    /// <summary>
    /// Turns the HttpRequestException thrown by ApiService into a message
    /// that can be shown to the user.
    /// </summary>
    public static class ApiErrorMessage
    {
        public static string From(HttpRequestException ex, string fallback)
        {
            // Server errors only carry "Internal Server Error", which is not helpful.
            if (ex.StatusCode == HttpStatusCode.InternalServerError)
            {
                return fallback;
            }

            // ApiService message format:
            // "API request failed. StatusCode: 400 (BadRequest). Response: {json}"
            var raw = ex.Message;
            var marker = raw.IndexOf("Response:", StringComparison.Ordinal);

            if (marker < 0)
            {
                return fallback;
            }

            var body = raw[(marker + "Response:".Length)..].Trim();

            try
            {
                using var doc = JsonDocument.Parse(body);

                if (doc.RootElement.ValueKind == JsonValueKind.Object)
                {
                    foreach (var property in doc.RootElement.EnumerateObject())
                    {
                        if (property.Name.Equals("message", StringComparison.OrdinalIgnoreCase)
                            && property.Value.ValueKind == JsonValueKind.String
                            && !string.IsNullOrWhiteSpace(property.Value.GetString()))
                        {
                            return property.Value.GetString()!;
                        }
                    }
                }
            }
            catch (JsonException)
            {
                // Response was not JSON - use the fallback.
            }

            return fallback;
        }
    }
}
