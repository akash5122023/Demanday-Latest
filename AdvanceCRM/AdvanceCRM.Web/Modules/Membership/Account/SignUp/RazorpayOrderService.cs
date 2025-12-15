using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AdvanceCRM.Razorpay;

namespace AdvanceCRM.Membership
{
    public interface IRazorpayOrderService
    {
        bool IsEnabled { get; }

        string KeyId { get; }

        string Currency { get; }

        Task<RazorpayOrderResult> CreateOrderAsync(int amount, string currency, IDictionary<string, object> notes = null, CancellationToken cancellationToken = default);

        Task<RazorpayOrderDetails> GetOrderAsync(string orderId, CancellationToken cancellationToken = default);

        bool VerifySignature(string orderId, string paymentId, string signature);
    }

    public class RazorpayOrderResult
    {
        public string Id { get; set; }

        public int Amount { get; set; }

        public string Currency { get; set; }
    }

    public class RazorpayOrderDetails : RazorpayOrderResult
    {
        public Dictionary<string, string> Notes { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public string Status { get; set; }

        public string Receipt { get; set; }
    }

    public class RazorpayOrderService : IRazorpayOrderService
    {
        private static readonly JsonSerializerOptions SerializerOptions = RazorpayJsonOptions.CreateDefault();

        private const int ErrorLogSnippetLength = 600;

        private readonly HttpClient _httpClient;
        private readonly RazorpaySettings _settings;
        private readonly ILogger<RazorpayOrderService> _logger;

        public RazorpayOrderService(HttpClient httpClient,
            Microsoft.Extensions.Options.IOptions<RazorpaySettings> options,
            ILogger<RazorpayOrderService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = options?.Value ?? new RazorpaySettings();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Defensive trimming of credentials to avoid accidental whitespace issues
            _settings.KeyId = _settings.KeyId?.Trim();
            _settings.KeySecret = _settings.KeySecret?.Trim();

            _httpClient.BaseAddress = new Uri("https://api.razorpay.com/v1/");

            if (IsEnabled)
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.KeyId}:{_settings.KeySecret}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            }
            else
            {
                _logger.LogDebug("[Razorpay] Service constructed but not enabled (missing KeyId / KeySecret).");
            }

            if (!string.IsNullOrEmpty(_settings.KeyId) && _settings.KeyId.Contains(" "))
            {
                _logger.LogWarning("[Razorpay] KeyId appears to contain whitespace. This may cause authentication failures.");
            }
        }

        public bool IsEnabled => _settings?.IsConfigured == true;

        public string KeyId => _settings?.KeyId ?? string.Empty;

        public string Currency => string.IsNullOrWhiteSpace(_settings?.Currency) ? "INR" : _settings.Currency;

        public Task<RazorpayOrderResult> CreateOrderAsync(int amount, string currency, IDictionary<string, object> notes = null, CancellationToken cancellationToken = default)
        {
            if (!IsEnabled)
                throw new InvalidOperationException("Razorpay integration has not been configured.");

            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            var normalizedCurrency = string.IsNullOrWhiteSpace(currency)
                ? Currency
                : currency.Trim().ToUpperInvariant();

            IDictionary<string, object> notesToSend = notes ?? new Dictionary<string, object>();

            return CreateOrderInternalAsync(amount, normalizedCurrency, notesToSend, cancellationToken);
        }

        public async Task<RazorpayOrderDetails> GetOrderAsync(string orderId, CancellationToken cancellationToken = default)
        {
            if (!IsEnabled)
                throw new InvalidOperationException("Razorpay integration has not been configured.");

            if (string.IsNullOrWhiteSpace(orderId))
                throw new ArgumentNullException(nameof(orderId));

            var trimmedOrderId = orderId.Trim();

            using var response = await _httpClient.GetAsync($"orders/{trimmedOrderId}", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<RazorpayOrderDetails>(json, SerializerOptions);

            if (result == null || string.IsNullOrWhiteSpace(result.Id))
                throw new InvalidOperationException("Unable to retrieve Razorpay order details.");

            if (result.Notes == null)
                result.Notes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            else
                result.Notes = new Dictionary<string, string>(result.Notes, StringComparer.OrdinalIgnoreCase);

            return result;
        }

        private async Task<RazorpayOrderResult> CreateOrderInternalAsync(int amount, string currency, IDictionary<string, object> notes, CancellationToken cancellationToken)
        {
            var receipt = $"signup_{Guid.NewGuid():N}";
            var payload = new Dictionary<string, object>
            {
                ["amount"] = amount,
                ["currency"] = currency,
                ["receipt"] = receipt,
                ["payment_capture"] = 1
            };

            if (notes != null && notes.Count > 0)
                payload["notes"] = notes;

            var serialized = JsonSerializer.Serialize(payload);
            _logger.LogDebug("[Razorpay][CreateOrder] Sending order create request AmountMinor={Amount} Currency={Currency} Receipt={Receipt} NotesKeys={NotesKeys}", amount, currency, receipt, notes?.Count ?? 0);
            using var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync("orders", content, cancellationToken).ConfigureAwait(false);

            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var (errorCode, errorDescription) = RazorpayGatewayException.ParseErrorPayload(responseBody);
                var snippet = BuildSnippet(responseBody);
                _logger.LogWarning("[Razorpay][CreateOrder] Non-success HTTP {Status} while creating order. Code={Code} Description={Description} BodySnippet={Snippet}",
                    (int)response.StatusCode, errorCode, errorDescription, snippet);

                throw new RazorpayGatewayException(response.StatusCode,
                    errorDescription,
                    responseBody,
                    errorCode,
                    errorDescription);
            }

            var result = JsonSerializer.Deserialize<RazorpayOrderResult>(responseBody, SerializerOptions);

            if (result == null || string.IsNullOrWhiteSpace(result.Id))
            {
                _logger.LogError("[Razorpay][CreateOrder] Deserialization failed or missing Id. RawBodyLength={Length}", responseBody?.Length ?? 0);
                throw new InvalidOperationException("Unable to create a payment order with Razorpay.");
            }

            _logger.LogInformation("[Razorpay][CreateOrder] Order created Id={OrderId} AmountMinor={Amount} Currency={Currency}", result.Id, result.Amount, result.Currency);
            return result;
        }

        public bool VerifySignature(string orderId, string paymentId, string signature)
        {
            if (!IsEnabled)
                return false;

            if (string.IsNullOrWhiteSpace(orderId) || string.IsNullOrWhiteSpace(paymentId) || string.IsNullOrWhiteSpace(signature))
                return false;

            var payload = $"{orderId}|{paymentId}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_settings.KeySecret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            var expectedSignature = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();

            return string.Equals(expectedSignature, signature, StringComparison.OrdinalIgnoreCase);
        }

        private static string BuildSnippet(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var trimmed = text.Trim();
            if (trimmed.Length > ErrorLogSnippetLength)
                trimmed = trimmed.Substring(0, ErrorLogSnippetLength) + "…";

            return trimmed;
        }
    }
}
