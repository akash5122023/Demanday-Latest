using System.Collections.Generic;

namespace AdvanceCRM.Membership
{
    public class RazorpaySettings
    {
        public const string SectionKey = "Razorpay";

        public string KeyId { get; set; }

        public string KeySecret { get; set; }

        public string Currency { get; set; } = "INR";

        public Dictionary<string, int> Plans { get; set; } = new();

        public bool IsConfigured =>
            !string.IsNullOrWhiteSpace(KeyId) &&
            !string.IsNullOrWhiteSpace(KeySecret);
    }

    public class RazorpayClientConfig
    {
        public bool Enabled { get; set; }

        public string Key { get; set; }

        public string Currency { get; set; }

        public Dictionary<string, int> Plans { get; set; } = new();
    }
}
