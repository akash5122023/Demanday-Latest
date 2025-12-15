namespace AdvanceCRM.Membership.Pages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Marketing;
    using AdvanceCRM.Masters;
    using AdvanceCRM.MultiTenancy;
    using AdvanceCRM.Settings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Serenity;
    using Serenity.Abstractions;
    using Serenity.Data;

    [Authorize]
    [Route("Account/Profile")]
    public class UserProfileController : Controller
    {
        private readonly ITenantAccessor tenantAccessor;
        private readonly ISqlConnections sqlConnections;
        private readonly IUserAccessor userAccessor;
        private readonly IUserRetrieveService userRetriever;
        private readonly IRazorpayPlanService? planService;
        private readonly ILogger<UserProfileController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly MultiTenancyOptions multiTenancyOptions;

        public UserProfileController(
            ITenantAccessor tenantAccessor,
            ISqlConnections sqlConnections,
            IUserAccessor userAccessor,
            IUserRetrieveService userRetriever,
            ILogger<UserProfileController> logger,
            IHttpContextAccessor httpContextAccessor,
            IOptions<MultiTenancyOptions> multiTenancyOptions,
            IRazorpayPlanService? planService = null)
        {
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
            this.sqlConnections = sqlConnections ?? throw new ArgumentNullException(nameof(sqlConnections));
            this.userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            this.userRetriever = userRetriever ?? throw new ArgumentNullException(nameof(userRetriever));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            if (multiTenancyOptions == null)
                throw new ArgumentNullException(nameof(multiTenancyOptions));

            this.multiTenancyOptions = multiTenancyOptions.Value ?? new MultiTenancyOptions();
            this.planService = planService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new UserProfilePageModel();

            UserDefinition userDefinition = userAccessor.User?.GetUserDefinition(userRetriever) as UserDefinition;
            if (userDefinition != null)
            {
                model.DisplayName = userDefinition.DisplayName;
                model.Username = userDefinition.Username;
                model.Email = userDefinition.Email;
            }

            var tenant = tenantAccessor.CurrentTenant;
            var showSuperAdminView = ShouldShowSuperAdminView(tenant);

            if (showSuperAdminView)
            {
                model.TenantName = tenant?.CompanyName ?? tenant?.Subdomain ??
                    model.DisplayName ?? model.Username ?? "My Account";
                SafeInvoke(() => LoadSuperAdminOverview(model),
                    "loading super administrator overview");
                return View(MVC.Views.Membership.Profile.UserProfileIndex, model);
            }

            if (tenant == null)
            {
                model.TenantName = model.DisplayName ?? model.Username ?? "My Account";
                return View(MVC.Views.Membership.Profile.UserProfileIndex, model);
            }

            model.TenantName = !string.IsNullOrWhiteSpace(tenant.CompanyName)
                ? tenant.CompanyName
                : (!string.IsNullOrWhiteSpace(tenant.Subdomain)
                    ? tenant.Subdomain
                    : $"Tenant #{tenant.TenantId}");

            SafeInvoke(() => LoadTenantMetadata(tenant.TenantId, model),
                "loading tenant metadata");
            SafeInvoke(() => LoadActiveUserCount(model),
                "loading active user count");
            SafeInvoke(() => LoadModules(model),
                "loading module details");
            SafeInvoke(() => LoadPlans(model),
                "loading available plans");

            if (model.LicenseEndDate.HasValue)
            {
                var days = (int)Math.Ceiling((model.LicenseEndDate.Value.Date - DateTime.UtcNow.Date).TotalDays);
                model.DaysRemaining = days;
            }

            return View(MVC.Views.Membership.Profile.UserProfileIndex, model);
        }

        private bool ShouldShowSuperAdminView(TenantInfo? tenant)
        {
            if (tenant == null)
                return true;

            if (tenant.IsSuperAdmin())
                return true;

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext == null)
                return false;

            var configuredHosts = multiTenancyOptions.SuperAdminHosts ?? Array.Empty<string>();
            if (configuredHosts.Length == 0)
                return false;

            var requestHosts = CollectRequestHosts(httpContext);
            if (requestHosts.Count == 0)
                return false;

            var tenantSubdomain = tenant.Subdomain?.Trim();

            foreach (var candidate in configuredHosts)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                    continue;

                var trimmed = candidate.Trim();
                if (trimmed.Length == 0)
                    continue;

                if (MatchesHostCandidate(trimmed, requestHosts))
                    return true;

                if (!string.IsNullOrEmpty(tenantSubdomain) &&
                    MatchesTenantCandidate(trimmed, tenantSubdomain))
                {
                    return true;
                }
            }

            return false;
        }

        private bool MatchesTenantCandidate(string candidate, string tenantSubdomain)
        {
            if (!string.IsNullOrEmpty(candidate) &&
                string.Equals(candidate, tenantSubdomain, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.IsNullOrEmpty(candidate) || string.IsNullOrEmpty(tenantSubdomain))
                return false;

            if (!candidate.Contains('.', StringComparison.Ordinal))
                return false;

            var rootDomain = multiTenancyOptions.RootDomain?.Trim();
            if (string.IsNullOrEmpty(rootDomain))
                return false;

            rootDomain = rootDomain.TrimStart('.');
            if (!candidate.EndsWith(rootDomain, StringComparison.OrdinalIgnoreCase))
                return false;

            var prefix = candidate[..(candidate.Length - rootDomain.Length)].TrimEnd('.');
            if (string.IsNullOrEmpty(prefix))
                return false;

            if (string.Equals(prefix, tenantSubdomain, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private static bool MatchesHostCandidate(string candidate, IReadOnlyCollection<string> requestHosts)
        {
            if (requestHosts.Count == 0)
                return false;

            if (Uri.TryCreate(candidate, UriKind.Absolute, out var absoluteCandidate))
            {
                var host = absoluteCandidate.Host;
                if (!string.IsNullOrEmpty(host))
                    candidate = host;
            }

            foreach (var host in requestHosts)
            {
                if (string.Equals(candidate, host, StringComparison.OrdinalIgnoreCase))
                    return true;

                if (candidate.Contains(':', StringComparison.Ordinal))
                {
                    var withoutPort = candidate.Split(':')[0];
                    if (string.Equals(withoutPort, host, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        private static List<string> CollectRequestHosts(HttpContext context)
        {
            var hosts = new List<string>();

            void AddHost(string? value)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                foreach (var part in value.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    var candidate = part.Trim();
                    if (string.IsNullOrEmpty(candidate))
                        continue;

                    if (candidate.Contains("://", StringComparison.Ordinal))
                    {
                        if (Uri.TryCreate(candidate, UriKind.Absolute, out var uri) && !string.IsNullOrEmpty(uri.Host))
                            candidate = uri.Host;
                        else
                            continue;
                    }

                    if (candidate.Contains(':', StringComparison.Ordinal))
                    {
                        var beforePort = candidate.Split(':')[0];
                        if (!string.IsNullOrEmpty(beforePort))
                            candidate = beforePort;
                    }

                    if (!hosts.Contains(candidate, StringComparer.OrdinalIgnoreCase))
                        hosts.Add(candidate);
                }
            }

            AddHost(context.Request.Host.Host);
            AddHost(context.Request.Host.Value);
            AddHost(context.Request.Headers["X-Forwarded-Host"].ToString());
            AddHost(context.Request.Headers["Host"].ToString());

            return hosts;
        }

        private void LoadTenantMetadata(int tenantId, UserProfilePageModel model)
        {
            TenantInfo? originalTenant = tenantAccessor.CurrentTenant;

            try
            {
                tenantAccessor.CurrentTenant = null;

                using var connection = sqlConnections.NewByKey("Default");
                var tenantRow = connection.TryById<TenantRow>(tenantId);
                if (tenantRow == null)
                    return;

                model.CurrentPlanName = tenantRow.Plan?.Trim();
                model.CanPurchaseModules = EvaluateModulePurchaseEligibility(tenantRow);
                model.LicenseStartDate = tenantRow.LicenseStartDate;
                model.LicenseEndDate = tenantRow.LicenseEndDate;
                model.PurchasedUsers = tenantRow.PurchasedUsers;
                model.PurchaseAmount = tenantRow.PurchaseAmount;
                model.PurchaseCurrency = tenantRow.PurchaseCurrency;
                model.PaymentOrderId = tenantRow.PaymentOrderId;
                model.PaymentId = tenantRow.PaymentId;

                if (!string.IsNullOrWhiteSpace(tenantRow.Modules))
                {
                    var modules = tenantRow.Modules
                        .Split(new[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    if (modules.Count > 0)
                        model.ActiveModules = modules;
                }
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }
        }

        private void SafeInvoke(Action action, string scope)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed while {Scope} on the profile page", scope);
            }
        }

        private void LoadActiveUserCount(UserProfilePageModel model)
        {
            using var connection = sqlConnections.NewFor<UserRow>();
            var u = UserRow.Fields;
            model.ActiveUserCount = connection.Count<UserRow>(u.IsActive == 1);
        }

        private void LoadSuperAdminOverview(UserProfilePageModel model)
        {
            model.IsSuperAdmin = true;

            TenantInfo? originalTenant = tenantAccessor.CurrentTenant;

            try
            {
                tenantAccessor.CurrentTenant = null;

                using var connection = sqlConnections.NewByKey("Default");
                var userFields = UserRow.Fields;
                model.TotalActiveUsers = connection.Count<UserRow>(userFields.IsActive == 1);
                model.TotalInactiveUsers = connection.Count<UserRow>(userFields.IsActive == 0);

                var tenants = connection.List<TenantRow>() ?? new List<TenantRow>();
                model.TotalTenantCount = tenants.Count;

                var now = DateTime.UtcNow.Date;
                const int expiringWindow = 30;
                model.ExpiringSoonWindowDays = expiringWindow;

                var expiring = new List<UserProfilePageModel.DomainStatusSummary>();
                var active = new List<UserProfilePageModel.DomainStatusSummary>();
                var expired = new List<UserProfilePageModel.DomainStatusSummary>();

                foreach (var tenant in tenants)
                {
                    var tenantName = !string.IsNullOrWhiteSpace(tenant.Name)
                        ? tenant.Name.Trim()
                        : (!string.IsNullOrWhiteSpace(tenant.Subdomain)
                            ? tenant.Subdomain.Trim()
                            : $"Tenant #{tenant.TenantId}");

                    var planName = !string.IsNullOrWhiteSpace(tenant.Plan)
                        ? tenant.Plan.Trim()
                        : null;

                    int? daysRemaining = null;
                    if (tenant.LicenseEndDate.HasValue)
                    {
                        daysRemaining = (int)Math.Ceiling((tenant.LicenseEndDate.Value.Date - now).TotalDays);
                    }

                    var summary = new UserProfilePageModel.DomainStatusSummary
                    {
                        TenantName = tenantName,
                        Subdomain = tenant.Subdomain,
                        PlanName = planName,
                        LicenseEndDate = tenant.LicenseEndDate,
                        DaysRemaining = daysRemaining
                    };

                    if (!tenant.LicenseEndDate.HasValue)
                    {
                        summary.Status = "Active";
                        active.Add(summary);
                        continue;
                    }

                    if (daysRemaining.HasValue && daysRemaining.Value < 0)
                    {
                        summary.Status = "Expired";
                        expired.Add(summary);
                    }
                    else if (daysRemaining.HasValue && daysRemaining.Value <= expiringWindow)
                    {
                        summary.Status = "Expiring Soon";
                        expiring.Add(summary);
                    }
                    else
                    {
                        summary.Status = "Active";
                        active.Add(summary);
                    }
                }

                model.ExpiringDomains = SortDomainSummaries(expiring);

                model.ActiveDomains = SortDomainSummaries(active);

                model.ExpiredDomains = SortDomainSummaries(expired);

                model.ExpiringDomainCount = model.ExpiringDomains.Count;
                model.ActiveDomainCount = model.ActiveDomains.Count;
                model.ExpiredDomainCount = model.ExpiredDomains.Count;
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }
        }

        private static List<UserProfilePageModel.DomainStatusSummary> SortDomainSummaries(
            IEnumerable<UserProfilePageModel.DomainStatusSummary> summaries)
        {
            return summaries
                .OrderByDescending(x => x.LicenseEndDate ?? DateTime.MinValue)
                .ThenByDescending(x => x.DaysRemaining ?? int.MinValue)
                .ThenBy(x => x.TenantName, StringComparer.CurrentCultureIgnoreCase)
                .ToList();
        }

        private void LoadModules(UserProfilePageModel model)
        {
            var activeSet = new HashSet<string>(model.ActiveModules ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);
            var options = new List<UserProfilePageModel.ModuleOption>();

            TenantInfo? originalTenant = tenantAccessor.CurrentTenant;

            var modules = new List<ProductModuleRow>();

            try
            {
                tenantAccessor.CurrentTenant = null;

                using var connection = sqlConnections.NewByKey("Default");
                try
                {
                    modules = connection.List<ProductModuleRow>(q =>
                    {
                        var fld = ProductModuleRow.Fields;
                        q.SelectTableFields();
                        q.OrderBy(fld.SortOrder);
                        q.OrderBy(fld.DisplayName);
                    }) ?? new List<ProductModuleRow>();
                }
                catch (Exception ex) when (ProductModulePricingHelper.IsMissingPricingColumnException(ex))
                {
                    modules = connection.List<ProductModuleRow>(q =>
                    {
                        var fld = ProductModuleRow.Fields;
                        q.Select(fld.Id);
                        q.Select(fld.Name);
                        q.Select(fld.DisplayName);
                        q.Select(fld.Description);
                        q.Select(fld.IsActive);
                        q.Select(fld.SortOrder);
                        q.Select(fld.CreatedOn);
                        q.Select(fld.CreatedBy);
                        q.Select(fld.ModifiedOn);
                        q.Select(fld.ModifiedBy);
                        q.OrderBy(fld.SortOrder);
                        q.OrderBy(fld.DisplayName);
                    }) ?? new List<ProductModuleRow>();
                }
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }

            foreach (var module in modules)
            {
                if (module == null)
                    continue;

                var key = !string.IsNullOrWhiteSpace(module.Name)
                    ? module.Name.Trim()
                    : (!string.IsNullOrWhiteSpace(module.DisplayName)
                        ? module.DisplayName.Trim()
                        : module.Id?.ToString() ?? string.Empty);

                if (string.IsNullOrWhiteSpace(key))
                    continue;

                var title = !string.IsNullOrWhiteSpace(module.DisplayName)
                    ? module.DisplayName.Trim()
                    : key;

                var isActive = activeSet.Contains(key) || activeSet.Contains(title);
                options.Add(new UserProfilePageModel.ModuleOption
                {
                    Key = key,
                    Title = title,
                    IsActive = isActive,
                    Price = module.Price,
                    Currency = string.IsNullOrWhiteSpace(module.Currency) ? null : module.Currency.Trim()
                });
            }

            foreach (var extra in activeSet)
            {
                if (options.Any(x => string.Equals(x.Key, extra, StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(x.Title, extra, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                options.Add(new UserProfilePageModel.ModuleOption
                {
                    Key = extra,
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(extra.ToLowerInvariant()),
                    IsActive = true
                });
            }

            model.AvailableModules = options
                .OrderByDescending(x => x.IsActive)
                .ThenBy(x => x.Title, StringComparer.CurrentCultureIgnoreCase)
                .ToList();
        }

        private void LoadPlans(UserProfilePageModel model)
        {
            if (planService == null)
                return;

            TenantInfo? originalTenant = tenantAccessor.CurrentTenant;
            IList<Settings.ProductPlanRow> plans;

            try
            {
                tenantAccessor.CurrentTenant = null;
                plans = planService.GetActivePlans() ?? Array.Empty<ProductPlanRow>();
            }
            catch
            {
                plans = Array.Empty<ProductPlanRow>();
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }

            if (plans.Count == 0)
                return;

            var summaries = plans
                .Where(p => p != null)
                .Select(p => new UserProfilePageModel.PlanSummary
                {
                    Id = p.Id,
                    Name = p.Name ?? string.Empty,
                    PricePerUser = p.PricePerUser,
                    Currency = p.Currency ?? string.Empty,
                    UserLimit = p.UserLimit,
                    TrialDays = p.TrialDays,
                    IsCurrent = !string.IsNullOrWhiteSpace(model.CurrentPlanName) &&
                        string.Equals(p.Name?.Trim(), model.CurrentPlanName.Trim(), StringComparison.OrdinalIgnoreCase),
                    Modules = (p.ModuleNames ?? new List<string>())
                        .Select(x => x?.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList()
                })
                .OrderBy(p => p.PricePerUser ?? 0m)
                .ThenBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            model.AvailablePlans = summaries;
        }

        private static bool EvaluateModulePurchaseEligibility(TenantRow tenantRow)
        {
            if (tenantRow == null)
                return false;

            var planName = tenantRow.Plan?.Trim();
            if (string.IsNullOrWhiteSpace(planName))
                return false;

            var normalized = planName.ToLowerInvariant();
            if (normalized.Contains("trial") || normalized.Contains("demo") || normalized.Contains("free"))
                return false;

            return true;
        }

        private static string GetEnumDescription(Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            if (member != null)
            {
                var attribute = member.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Description))
                    return attribute.Description;
            }

            return value.ToString();
        }
    }
}
