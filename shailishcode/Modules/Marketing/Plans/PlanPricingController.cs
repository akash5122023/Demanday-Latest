using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdvanceCRM.Marketing
{
    [Route("api/public/plan-pricing")]
    public class PlanPricingController : Controller
    {
        private readonly IConfiguration configuration;

        public PlanPricingController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult Get()
        {
            var pricingSection = configuration.GetSection("PlanPricing");
            if (!pricingSection.Exists())
                return Ok(new PlanPricingResponse());

            var response = new PlanPricingResponse
            {
                CurrencySymbol = TrimOrNull(pricingSection.GetValue<string>("CurrencySymbol")),
                CurrencyCode = TrimOrNull(pricingSection.GetValue<string>("CurrencyCode"))
            };

            var plansSection = pricingSection.GetSection("Plans");
            if (plansSection.Exists())
            {
                foreach (var child in plansSection.GetChildren())
                {
                    var parsed = ParseAmount(child.Value);
                    if (parsed.HasValue)
                        response.Plans[child.Key] = parsed.Value;
                }
            }

            return Ok(response);
        }

        private static decimal? ParseAmount(string rawValue)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
                return null;

            if (decimal.TryParse(rawValue, NumberStyles.Number, CultureInfo.InvariantCulture, out var parsed))
                return parsed;

            return null;
        }

        private static string TrimOrNull(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var trimmed = value.Trim();
            return trimmed.Length == 0 ? null : trimmed;
        }

        private sealed class PlanPricingResponse
        {
            public string CurrencySymbol { get; set; }

            public string CurrencyCode { get; set; }

            public IDictionary<string, decimal> Plans { get; } = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
