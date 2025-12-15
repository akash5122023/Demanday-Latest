using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.Membership
{
    [FormScript("Membership.SignUp")]
    public class SignUpForm
    {
        [DisplayName("Plan"), ReadOnly(true)]
        public string Plan { get; set; }

        [Required(true), Placeholder("Company")]
        public string Company { get; set; }

        [Required(true), Placeholder("Full name")]
        public string DisplayName { get; set; }

        [EmailEditor, Required(true), Placeholder("Email")]
        public string Email { get; set; }

        [EmailEditor, Required(true), Placeholder("Confirm email")]
        public string ConfirmEmail { get; set; }

        [Required(true), Placeholder("Mobile number")]
        public string MobileNumber { get; set; }

        [PasswordEditor, Required(true), Placeholder("Password")]
        public string Password { get; set; }

        [PasswordEditor, Required(true), Placeholder("Confirm password")]
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
    }
}
