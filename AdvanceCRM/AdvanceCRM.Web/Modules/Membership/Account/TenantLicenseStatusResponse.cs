namespace AdvanceCRM.Membership
{
    using Serenity.Services;
    using System;

    public class TenantLicenseStatusResponse : ServiceResponse
    {
        public bool HasLicense { get; set; }
        public string? Plan { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public int? TotalDays { get; set; }
        public int? RemainingDays { get; set; }
        public bool IsExpired { get; set; }
        public bool IsTrial { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public string? PurchaseCurrency { get; set; }
        public int? PurchasedUsers { get; set; }
        public string? PaymentOrderId { get; set; }
        public string? PaymentId { get; set; }
        public DateTime ServerDateUtc { get; set; }
    }
}
