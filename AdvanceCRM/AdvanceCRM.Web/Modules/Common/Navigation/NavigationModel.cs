using AdvanceCRM.Administration;
using AdvanceCRM.Administration.Entities;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Serenity;
using Serenity.Abstractions;
using Serenity.Navigation;
using Serenity.Services;
using Serenity.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvanceCRM.Navigation
{
    public partial class NavigationModel
    {
        private readonly IPermissionService _permissionService;
        private readonly ITypeSource _typeSource;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;
        private readonly ITenantAccessor _tenantAccessor;
        private readonly IRequestContext Context;
        public List<NavigationItem> Items { get; private set; }
        public int[] ActivePath { get; set; }

        public NavigationModel()
            : this(
                Dependency.Resolve<IPermissionService>(),
                Dependency.Resolve<ITypeSource>(),
                Dependency.Resolve<IServiceProvider>(),
                Dependency.Resolve<IHttpContextAccessor>(),
                Dependency.Resolve<IRequestContext>(),
                Dependency.Resolve<IWebHostEnvironment>(),
                Dependency.Resolve<ITenantAccessor>())
        {
        }

        public NavigationModel(
            IPermissionService permissionService,
            ITypeSource typeSource,
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor,
            IRequestContext context,
            IWebHostEnvironment env,
            ITenantAccessor tenantAccessor)
        {
            _permissionService = permissionService;
            _typeSource = typeSource;
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _env = env;
            _tenantAccessor = tenantAccessor;

            var tenant = _tenantAccessor?.CurrentTenant;
            var host = _httpContextAccessor.HttpContext?.Request.Host.Host;
            var modules = GetAllowedModules(tenant);
            var moduleKeySuffix = BuildModuleCacheKeySuffix(modules);
            var tenantKeySuffix = BuildTenantCacheKeySuffix(tenant, host);

            Items = LocalCache.GetLocalStoreOnly(
                "LeftNavigationModel:NavigationItems:" + (Context.User.GetIdentifier() ?? "-1") + tenantKeySuffix + moduleKeySuffix,
                TimeSpan.Zero,
                UserPermissionRow.Fields.GenerationKey,
                () =>
                {
                    var navigationItems = NavigationHelper.GetNavigationItems(
                        _permissionService,
                        _typeSource,
                        _serviceProvider,
                        path => path != null && path.StartsWith("~/", StringComparison.Ordinal)
                            ? ToAbsolute(path)
                            : path).ToList();

                    if (modules != null && modules.Count > 0)
                        FilterNavigationItems(navigationItems, modules);

                    FilterDemoOnlyNavigationItems(navigationItems, tenant, host);

                    return navigationItems;
                });

            SetActivePath();
        }

        private ISet<string>? GetAllowedModules(TenantInfo? tenant)
        {
            if (tenant == null)
                return null;

            if (!tenant.ShouldFilterModules())
                return null;

            return tenant.Modules;
        }

        private static string BuildTenantCacheKeySuffix(TenantInfo? tenant, string? host)
        {
            var segments = new List<string>();

            if (tenant?.TenantId > 0)
            {
                segments.Add("tenant:" + tenant.TenantId);
            }
            else
            {
                var subdomain = tenant?.Subdomain;
                if (!string.IsNullOrWhiteSpace(subdomain))
                    segments.Add("subdomain:" + subdomain.Trim().ToLowerInvariant());
            }

            if (!string.IsNullOrWhiteSpace(host))
                segments.Add("host:" + host.Trim().ToLowerInvariant());

            if (segments.Count == 0)
                return string.Empty;

            return ":" + string.Join(":", segments);
        }

        private static string BuildModuleCacheKeySuffix(ISet<string>? modules)
        {
            if (modules == null || modules.Count == 0)
                return string.Empty;

            var signature = string.Join(",", modules
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .OrderBy(x => x, StringComparer.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(signature))
                return string.Empty;

            return ":modules=" + signature;
        }

        private static void FilterNavigationItems(IList<NavigationItem> items, ISet<string> allowedModules)
        {
            if (items == null)
                return;

            var pathSegments = new List<string>();

            for (var i = items.Count - 1; i >= 0; i--)
            {
                if (!ShouldKeepItem(items[i], allowedModules, pathSegments))
                    items.RemoveAt(i);
            }
        }

        private static bool ShouldKeepItem(NavigationItem item, ISet<string> allowedModules, List<string> pathSegments)
        {
            var title = item?.Title;
            var addedSegment = !string.IsNullOrWhiteSpace(title);

            if (addedSegment)
                pathSegments.Add(title!.Trim());

            if (item?.Children != null && item.Children.Count > 0)
            {
                for (var i = item.Children.Count - 1; i >= 0; i--)
                {
                    if (!ShouldKeepItem(item.Children[i], allowedModules, pathSegments))
                        item.Children.RemoveAt(i);
                }
            }

            var path = BuildPath(pathSegments);
            var moduleKey = ResolveModuleKey(item, path);

            bool keep;
            if (moduleKey == null)
            {
                keep = (item?.Children != null && item.Children.Count > 0) || !string.IsNullOrWhiteSpace(item?.Url);
            }
            else
            {
                keep = allowedModules.Contains(moduleKey);
                if (!keep && item?.Children != null)
                    item.Children.Clear();
            }

            if (addedSegment)
                pathSegments.RemoveAt(pathSegments.Count - 1);

            return keep;
        }

        private void FilterDemoOnlyNavigationItems(IList<NavigationItem> items, TenantInfo? tenant, string? host)
        {
            if (items == null || items.Count == 0)
                return;

            if (tenant?.IsSuperAdmin() == true || string.Equals(host?.Trim(), "demo.bizpluserp.com", StringComparison.OrdinalIgnoreCase))
                return;

            var demoOnlyPaths = new[]
            {
                "Settings/Product Plans",
                "Settings/Coupon Codes",
                "Settings/Module Pricing",
                "Administration/Client License Monitor",
                "Settings/Product Modules",
                "Settings/Default Features"
            };

            foreach (var path in demoOnlyPaths)
                RemoveNavigationItemByPath(items, path);

            var demoOnlyTitles = new[]
            {
                "Product Plans",
                "Coupon Codes",
                "Module Pricing",
                "Client License Monitor",
                "Product Modules",
                "Default Features"
            };

            RemoveNavigationItems(items, item =>
            {
                var title = item?.Title;
                if (string.IsNullOrWhiteSpace(title))
                    return false;

                title = title.Trim();

                foreach (var demoTitle in demoOnlyTitles)
                {
                    if (string.Equals(title, demoTitle, StringComparison.OrdinalIgnoreCase))
                        return true;
                }

                return false;
            });

            var demoOnlyUrlFragments = new[]
            {
                "ProductPlans",
                "CouponCodes",
                "ModulePricing",
                "TenantLicense",
                "ProductModules",
                "DefaultFeatures"
            };

            RemoveNavigationItemsByUrlFragment(items, demoOnlyUrlFragments);
        }

        private static void RemoveNavigationItemsByUrlFragment(IList<NavigationItem> items, string[] fragments)
        {
            if (items == null || items.Count == 0 || fragments == null || fragments.Length == 0)
                return;

            RemoveNavigationItems(items, item =>
            {
                var url = item?.Url;
                if (!string.IsNullOrWhiteSpace(url))
                {
                    foreach (var fragment in fragments)
                    {
                        if (url.IndexOf(fragment, StringComparison.OrdinalIgnoreCase) >= 0)
                            return true;
                    }
                }

                return false;
            });
        }

        private static void RemoveNavigationItems(IList<NavigationItem> items, Func<NavigationItem, bool> predicate)
        {
            if (items == null || predicate == null)
                return;

            for (var i = items.Count - 1; i >= 0; i--)
            {
                var item = items[i];

                if (item.Children != null && item.Children.Count > 0)
                    RemoveNavigationItems(item.Children, predicate);

                if (predicate(item))
                {
                    items.RemoveAt(i);
                }
                else if (item.Children != null && item.Children.Count == 0 && string.IsNullOrWhiteSpace(item.Url))
                {
                    items.RemoveAt(i);
                }
            }
        }

        private static void RemoveNavigationItemByPath(IList<NavigationItem> items, string path)
        {
            if (items == null || items.Count == 0 || string.IsNullOrWhiteSpace(path))
                return;

            var segments = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 0)
                return;

            RemoveNavigationItemByPath(items, segments, 0);
        }

        private static bool RemoveNavigationItemByPath(IList<NavigationItem> items, string[] segments, int index)
        {
            if (items == null || segments == null || segments.Length == 0)
                return false;

            for (var i = items.Count - 1; i >= 0; i--)
            {
                var item = items[i];
                var title = item?.Title;
                if (string.IsNullOrWhiteSpace(title))
                    continue;

                if (!string.Equals(title.Trim(), segments[index], StringComparison.OrdinalIgnoreCase))
                    continue;

                if (index == segments.Length - 1)
                {
                    items.RemoveAt(i);
                    return true;
                }

                if (item.Children != null && item.Children.Count > 0 &&
                    RemoveNavigationItemByPath(item.Children, segments, index + 1))
                {
                    if (item.Children.Count == 0 && string.IsNullOrWhiteSpace(item.Url))
                        items.RemoveAt(i);

                    return true;
                }
            }

            return false;
        }

        private static string BuildPath(List<string> segments)
        {
            if (segments == null || segments.Count == 0)
                return string.Empty;

            return string.Join("/", segments);
        }

        private static readonly Dictionary<string, string> NavigationModulePathMap = new(StringComparer.OrdinalIgnoreCase)
        {
            ["CEO Dashboards"] = "CEO",
            ["CEO Dashboards/Enquiry Dashboard"] = "CEO",
            ["CEO Dashboards/Quotation Dashboard"] = "CEO",
            ["CEO Dashboards/Sales Dashboard"] = "CEO",
            ["CEO Dashboards/Representative Dashboard"] = "CEO",
            ["CEO Dashboards/Representative Performance"] = "CEO",
            ["CEO Dashboards/Target Setting"] = "CEO",
            ["Sales"] = "Sales",
            ["Sales/Sales"] = "Sales",
            ["Sales/Sales Return"] = "Sales",
            ["Sales/Proforma Invoice"] = "Proforma",
            ["Sales/Delivery Challan"] = "Challan",
            ["Reports/Sales"] = "Sales",
            ["Reports/Followups/Sales"] = "Sales",
            ["Reports/Followups/Proforma"] = "Proforma",
            ["Reports/Appointments/Proforma"] = "Proforma",
            ["Purchase"] = "Purchase",
            ["Purchase/Purchase"] = "Purchase",
            ["Purchase/Purchase Return"] = "Purchase",
            ["Purchase/Purchase Order"] = "PurchaseOrder",
            ["Templates/PO Template"] = "PurchaseOrder",
            ["Accounting/Cashbook"] = "Cashbook",
            ["Accounting/Expense Management"] = "ExpenseManagement",
            ["Services/CMS"] = "CMS",
            ["Reports/CMS Reports"] = "CMS",
            ["Templates/CMS Template"] = "CMS",
            ["Services/AMC"] = "AMC",
            ["Reports/Appointments/AMC"] = "AMC",
            ["Templates/AMC Template"] = "AMC",
            ["Services/Tele Calling"] = "TeleCalling",
            ["Reports/Followups/TeleCalling"] = "TeleCalling",
            ["Reports/Appointments/TeleCalling"] = "TeleCalling",
            ["Integrations/Telecall"] = "TeleCalling",
            ["Templates/TeleCalling Template"] = "TeleCalling",
            ["Services/Ticket"] = "Ticket",
            ["Integrations/TikcetWebGenerated"] = "Ticket",
            ["Settings/Ticket Web Configuration"] = "Ticket",
            ["DMS"] = "DMS",
            ["Services/MailChimp"] = "MailChimp",
            ["Services/MailChimp/Home"] = "MailChimp",
            ["Services/MailChimp/Campaign"] = "MailChimp",
            ["Services/MailChimp/Template"] = "MailChimp",
            ["Services/MailChimp/Add Subscriber"] = "MailChimp",
            ["Services/MailChimp/Campaign Reports"] = "MailChimp",
            ["Dashboard/IVR Dashboard"] = "IVR",
            ["Integrations/IVRDetails"] = "IVR",
            ["Reports/IVRDetails"] = "IVR",
            ["Reports/IVRDetails/IVRrep"] = "IVR",
            ["Settings/IVR Configuration"] = "IVR",
            ["Integrations/IndiaMART"] = "IndiaMART",
            ["Settings/IndiaMART Configuration"] = "IndiaMART",
            ["BizMail/Setting/IndiaMart"] = "IndiaMART",
            ["Integrations/TradeIndia"] = "TradeIndia",
            ["Settings/TradeIndia Configuration"] = "TradeIndia",
            ["BizMail/Setting/TradeIndia"] = "TradeIndia",
            ["Integrations/JustDial"] = "JustDial",
            ["Settings/JustDial Configuration"] = "JustDial",
            ["Integrations/Facebook"] = "Facebook",
            ["Settings/Facebook Configuration"] = "Facebook",
            ["BizMail/Setting/Facebook"] = "Facebook",
            ["Templates/Challan Template"] = "Challan",
            ["Templates/Invoice Template"] = "Sales"
        };

        private static string? ResolveModuleKey(NavigationItem item, string path)
        {
            if (!string.IsNullOrWhiteSpace(path) && NavigationModulePathMap.TryGetValue(path, out var module))
                return module;

            var candidate = path;
            if (string.IsNullOrWhiteSpace(candidate))
                candidate = item?.Title ?? string.Empty;

            if (string.IsNullOrWhiteSpace(candidate))
                return null;

            if (candidate.Contains("Proforma", StringComparison.OrdinalIgnoreCase))
                return "Proforma";
            if (candidate.Contains("Challan", StringComparison.OrdinalIgnoreCase))
                return "Challan";
            if (candidate.Contains("Purchase Order", StringComparison.OrdinalIgnoreCase))
                return "PurchaseOrder";
            if (candidate.Contains("Purchase", StringComparison.OrdinalIgnoreCase))
                return "Purchase";
            if (candidate.Contains("Cashbook", StringComparison.OrdinalIgnoreCase))
                return "Cashbook";
            if (candidate.Contains("Expense Management", StringComparison.OrdinalIgnoreCase))
                return "ExpenseManagement";
            if (candidate.Contains("Tele", StringComparison.OrdinalIgnoreCase))
                return "TeleCalling";
            if (candidate.Contains("Ticket", StringComparison.OrdinalIgnoreCase))
                return "Ticket";
            if (candidate.Contains("MailChimp", StringComparison.OrdinalIgnoreCase))
                return "MailChimp";
            if (candidate.Contains("Facebook", StringComparison.OrdinalIgnoreCase))
                return "Facebook";
            if (candidate.Contains("India", StringComparison.OrdinalIgnoreCase) && candidate.Contains("Mart", StringComparison.OrdinalIgnoreCase))
                return "IndiaMART";
            if (candidate.Contains("Trade", StringComparison.OrdinalIgnoreCase) && candidate.Contains("India", StringComparison.OrdinalIgnoreCase))
                return "TradeIndia";
            if (candidate.Contains("Just", StringComparison.OrdinalIgnoreCase) && candidate.Contains("Dial", StringComparison.OrdinalIgnoreCase))
                return "JustDial";
            if (candidate.Contains("IVR", StringComparison.OrdinalIgnoreCase))
                return "IVR";
            if (candidate.Contains("AMC", StringComparison.OrdinalIgnoreCase))
                return "AMC";
            if (candidate.Contains("CMS", StringComparison.OrdinalIgnoreCase))
                return "CMS";
            if (candidate.Contains("CEO", StringComparison.OrdinalIgnoreCase))
                return "CEO";
            if (candidate.Contains("Sales", StringComparison.OrdinalIgnoreCase))
                return "Sales";

            return null;
        }

        private string ToAbsolute(string path)
        {
            var basePath = _httpContextAccessor.HttpContext?.Request.PathBase.Value?.TrimEnd('/') ?? string.Empty;
            return basePath + "/" + path.TrimStart('~', '/');
        }

        private void SetActivePath()
        {
            string currentUrl = string.Empty;
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                var request = context.Request;
                var requestUrl = (request.PathBase + request.Path).ToString();
                currentUrl = requestUrl;
                if (!requestUrl.EndsWith("/") &&
                    string.Equals(request.Path, request.PathBase, StringComparison.OrdinalIgnoreCase))
                {
                    currentUrl += "/";
                }
            }

            int[] currentPath = new int[10];
            int[] bestMatch = null;
            int bestMatchLength = 0;

            foreach (var item in Items)
                SearchActivePath(item, currentUrl, currentPath, 0, ref bestMatch, ref bestMatchLength);

            ActivePath = bestMatch ?? new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        }

        private void SearchActivePath(NavigationItem link, string currentUrl, int[] currentPath, int depth,
            ref int[] bestMatch, ref int bestMatchLength)
        {
            currentPath[depth + 1] = 0;
            var url = link.Url ?? string.Empty;

            if (url.StartsWith("~/", StringComparison.Ordinal))
                url = ToAbsolute(url);

            if (currentUrl.IndexOf(url, StringComparison.OrdinalIgnoreCase) >= 0 &&
                (bestMatchLength == 0 || url.Length > bestMatchLength))
            {
                bestMatch = (int[])currentPath.Clone();
                bestMatchLength = url.Length;
            }

            if (depth <= 9)
            {
                foreach (var child in link.Children)
                    SearchActivePath(child, currentUrl, currentPath, depth + 1, ref bestMatch, ref bestMatchLength);
            }

            currentPath[depth]++;
        }
    }
}
