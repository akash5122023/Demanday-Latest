using System;
using System.Net;
using System.Text.Json;

namespace AdvanceCRM.Membership
{
    public class RazorpayGatewayException : Exception
    {
        public RazorpayGatewayException(HttpStatusCode statusCode, string message, string responseBody, string errorCode = null, string errorDescription = null, Exception innerException = null)
            : base(string.IsNullOrWhiteSpace(message) ? $"Razorpay request failed with HTTP {(int)statusCode}." : message, innerException)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody ?? string.Empty;
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }

        public HttpStatusCode StatusCode { get; }

        public string ResponseBody { get; }

        public string ErrorCode { get; }

        public string ErrorDescription { get; }

        public string GetBodySnippet(int maxLength = 200)
        {
            if (string.IsNullOrWhiteSpace(ResponseBody))
                return string.Empty;

            var trimmed = ResponseBody.Trim();
            if (trimmed.Length > maxLength)
                trimmed = trimmed.Substring(0, maxLength) + "…";

            return trimmed;
        }

        public static (string code, string description) ParseErrorPayload(string responseBody)
        {
            if (string.IsNullOrWhiteSpace(responseBody))
                return (null, null);

            try
            {
                using var doc = JsonDocument.Parse(responseBody);
                if (doc.RootElement.TryGetProperty("error", out var errorElement) && errorElement.ValueKind == JsonValueKind.Object)
                {
                    string code = null;
                    string description = null;

                    if (errorElement.TryGetProperty("code", out var codeElement) && codeElement.ValueKind == JsonValueKind.String)
                        code = codeElement.GetString();

                    if (errorElement.TryGetProperty("description", out var descElement) && descElement.ValueKind == JsonValueKind.String)
                        description = descElement.GetString();

                    if (string.IsNullOrWhiteSpace(description) && errorElement.TryGetProperty("reason", out var reasonElement) && reasonElement.ValueKind == JsonValueKind.String)
                        description = reasonElement.GetString();

                    return (code, description);
                }
            }
            catch (JsonException)
            {
                // Ignore parse failures – caller will use default messaging.
            }

            return (null, null);
        }
    }
}
