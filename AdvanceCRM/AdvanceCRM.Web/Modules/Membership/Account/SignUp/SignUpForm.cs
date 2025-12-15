using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.Membership
{
    [FormScript("Membership.SignUp")]
    public class SignUpForm
    {
        [Hidden]
        public string Plan { get; set; }

        [Required(true), Placeholder("Company")]
        public string Company { get; set; }

    // Show expected suffix to guide user (domain will be composed as subdomain.bizpluserp.com)
    [Required(true), Placeholder("Workspace (e.g. mycompany)")]
        public string Subdomain { get; set; }

        [Required(true), Placeholder("Full name")]
        public string DisplayName { get; set; }

        [EmailEditor, Required(true), Placeholder("Email")]
        public string Email { get; set; }

        [EmailEditor, Required(true), Placeholder("Confirm email")]
        public string ConfirmEmail { get; set; }

    [Required(true), Placeholder("Mobile number")]
        public string MobileNumber { get; set; }

    [Hidden]
    public string AreaCode { get; set; }

        [Hidden]
        public string Password { get; set; }

        [Hidden]
        public string ConfirmPassword { get; set; }

        [Hidden]
        public string PaymentOrderId { get; set; }

        [Hidden]
        public string PaymentId { get; set; }

        [Hidden]
        public string PaymentSignature { get; set; }

        [Hidden]
        public string PaymentAmount { get; set; }

        [Hidden]
        public string PaymentCurrency { get; set; }

        [Hidden]
        public int? Users { get; set; }

        [Hidden]
        public string CouponCode { get; set; }
    }
}
