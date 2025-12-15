using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AdvanceCRM.Settings;
using AdvanceCRM.Membership;
using AdvanceCRM.MultiTenancy; // added for ITenantAccessor

namespace AdvanceCRM.Marketing
{
    [Route("api/public/plan-pricing")]
    public class PlanPricingController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IRazorpayPlanService planService;
        private readonly ITenantAccessor tenantAccessor; // to force central (demo) DB

        public PlanPricingController(IConfiguration configuration, IRazorpayPlanService planService, ITenantAccessor tenantAccessor)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.planService = planService ?? throw new ArgumentNullException(nameof(planService));
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
        }

        [HttpGet]
        // In single-tenant deployments public plan pricing is disabled.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Get()
        {
            return NotFound();
        }

        private sealed class PlanPricingResponse
        {
            public string CurrencySymbol { get; set; }

            public string CurrencyCode { get; set; }

            public IList<PlanPricingItem> Plans { get; } = new List<PlanPricingItem>();
        }

        private sealed class PlanPricingItem
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal PricePerUser { get; set; }

            public int? SortOrder { get; set; }

            public IList<string> Modules { get; } = new List<string>();

            public IList<string> Features { get; } = new List<string>();

            public int? NonOperationalUsers { get; set; }

            public string BadgeLabel { get; set; }

            public bool? BadgeHighlight { get; set; }
        }

        private static string ResolveBadgeLabel(PlanBadgeLabel? badge)
        {
            if (badge == null || badge == PlanBadgeLabel.None)
                return null;

            return GetEnumDescription(badge.Value);
        }

        private static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);
            if (field == null)
                return value.ToString();

            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .FirstOrDefault();

            return attribute?.Description ?? value.ToString();
        }

        private string ResolveCurrencyCode(IList<ProductPlanRow> plans)
        {
            var planCurrency = plans?
                .Where(p => !string.IsNullOrWhiteSpace(p.Currency))
                .Select(p => p.Currency.Trim())
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(planCurrency))
                return planCurrency.ToUpperInvariant();

            var configuredCurrency = configuration?.GetSection(RazorpaySettings.SectionKey)?.GetValue<string>("Currency");
            if (!string.IsNullOrWhiteSpace(configuredCurrency))
                return configuredCurrency.Trim().ToUpperInvariant();

            return "INR";
        }

        private static string ResolveCurrencySymbol(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                return "₹";

            try
            {
                var region = CultureInfo
                    .GetCultures(CultureTypes.SpecificCultures)
                    .Select(c => new RegionInfo(c.LCID))
                    .FirstOrDefault(r => string.Equals(r.ISOCurrencySymbol, currencyCode, StringComparison.OrdinalIgnoreCase));

                if (region != null)
                    return region.CurrencySymbol;
            }
            catch
            {
            }

            return currencyCode;
        }
    }
}
