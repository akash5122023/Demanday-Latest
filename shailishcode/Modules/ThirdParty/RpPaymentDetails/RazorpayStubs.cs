namespace Razorpay.Api
{
    using System.Collections.Generic;

    public class RazorpayClient
    {
        public RazorpayClient(string key, string secret)
        {
        }

        public Payment Payment => new Payment();
    }

    public class Payment
    {
        public Dictionary<string, object> Attributes { get; } = new();

        public List<Payment> All(Dictionary<string, object> options) => new List<Payment>();

        public Payment Capture(Dictionary<string, object> options) => new Payment();
    }
}
