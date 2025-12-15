using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdvanceCRM.Marketing
{
    [Route("api/public/trial-settings")]
    public class TrialSettingsController : Controller
    {
        private readonly IConfiguration configuration;

        public TrialSettingsController(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult Get()
        {
            var trialSection = configuration.GetSection("TrialSettings");
            if (!trialSection.Exists())
            {
                return Ok(new TrialSettingsResponse
                {
                    DefaultDays = null,
                    Plans = new Dictionary<string, int>()
                });
            }

            int? defaultDays = ParseDays(trialSection.GetValue<string>("DefaultDays"));
            var plansSection = trialSection.GetSection("Plans");
            var planDictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var child in plansSection.GetChildren())
            {
                var rawValue = child.Value;
                var planDays = ParseDays(rawValue);
                if (planDays.HasValue)
                    planDictionary[child.Key] = planDays.Value;
            }

            return Ok(new TrialSettingsResponse
            {
                DefaultDays = defaultDays,
                Plans = planDictionary
            });
        }

        private static int? ParseDays(string? rawValue)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
                return null;

            if (int.TryParse(rawValue, out var parsed) && parsed > 0)
                return parsed;

            return null;
        }

        private sealed class TrialSettingsResponse
        {
            public int? DefaultDays { get; set; }
            public IDictionary<string, int> Plans { get; set; } = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
