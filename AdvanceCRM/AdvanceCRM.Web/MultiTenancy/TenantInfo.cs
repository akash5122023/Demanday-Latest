using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvanceCRM.MultiTenancy
{
    /// <summary>
    /// Represents a tenant resolved from the current request context.
    /// </summary>
    public class TenantInfo
    {
        public int TenantId { get; set; }

        public string? Subdomain { get; set; }

        public string? DbName { get; set; }

        /// <summary>
        /// Cached connection string pointing to the tenant database.
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// Display name of the tenant / company resolved for the current request.
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// Name of the active subscription plan.
        /// </summary>
        public string? Plan { get; set; }

        /// <summary>
        /// Modules assigned to the tenant via the subscription plan.
        /// </summary>
        public ISet<string> Modules { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the tenant has access to a specific module.
        /// </summary>
        public bool HasModule(string moduleName)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
                return false;

            return Modules.Contains(moduleName);
        }

        /// <summary>
        /// Determines whether module filtering should be applied for the current tenant.
        /// Super administrators and tenants without assigned modules should see all modules.
        /// </summary>
        public bool ShouldFilterModules()
        {
            if (IsSuperAdmin())
                return false;

            return Modules != null && Modules.Count > 0;
        }

        /// <summary>
        /// Indicates whether the current tenant represents the super administrator tenant.
        /// </summary>
        public bool IsSuperAdmin()
        {
            var subdomain = Subdomain?.Trim();
            if (string.IsNullOrEmpty(subdomain))
                return false;

            return string.Equals(subdomain, "demo", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Parses a module list into a case-insensitive set.
        /// </summary>
        public static ISet<string> ParseModules(string? modules)
        {
            if (string.IsNullOrWhiteSpace(modules))
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var values = modules
                .Split(new[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x));

            return new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
        }
    }
}
