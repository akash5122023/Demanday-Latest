using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DapperSql = Dapper.SqlMapper;
using AdvanceCRM.Administration;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Serenity.Data;

namespace AdvanceCRM.Marketing
{
    public interface IRazorpayPlanService
    {
        IList<ProductPlanRow> GetActivePlans();

        ProductPlanRow TryGetActivePlanById(int planId);
    }

    public class RazorpayPlanService : IRazorpayPlanService
    {
        private readonly ISqlConnections sqlConnections;
        private readonly ITenantAccessor tenantAccessor;
    private readonly ILogger<RazorpayPlanService> logger;
    private readonly IConfiguration configuration;
    private readonly bool allowDefaultPlansFallback;

        public RazorpayPlanService(ISqlConnections sqlConnections, ITenantAccessor tenantAccessor, ILogger<RazorpayPlanService> logger, IConfiguration configuration)
        {
            this.sqlConnections = sqlConnections ?? throw new ArgumentNullException(nameof(sqlConnections));
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            allowDefaultPlansFallback = this.configuration.GetValue<bool>("AllowDefaultPlansFallback");
        }

        public IList<ProductPlanRow> GetActivePlans()
        {
            var tenant = tenantAccessor.CurrentTenant;
            logger.LogInformation("[Plans] Loading active product plans for tenant {TenantId} ({Subdomain})", tenant?.TenantId, tenant?.Subdomain);

            // Always attempt to load plans strictly within the current tenant context.
            // We no longer silently fall back to the default (host) database unless an explicit
            // configuration flag AllowDefaultPlansFallback = true is provided. This prevents
            // accidental pricing mismatches where legacy / default plans leak into tenant pricing.
            var plans = TryListActivePlans();

            if (plans == null || plans.Count == 0)
            {
                logger.LogWarning("[Plans] No active product plans found for tenant {TenantId} ({Subdomain}). Fallback enabled? {Fallback}", tenant?.TenantId, tenant?.Subdomain, allowDefaultPlansFallback);

                if (allowDefaultPlansFallback)
                {
                    try
                    {
                        var fallback = FallbackToDefaultPlans(tenant);
                        if (fallback != null && fallback.Count > 0)
                        {
                            logger.LogWarning("[Plans] Using DEFAULT DB fallback plans for tenant {TenantId} ({Subdomain}). Count={Count}", tenant?.TenantId, tenant?.Subdomain, fallback.Count);
                            foreach (var p in fallback)
                                logger.LogInformation("[Plans][Fallback] Plan Id={Id}, Name={Name}, PricePerUser={Price}, IsActive={IsActive}", p.Id, p.Name, p.PricePerUser, p.IsActive);
                            return fallback;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "[Plans] Error while attempting fallback plan load for tenant {TenantId} ({Subdomain}).", tenant?.TenantId, tenant?.Subdomain);
                    }
                }

                return Array.Empty<ProductPlanRow>();
            }

            logger.LogInformation("[Plans] Found {Count} active plans for tenant {TenantId} ({Subdomain}).", plans.Count, tenant?.TenantId, tenant?.Subdomain);
            foreach (var p in plans)
            {
                logger.LogInformation("[Plans] Plan Id={Id}, Name={Name}, PricePerUser={Price}, IsActive={IsActive}", p.Id, p.Name, p.PricePerUser, p.IsActive);
            }

            return plans;
        }

        public ProductPlanRow TryGetActivePlanById(int planId)
        {
            if (planId <= 0)
                return null;

            return GetActivePlans().FirstOrDefault(p => p.Id == planId);
        }

        private IList<ProductPlanRow> TryListActivePlans()
        {
            try
            {
                using var connection = sqlConnections.NewFor<ProductPlanRow>();
                var fields = ProductPlanRow.Fields;

                var plans = connection.List<ProductPlanRow>(q => q
                    .SelectTableFields()
                    .Where(fields.IsActive == 1)
                    .OrderBy(fields.SortOrder)
                    .OrderBy(fields.Id));

                PopulatePlanModules(connection, plans);
                PopulatePlanFeatures(connection, plans);

                return plans;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to list active Razorpay plans for the current tenant context.");
                return Array.Empty<ProductPlanRow>();
            }
        }

        private IList<ProductPlanRow> FallbackToDefaultPlans(TenantInfo tenant)
        {
            if (tenant == null)
                return Array.Empty<ProductPlanRow>();

            var originalTenant = tenantAccessor.CurrentTenant;

            try
            {
                tenantAccessor.CurrentTenant = null;

                var defaultPlans = TryListActivePlans();
                if (defaultPlans.Count == 0)
                    return defaultPlans;

                var tenantPlanName = ResolveTenantPlanName(tenant);
                if (string.IsNullOrWhiteSpace(tenantPlanName))
                    return defaultPlans;

                var filtered = defaultPlans
                    .Where(p => !string.IsNullOrWhiteSpace(p.Name) &&
                        string.Equals(p.Name.Trim(), tenantPlanName, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                return filtered.Count > 0 ? filtered : defaultPlans;
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }
        }

        private void PopulatePlanModules(IDbConnection connection, IList<ProductPlanRow> plans)
        {
            if (plans == null || plans.Count == 0)
                return;

            var planIds = plans
                .Where(p => p?.Id != null && p.Id.Value > 0)
                .Select(p => p.Id.Value)
                .Distinct()
                .ToList();

            if (planIds.Count == 0)
                return;

                        var records = DapperSql.Query<PlanModuleRecord>(
                                connection,
                                @"SELECT ppm.PlanId, ppm.ModuleId, pm.DisplayName
                                    FROM ProductPlanModules ppm
                                    INNER JOIN ProductModules pm ON pm.Id = ppm.ModuleId
                                    WHERE ppm.PlanId IN @PlanIds AND pm.IsActive = 1
                                    ORDER BY ppm.PlanId, COALESCE(pm.SortOrder, 2147483647), pm.DisplayName",
                                new { PlanIds = planIds }).ToList();

            if (records.Count == 0)
                return;

            var lookup = records
                .GroupBy(x => x.PlanId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var plan in plans)
            {
                if (plan?.Id == null)
                    continue;

                if (!lookup.TryGetValue(plan.Id.Value, out var moduleRecords))
                {
                    plan.ModuleList = new List<int>();
                    plan.ModuleNames = new List<string>();
                    continue;
                }

                var moduleIds = new List<int>();
                var moduleNames = new List<string>();

                foreach (var record in moduleRecords)
                {
                    if (!moduleIds.Contains(record.ModuleId))
                        moduleIds.Add(record.ModuleId);

                    var name = (record.DisplayName ?? string.Empty).Trim();
                    if (name.Length > 0 && !moduleNames.Any(x => string.Equals(x, name, StringComparison.OrdinalIgnoreCase)))
                        moduleNames.Add(name);
                }

                plan.ModuleList = moduleIds;
                plan.ModuleNames = moduleNames;
            }
        }

        private sealed class PlanModuleRecord
        {
            public int PlanId { get; set; }

            public int ModuleId { get; set; }

            public string DisplayName { get; set; }
        }

        private void PopulatePlanFeatures(IDbConnection connection, IList<ProductPlanRow> plans)
        {
            if (plans == null || plans.Count == 0)
                return;

            var planIds = plans
                .Where(p => p?.Id != null && p.Id.Value > 0)
                .Select(p => p.Id.Value)
                .Distinct()
                .ToList();

            if (planIds.Count == 0)
                return;

            var records = DapperSql.Query<PlanFeatureRecord>(
                    connection,
                    @"SELECT ppdf.PlanId, ppdf.FeatureId, df.Name
                        FROM ProductPlanDefaultFeatures ppdf
                        INNER JOIN DefaultFeatures df ON df.Id = ppdf.FeatureId
                        WHERE ppdf.PlanId IN @PlanIds
                        ORDER BY ppdf.PlanId, df.Name",
                    new { PlanIds = planIds }).ToList();

            if (records.Count == 0)
            {
                foreach (var plan in plans)
                {
                    plan.FeatureList = new List<int>();
                    plan.FeatureNames = new List<string>();
                }

                return;
            }

            var lookup = records
                .GroupBy(x => x.PlanId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var plan in plans)
            {
                if (plan?.Id == null)
                    continue;

                if (!lookup.TryGetValue(plan.Id.Value, out var featureRecords))
                {
                    plan.FeatureList = new List<int>();
                    plan.FeatureNames = new List<string>();
                    continue;
                }

                var featureIds = new List<int>();
                var featureNames = new List<string>();

                foreach (var record in featureRecords)
                {
                    if (!featureIds.Contains(record.FeatureId))
                        featureIds.Add(record.FeatureId);

                    var name = (record.Name ?? string.Empty).Trim();
                    if (name.Length > 0 && !featureNames.Any(x => string.Equals(x, name, StringComparison.OrdinalIgnoreCase)))
                        featureNames.Add(name);
                }

                plan.FeatureList = featureIds;
                plan.FeatureNames = featureNames;
            }
        }

        private sealed class PlanFeatureRecord
        {
            public int PlanId { get; set; }

            public int FeatureId { get; set; }

            public string Name { get; set; }
        }

        private string ResolveTenantPlanName(TenantInfo tenant)
        {
            if (tenant == null)
                return null;

            try
            {
                using var connection = sqlConnections.NewByKey("Default");
                var fields = TenantRow.Fields;

                TenantRow tenantRow = null;

                if (tenant.TenantId > 0)
                {
                    tenantRow = connection.TryFirst<TenantRow>(q => q
                        .Select(fields.Plan)
                        .Where(fields.TenantId == tenant.TenantId));
                }

                if (tenantRow == null && !string.IsNullOrWhiteSpace(tenant.Subdomain))
                {
                    var subdomain = tenant.Subdomain.Trim();
                    tenantRow = connection.TryFirst<TenantRow>(q => q
                        .Select(fields.Plan)
                        .Where(fields.Subdomain == subdomain));
                }

                var planName = tenantRow?.Plan;
                return string.IsNullOrWhiteSpace(planName) ? null : planName.Trim();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to resolve tenant plan metadata for tenant {TenantId} ({Subdomain}).", tenant.TenantId, tenant.Subdomain);
                return null;
            }
        }
    }
}
