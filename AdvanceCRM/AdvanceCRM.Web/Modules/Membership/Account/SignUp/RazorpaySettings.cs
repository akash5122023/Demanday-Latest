using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AdvanceCRM.Membership
{
    public class RazorpaySettings
    {
        public const string SectionKey = "Razorpay";

        public string KeyId { get; set; }

        public string KeySecret { get; set; }

        public string Currency { get; set; } = "INR";

        public bool IsConfigured =>
            !string.IsNullOrWhiteSpace(KeyId) &&
            !string.IsNullOrWhiteSpace(KeySecret);
    }

    public class RazorpayClientConfig
    {
        [JsonProperty("enabled")]
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("key")]
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonProperty("currency")]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonProperty("plans")]
        [JsonPropertyName("plans")]
        public IEnumerable<RazorpayClientPlan> Plans { get; set; } = Array.Empty<RazorpayClientPlan>();
    }

    public class RazorpayClientPlan
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("pricePerUser")]
        [JsonPropertyName("pricePerUser")]
        public decimal PricePerUser { get; set; }

        [JsonProperty("trialDays")]
        [JsonPropertyName("trialDays")]
        public int TrialDays { get; set; }

        [JsonProperty("userLimit")]
        [JsonPropertyName("userLimit")]
        public int UserLimit { get; set; }

        [JsonProperty("currency")]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
