using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AdvanceCRM.Membership
{
    public interface IRazorpayOrderService
    {
        bool IsEnabled { get; }

        string KeyId { get; }

        string Currency { get; }

        IReadOnlyDictionary<string, int> PlanAmounts { get; }

        int GetAmountForPlan(string plan);

        Task<RazorpayOrderResult> CreateOrderAsync(string plan, CancellationToken cancellationToken = default);

        bool VerifySignature(string orderId, string paymentId, string signature);
    }

    public class RazorpayOrderResult
    {
        public string Id { get; set; }

        public int Amount { get; set; }

        public string Currency { get; set; }
    }

    public class RazorpayOrderService : IRazorpayOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly RazorpaySettings _settings;

        public RazorpayOrderService(HttpClient httpClient, Microsoft.Extensions.Options.IOptions<RazorpaySettings> options)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = options?.Value ?? new RazorpaySettings();

            _httpClient.BaseAddress = new Uri("https://api.razorpay.com/v1/");

            if (IsEnabled)
            {
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.KeyId}:{_settings.KeySecret}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            }
        }

        public bool IsEnabled => _settings?.IsConfigured == true;

        public string KeyId => _settings?.KeyId ?? string.Empty;

        public string Currency => string.IsNullOrWhiteSpace(_settings?.Currency) ? "INR" : _settings.Currency;

        public IReadOnlyDictionary<string, int> PlanAmounts => _settings?.Plans ?? new Dictionary<string, int>();

        public int GetAmountForPlan(string plan)
        {
            if (string.IsNullOrWhiteSpace(plan) || PlanAmounts == null)
                return 0;

            return PlanAmounts.TryGetValue(plan, out var amount)
                ? amount
                : 0;
        }

        public async Task<RazorpayOrderResult> CreateOrderAsync(string plan, CancellationToken cancellationToken = default)
        {
            if (!IsEnabled)
                throw new InvalidOperationException("Razorpay integration has not been configured.");

            if (string.IsNullOrWhiteSpace(plan))
                throw new ArgumentNullException(nameof(plan));

            var amount = GetAmountForPlan(plan);
            if (amount <= 0)
                throw new ValidationError("RazorpayAmountMissing", "Plan", "No payment amount configured for the selected plan.");

            var payload = new
            {
                amount,
                currency = Currency,
                receipt = $"signup_{Guid.NewGuid():N}",
                payment_capture = 1,
                notes = new { plan }
            };

            using var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            using var response = await _httpClient.PostAsync("orders", content, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<RazorpayOrderResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null || string.IsNullOrWhiteSpace(result.Id))
                throw new InvalidOperationException("Unable to create a payment order with Razorpay.");

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
    }
}
