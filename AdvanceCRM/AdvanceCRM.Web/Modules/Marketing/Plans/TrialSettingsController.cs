using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AdvanceCRM.Settings;

namespace AdvanceCRM.Marketing
{
    [Route("api/public/trial-settings")]
    public class TrialSettingsController : Controller
    {
        private readonly IRazorpayPlanService planService;

        public TrialSettingsController(IRazorpayPlanService planService)
        {
            this.planService = planService ?? throw new ArgumentNullException(nameof(planService));
        }

        [HttpGet]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public IActionResult Get()
        {
            // Public trial configuration is disabled in single-tenant deployments.
            return NotFound();
        }

        private sealed class TrialSettingsResponse
        {
            public int? DefaultDays { get; set; }
            public IDictionary<string, int> Plans { get; set; } = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }

    }
}
