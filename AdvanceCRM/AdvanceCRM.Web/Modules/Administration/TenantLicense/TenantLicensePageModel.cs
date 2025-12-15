using System;
using System.Collections.Generic;

namespace AdvanceCRM.Administration.Pages
{
    public class TenantLicensePageModel
    {
        public IList<TenantLicenseInfo> Tenants { get; set; } = new List<TenantLicenseInfo>();
        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }
        public bool HasTenants => Tenants != null && Tenants.Count > 0;
        public string? FilterTerm { get; set; }
        public string? FilterStatus { get; set; }
    }

    public class TenantLicenseInfo
    {
        public int TenantId { get; set; }
        public string? Name { get; set; }
        public string? Subdomain { get; set; }
        public string? PortalUrl { get; set; }
        public string? Plan { get; set; }
        public DateTime? LicenseStartDate { get; set; }
        public DateTime? LicenseEndDate { get; set; }
        public int? RemainingDays { get; set; }
        public bool IsExpired { get; set; }
        public decimal? PurchaseAmount { get; set; }
        public string? PurchaseCurrency { get; set; }
        public int? PurchasedUsers { get; set; }
        public string? PrimaryContactEmail { get; set; }
        public string? PrimaryContactName { get; set; }
        public string? PrimaryContactPhone { get; set; }
        public int? PrimaryContactUserId { get; set; }
        public string? PaymentOrderId { get; set; }
        public string? PaymentId { get; set; }
    }
}
