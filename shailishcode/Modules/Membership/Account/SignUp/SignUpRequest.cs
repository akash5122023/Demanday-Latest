using Serenity.Services;
using System;

namespace AdvanceCRM.Membership
{
    public class SignUpRequest : ServiceRequest
    {
        public string Plan { get; set; }
        public string Company { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string PaymentOrderId { get; set; }
        public string PaymentId { get; set; }
        public string PaymentSignature { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentCurrency { get; set; }
    }
}
