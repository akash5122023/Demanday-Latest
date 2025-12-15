using Serenity.Services;

namespace AdvanceCRM.Membership
{
    public class RazorpayCreateOrderRequest : ServiceRequest
    {
        public string Plan { get; set; }
    }

    public class RazorpayCreateOrderResponse : ServiceResponse
    {
        public string OrderId { get; set; }

        public int Amount { get; set; }

        public string Currency { get; set; }

        public string Key { get; set; }

        public bool Success { get; set; }
    }
}
