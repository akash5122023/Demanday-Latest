namespace AdvanceCRM.Membership
{
    using System;
    using System.Collections.Generic;

    public class UserProfilePageModel
    {
        public string? TenantName { get; set; }

        public string? DisplayName { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? CurrentPlanName { get; set; }

        public bool IsSuperAdmin { get; set; }

        public DateTime? LicenseStartDate { get; set; }

        public DateTime? LicenseEndDate { get; set; }

        public int? DaysRemaining { get; set; }

        public int? PurchasedUsers { get; set; }

        public int ActiveUserCount { get; set; }

        public int TotalActiveUsers { get; set; }

        public int TotalInactiveUsers { get; set; }

        public int TotalTenantCount { get; set; }

        public int ActiveDomainCount { get; set; }

        public int ExpiringDomainCount { get; set; }

        public int ExpiredDomainCount { get; set; }

        public int ExpiringSoonWindowDays { get; set; }

        public decimal? PurchaseAmount { get; set; }

        public string? PurchaseCurrency { get; set; }

        public string? PaymentOrderId { get; set; }

        public string? PaymentId { get; set; }

        public IReadOnlyList<string> ActiveModules { get; set; } = Array.Empty<string>();

        public IReadOnlyList<ModuleOption> AvailableModules { get; set; } = Array.Empty<ModuleOption>();

        public IReadOnlyList<PlanSummary> AvailablePlans { get; set; } = Array.Empty<PlanSummary>();

        public bool CanPurchaseModules { get; set; }

        public IReadOnlyList<DomainStatusSummary> ExpiringDomains { get; set; } = Array.Empty<DomainStatusSummary>();

        public IReadOnlyList<DomainStatusSummary> ActiveDomains { get; set; } = Array.Empty<DomainStatusSummary>();

        public IReadOnlyList<DomainStatusSummary> ExpiredDomains { get; set; } = Array.Empty<DomainStatusSummary>();

        public class ModuleOption
        {
            public string Key { get; set; } = string.Empty;

            public string Title { get; set; } = string.Empty;

            public bool IsActive { get; set; }

            public decimal? Price { get; set; }

            public string? Currency { get; set; }
        }

        public class PlanSummary
        {
            public int? Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public decimal? PricePerUser { get; set; }

            public string Currency { get; set; } = string.Empty;

            public int? UserLimit { get; set; }

            public int? TrialDays { get; set; }

            public bool IsCurrent { get; set; }

            public IReadOnlyList<string> Modules { get; set; } = Array.Empty<string>();
        }

        public class DomainStatusSummary
        {
            public string TenantName { get; set; } = string.Empty;

            public string? Subdomain { get; set; }

            public string? PlanName { get; set; }

            public DateTime? LicenseEndDate { get; set; }

            public int? DaysRemaining { get; set; }

            public string Status { get; set; } = string.Empty;
        }
    }
}
