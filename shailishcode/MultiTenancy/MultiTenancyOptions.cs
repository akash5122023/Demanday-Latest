using System;
using System.Collections.Generic;
namespace AdvanceCRM.MultiTenancy
{
    /// <summary>
    /// Options controlling how tenant specific resources are discovered.
    /// </summary>
    public class MultiTenancyOptions
    {
        public class StaticTenantDefinition
        {
            public string Name { get; set; } = string.Empty;
            public string Database { get; set; } = string.Empty;
            public string Plan { get; set; } = string.Empty;
        }

        public const string SectionKey = "MultiTenancy";

        /// <summary>
        /// Root domain that contains all tenant specific sub domains (e.g. bizpluserp.com).
        /// Requests that do not end with this root domain are ignored.
        /// </summary>
        public string RootDomain { get; set; } = string.Empty;

        /// <summary>
        /// Connection key that should be used when a tenant is not resolved.
        /// </summary>
        public string DefaultConnectionKey { get; set; } = "Default";

        /// <summary>
        /// Connection keys that should be switched to the tenant database once a tenant is resolved.
        /// </summary>
        public string[] ConnectionKeys { get; set; } = new[] { "Default", "Log" };

        /// <summary>
        /// When true a 404 is returned if a subdomain cannot be resolved to a tenant.
        /// </summary>
        public bool EnforceTenant { get; set; }

        public string[] MarketingHosts { get; set; } = Array.Empty<string>();

        public Dictionary<string, StaticTenantDefinition> StaticTenants { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    }
}


