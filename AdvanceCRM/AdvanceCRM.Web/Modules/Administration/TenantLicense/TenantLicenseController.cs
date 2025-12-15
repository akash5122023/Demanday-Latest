using AdvanceCRM.Common;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Common; // For IEmailSender
using Dapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Serenity.Abstractions;
using Serenity.Data;
using Serenity.Reporting;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DapperSql = Dapper.SqlMapper; // Explicit alias to resolve Query<T> ambiguity

namespace AdvanceCRM.Administration.Pages
{
    [Route("Administration/TenantLicense")]
    [PageAuthorize(PermissionKeys.Security)]
    public class TenantLicenseController : Controller
    {
        private readonly ISqlConnections connections;
        private readonly IEmailSender emailSender;
        private readonly ILogger<TenantLicenseController> logger;
        private readonly ITenantAccessor tenantAccessor;
        private readonly MultiTenancyOptions multiTenancyOptions;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly EnvironmentSettings environmentSettings;

        private const string SuccessTempDataKey = "TenantLicenseSuccess";
        private const string ErrorTempDataKey = "TenantLicenseError";

        public TenantLicenseController(
            ISqlConnections connections,
            IEmailSender emailSender,
            ILogger<TenantLicenseController> logger,
            ITenantAccessor tenantAccessor,
            IOptions<MultiTenancyOptions> multiTenancyOptions,
            IWebHostEnvironment hostEnvironment,
            IOptions<EnvironmentSettings> environmentSettings)
        {
            this.connections = connections ?? throw new ArgumentNullException(nameof(connections));
            this.emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
            if (multiTenancyOptions == null)
                throw new ArgumentNullException(nameof(multiTenancyOptions));
            this.multiTenancyOptions = multiTenancyOptions.Value ?? new MultiTenancyOptions();
            this.hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
            if (environmentSettings == null)
                throw new ArgumentNullException(nameof(environmentSettings));
            this.environmentSettings = environmentSettings.Value ?? new EnvironmentSettings();
        }

        [HttpGet]
        public IActionResult Index(string? searchTerm = null, string? status = null)
        {
            var model = BuildTenantLicensePageModel(searchTerm, status);

            if (TempData.TryGetValue(SuccessTempDataKey, out var success))
                model.SuccessMessage = success as string ?? Convert.ToString(success, CultureInfo.InvariantCulture);

            if (TempData.TryGetValue(ErrorTempDataKey, out var error))
                model.ErrorMessage = error as string ?? Convert.ToString(error, CultureInfo.InvariantCulture);

            return View(MVC.Views.Administration.TenantLicense.TenantLicenseIndex, model);
        }

        [HttpGet("ExportPdf")]
        public IActionResult ExportPdf(string? searchTerm = null, string? status = null)
        {
            var model = BuildTenantLicensePageModel(searchTerm, status);

            var renderPath = Url.Action(
                nameof(ExportPdfView),
                "TenantLicense",
                new
                {
                    area = "Administration",
                    searchTerm = model.FilterTerm,
                    status = model.FilterStatus
                });

            if (string.IsNullOrEmpty(renderPath))
            {
                logger.LogError("Url.Action returned null while building TenantLicense PDF render path.");
                TempData[ErrorTempDataKey] = "Unable to determine the PDF export URL.";
                return RedirectToIndexWithFilters(model.FilterTerm, model.FilterStatus);
            }

            var externalBaseUrl = environmentSettings?.SiteExternalUrl;
            if (string.IsNullOrWhiteSpace(externalBaseUrl))
            {
                var request = HttpContext?.Request;
                if (request == null || !request.Host.HasValue)
                {
                    logger.LogError("Unable to determine request host while building TenantLicense PDF URL.");
                    TempData[ErrorTempDataKey] = "Unable to determine the PDF export URL.";
                    return RedirectToIndexWithFilters(model.FilterTerm, model.FilterStatus);
                }

                externalBaseUrl = $"{request.Scheme}://{request.Host}{Url.Content("~/")}";
            }

            externalBaseUrl = externalBaseUrl.Trim();
            if (!externalBaseUrl.EndsWith("/", StringComparison.Ordinal))
                externalBaseUrl += "/";

            if (!Uri.TryCreate(externalBaseUrl, UriKind.Absolute, out var baseUri))
            {
                logger.LogError("Invalid external base URL '{ExternalBaseUrl}' while building TenantLicense PDF URL.", externalBaseUrl);
                TempData[ErrorTempDataKey] = "Unable to determine the PDF export URL.";
                return RedirectToIndexWithFilters(model.FilterTerm, model.FilterStatus);
            }

            if (!Uri.TryCreate(baseUri, renderPath, out var renderUri))
            {
                logger.LogError("Invalid render path '{RenderPath}' while building TenantLicense PDF URL.", renderPath);
                TempData[ErrorTempDataKey] = "Unable to determine the PDF export URL.";
                return RedirectToIndexWithFilters(model.FilterTerm, model.FilterStatus);
            }

            var renderUrl = renderUri.ToString();

            var converter = new HtmlToPdfConverter();

            try
            {
                var wkhtmlPath = ResolveWkhtmltopdfPath();
                converter.UtilityExePath = wkhtmlPath;
            }
            catch (FileNotFoundException ex)
            {
                logger.LogError(ex, "wkhtmltopdf not found while generating tenant license PDF");
                TempData[ErrorTempDataKey] = "Unable to generate PDF because the wkhtmltopdf utility is missing.";
                return RedirectToIndexWithFilters(model.FilterTerm, model.FilterStatus);
            }

            converter.Url = renderUrl;

            var authCookie = HttpContext.Request.Cookies[CookieAuthenticationDefaults.CookiePrefix + "Cookies"];
            if (authCookie != null)
                converter.Cookies[CookieAuthenticationDefaults.CookiePrefix + "Cookies"] = authCookie;

            try
            {
                var pdfBytes = converter.Execute();

                if (pdfBytes == null || pdfBytes.Length == 0)
                {
                    logger.LogError("wkhtmltopdf returned an empty document for URL {RenderUrl}", renderUrl);
                    TempData[ErrorTempDataKey] = "Failed to generate the PDF file. Please try again later.";
                    return RedirectToIndexWithFilters(model.FilterTerm, model.FilterStatus);
                }

                var fileName = $"ClientLicenseMonitor-{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to generate tenant license PDF");
                TempData[ErrorTempDataKey] = "Failed to generate the PDF file. Please try again later.";
                return RedirectToIndexWithFilters(model.FilterTerm, model.FilterStatus);
            }
        }

        [HttpGet("ExportPdfView")]
        public IActionResult ExportPdfView(string? searchTerm = null, string? status = null)
        {
            var model = BuildTenantLicensePageModel(searchTerm, status);
            return View(MVC.Views.Administration.TenantLicense.TenantLicensePdf, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendReminder(int tenantId, string? filterTerm = null, string? filterStatus = null)
        {
            try
            {
                var tenantInfo = LoadTenantLicenses(tenantId).FirstOrDefault();
                if (tenantInfo == null)
                {
                    TempData[ErrorTempDataKey] = "The selected tenant could not be found.";
                    return RedirectToIndexWithFilters(filterTerm, filterStatus);
                }

                if (string.IsNullOrWhiteSpace(tenantInfo.PrimaryContactEmail))
                {
                    TempData[ErrorTempDataKey] = $"No contact email is configured for {tenantInfo.Name ?? "the tenant"}.";
                    return RedirectToIndexWithFilters(filterTerm, filterStatus);
                }

                var message = BuildReminderEmail(tenantInfo);
                emailSender.Send(message, skipQueue: false);

                TempData[SuccessTempDataKey] = $"Reminder email sent to {tenantInfo.PrimaryContactEmail}.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send tenant subscription reminder for TenantId={TenantId}", tenantId);
                TempData[ErrorTempDataKey] = "Failed to send reminder email. Please check the error logs for more details.";
            }

            return RedirectToIndexWithFilters(filterTerm, filterStatus);
        }

        private static string? NormalizeStatus(string? status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return null;

            return status.Trim().Equals("expired", StringComparison.OrdinalIgnoreCase)
                ? "expired"
                : status.Trim().Equals("active", StringComparison.OrdinalIgnoreCase)
                    ? "active"
                    : null;
        }

        private IActionResult RedirectToIndexWithFilters(string? filterTerm, string? filterStatus)
        {
            return RedirectToAction(nameof(Index), new
            {
                searchTerm = string.IsNullOrWhiteSpace(filterTerm) ? null : filterTerm.Trim(),
                status = NormalizeStatus(filterStatus)
            });
        }

        private TenantLicensePageModel BuildTenantLicensePageModel(string? searchTerm, string? status)
        {
            var tenants = LoadTenantLicenses();
            var trimmedSearch = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.Trim();
            var normalizedStatus = NormalizeStatus(status);

            if (!string.IsNullOrWhiteSpace(trimmedSearch))
            {
                tenants = tenants
                    .Where(x =>
                        (!string.IsNullOrWhiteSpace(x.Name) &&
                            x.Name.Contains(trimmedSearch, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrWhiteSpace(x.Subdomain) &&
                            x.Subdomain.Contains(trimmedSearch, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrWhiteSpace(x.Plan) &&
                            x.Plan.Contains(trimmedSearch, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(normalizedStatus))
            {
                tenants = normalizedStatus == "expired"
                    ? tenants.Where(x => x.IsExpired).ToList()
                    : tenants.Where(x => !x.IsExpired).ToList();
            }

            return new TenantLicensePageModel
            {
                Tenants = tenants,
                FilterTerm = trimmedSearch,
                FilterStatus = normalizedStatus
            };
        }

        private string ResolveWkhtmltopdfPath()
        {
            string wkhtmlPath;

            if (OperatingSystem.IsWindows())
            {
                var webRootPath = hostEnvironment.WebRootPath ?? hostEnvironment.ContentRootPath;

                wkhtmlPath = Path.Combine(hostEnvironment.ContentRootPath, "bin", "wkhtmltopdf.exe");
                if (!System.IO.File.Exists(wkhtmlPath))
                    wkhtmlPath = Path.Combine(webRootPath, "Reporting", "wkhtmltopdf.exe");
                if (!System.IO.File.Exists(wkhtmlPath))
                    wkhtmlPath = Path.Combine(hostEnvironment.ContentRootPath, "wkhtmltopdf.exe");
                if (!System.IO.File.Exists(wkhtmlPath))
                    wkhtmlPath = Path.Combine(hostEnvironment.ContentRootPath, "App_Data", "wkhtmltopdf.exe");
            }
            else
            {
                wkhtmlPath = "/usr/bin/wkhtmltopdf";
            }

            if (!System.IO.File.Exists(wkhtmlPath))
                throw new FileNotFoundException("wkhtmltopdf not found. Please install it.");

            return wkhtmlPath;
        }

        private MimeMessage BuildReminderEmail(TenantLicenseInfo tenant)
        {
            if (tenant == null)
                throw new ArgumentNullException(nameof(tenant));

            var message = new MimeMessage();

            if (!string.IsNullOrWhiteSpace(tenant.PrimaryContactName))
            {
                message.To.Add(new MailboxAddress(tenant.PrimaryContactName!, tenant.PrimaryContactEmail));
            }
            else
            {
                message.To.Add(MailboxAddress.Parse(tenant.PrimaryContactEmail!));
            }

            var planName = string.IsNullOrWhiteSpace(tenant.Plan) ? "your current" : $"your \"{tenant.Plan}\"";
            var companyName = string.IsNullOrWhiteSpace(tenant.Name) ? "your account" : tenant.Name.Trim();
            var expiryText = tenant.LicenseEndDate.HasValue
                ? tenant.LicenseEndDate.Value.ToString("dd MMM yyyy", CultureInfo.InvariantCulture)
                : "soon";

            message.Subject = $"Subscription reminder for {companyName}";

            var greetingName = tenant.PrimaryContactName ?? tenant.Name ?? "Customer";
            var statusLine = tenant.IsExpired
                ? $"expired on {expiryText}"
                : $"is scheduled to expire on {expiryText}";

            var body =
$"Dear {greetingName},\n\n" +
$"This is a friendly reminder that {planName} subscription for {companyName} {statusLine}.\n" +
"Please renew your package to keep your CRM data active. Otherwise we will need to deactivate and remove your profile from our system.\n\n" +
"You can renew by signing in to your portal and choosing the subscription plan that best fits your needs. If you need any assistance, simply reply to this email and our support team will be happy to help.\n\n" +
"Thank you for being with AdvanceCRM.\n" +
"--\nAdvanceCRM Support";

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            return message;
        }

        private IList<TenantLicenseInfo> LoadTenantLicenses(int? tenantId = null)
        {
            var tenants = new List<TenantLicenseInfo>();
            var originalTenant = tenantAccessor.CurrentTenant;

            try
            {
                tenantAccessor.CurrentTenant = null;

                using var connection = connections.NewFor<TenantRow>();
                var tenantFields = TenantRow.Fields;

                var tenantRows = connection.List<TenantRow>(query =>
                {
                    query.Select(tenantFields.TenantId)
                        .Select(tenantFields.Name)
                        .Select(tenantFields.Subdomain)
                        .Select(tenantFields.Plan)
                        .Select(tenantFields.LicenseStartDate)
                        .Select(tenantFields.LicenseEndDate)
                        .Select(tenantFields.PurchaseAmount)
                        .Select(tenantFields.PurchaseCurrency)
                        .Select(tenantFields.PurchasedUsers)
                        .Select(tenantFields.PaymentOrderId)
                        .Select(tenantFields.PaymentId);

                    if (tenantId.HasValue)
                        query.Where(tenantFields.TenantId == tenantId.Value);

                    query.OrderBy(tenantFields.Name);
                });

                if (tenantRows.Count == 0)
                    return tenants;

                var tenantIds = tenantRows
                    .Select(x => x.TenantId)
                    .Where(x => x.HasValue)
                    .Select(x => x.Value)
                    .Distinct()
                    .ToList();

                var contactLookupByTenantId = new Dictionary<int, TenantContactSelection>();
                var contactLookupByUrl = new Dictionary<string, TenantContactSelection>(StringComparer.OrdinalIgnoreCase);

                var portalUrlCandidates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var tenantRow in tenantRows)
                {
                    if (!string.IsNullOrWhiteSpace(tenantRow.Subdomain))
                        portalUrlCandidates.Add(tenantRow.Subdomain.Trim());

                    var portalUrl = BuildTenantPortalUrl(tenantRow.Subdomain);
                    if (!string.IsNullOrWhiteSpace(portalUrl))
                    {
                        var trimmedPortal = portalUrl.Trim();
                        portalUrlCandidates.Add(trimmedPortal);

                        var normalizedPortal = NormalizeUrlKey(trimmedPortal);
                        if (!string.IsNullOrEmpty(normalizedPortal))
                            portalUrlCandidates.Add(normalizedPortal);
                    }
                }

                var portalUrls = portalUrlCandidates.ToList();

                if (tenantIds.Count > 0 || portalUrls.Count > 0)
                {
                    var filterConditions = new List<string>();
                    if (tenantIds.Count > 0)
                        filterConditions.Add("TenantId IN @TenantIds");
                    if (portalUrls.Count > 0)
                        filterConditions.Add("Url IN @PortalUrls");

                    var contactSql =
                        @"SELECT UserId, TenantId, Url, Email, DisplayName, Username, Phone, UID, EmailId
                          FROM dbo.Users
                          WHERE ISNULL(IsActive, 0) = 1";

                    if (filterConditions.Count == 1)
                    {
                        contactSql += $" AND {filterConditions[0]}";
                    }
                    else if (filterConditions.Count > 1)
                    {
                        contactSql += $" AND ({string.Join(" OR ", filterConditions)})";
                    }

                    contactSql += " ORDER BY TenantId, UserId";

                    // Use Dapper's SqlMapper directly to avoid ambiguity with Serenity.Data.SqlMapper
                    foreach (var contact in DapperSql.Query<TenantUserContact>((System.Data.IDbConnection)connection, contactSql, new { TenantIds = tenantIds, PortalUrls = portalUrls }))
                    {
                        var normalized = NormalizeContact(contact);
                        if (normalized == null)
                            continue;

                        if (normalized.TenantId > 0)
                        {
                            if (!contactLookupByTenantId.TryGetValue(normalized.TenantId, out var existingByTenant) || ShouldReplaceContact(normalized, existingByTenant))
                                contactLookupByTenantId[normalized.TenantId] = normalized;
                        }

                        if (!string.IsNullOrEmpty(normalized.UrlKey))
                        {
                            if (!contactLookupByUrl.TryGetValue(normalized.UrlKey, out var existingByUrl) || ShouldReplaceContact(normalized, existingByUrl))
                                contactLookupByUrl[normalized.UrlKey] = normalized;
                        }
                    }
                }

                var today = DateTime.UtcNow.Date;

                foreach (var tenantRow in tenantRows)
                {
                    if (!(tenantRow.TenantId > 0))
                        continue;

                    var info = new TenantLicenseInfo
                    {
                        TenantId = tenantRow.TenantId.Value,
                        Name = tenantRow.Name?.Trim(),
                        Subdomain = tenantRow.Subdomain?.Trim(),
                        PortalUrl = BuildTenantPortalUrl(tenantRow.Subdomain),
                        Plan = tenantRow.Plan?.Trim(),
                        LicenseStartDate = tenantRow.LicenseStartDate,
                        LicenseEndDate = tenantRow.LicenseEndDate,
                        PurchaseAmount = tenantRow.PurchaseAmount,
                        PurchaseCurrency = tenantRow.PurchaseCurrency?.Trim(),
                        PurchasedUsers = tenantRow.PurchasedUsers,
                        PaymentOrderId = tenantRow.PaymentOrderId?.Trim(),
                        PaymentId = tenantRow.PaymentId?.Trim()
                    };

                    var endDate = tenantRow.LicenseEndDate?.Date;
                    if (endDate.HasValue)
                    {
                        var days = (endDate.Value - today).TotalDays;
                        if (days < 0)
                        {
                            info.IsExpired = true;
                            info.RemainingDays = 0;
                        }
                        else
                        {
                            info.RemainingDays = (int)Math.Floor(days) + 1;
                        }
                    }

                    var contact = contactLookupByTenantId.TryGetValue(info.TenantId, out var contactByTenant)
                        ? contactByTenant
                        : null;

                    if (contact == null)
                    {
                        var portalUrlKey = NormalizeUrlKey(info.PortalUrl);
                        if (portalUrlKey == null && !string.IsNullOrWhiteSpace(info.Subdomain))
                            portalUrlKey = NormalizeUrlKey(info.Subdomain);

                        if (portalUrlKey != null && contactLookupByUrl.TryGetValue(portalUrlKey, out var contactByUrl))
                            contact = contactByUrl;
                    }

                    if (contact != null)
                    {
                        info.PrimaryContactEmail = contact.Email;
                        info.PrimaryContactName = contact.DisplayName;
                        info.PrimaryContactPhone = contact.Phone;
                        info.PrimaryContactUserId = contact.UserId;
                    }

                    tenants.Add(info);
                }
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }

            return tenants;
        }

        private static TenantContactSelection? NormalizeContact(TenantUserContact contact)
        {
            if (contact == null)
                return null;

            if (contact.TenantId <= 0 && string.IsNullOrWhiteSpace(contact.Url))
                return null;

            var email = SelectEmail(contact);
            var phone = string.IsNullOrWhiteSpace(contact.Phone) ? null : contact.Phone.Trim();
            var displayName = string.IsNullOrWhiteSpace(contact.DisplayName)
                ? (!string.IsNullOrWhiteSpace(contact.Username) ? contact.Username.Trim() : null)
                : contact.DisplayName.Trim();

            var userId = contact.UserId > 0 ? contact.UserId : (int?)null;

            var score = 0;

            if (!string.IsNullOrWhiteSpace(email))
                score += 2;

            if (!string.IsNullOrWhiteSpace(phone))
                score += 1;

            if (score == 0)
                return null;

            return new TenantContactSelection
            {
                UserId = userId,
                TenantId = contact.TenantId,
                Email = email,
                DisplayName = displayName,
                Phone = phone,
                Score = score,
                UrlKey = NormalizeUrlKey(contact.Url)
            };
        }

        private static string? SelectEmail(TenantUserContact contact)
        {
            if (contact == null)
                return null;

            var candidates = new[]
            {
                contact.Email,
                contact.EmailId,
                contact.Username,
                contact.Uid
            };

            string? firstNonEmpty = null;

            foreach (var candidate in candidates)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                    continue;

                var trimmed = candidate.Trim();

                if (firstNonEmpty == null)
                    firstNonEmpty = trimmed;

                if (IsLikelyEmail(trimmed))
                    return trimmed;
            }

            return firstNonEmpty;
        }

        private static bool ShouldReplaceContact(TenantContactSelection candidate, TenantContactSelection existing)
        {
            if (existing == null)
                return true;

            if (candidate.Score > existing.Score)
                return true;

            if (candidate.Score < existing.Score)
                return false;

            if (!string.IsNullOrWhiteSpace(candidate.Email) && string.IsNullOrWhiteSpace(existing.Email))
                return true;

            if (!string.IsNullOrWhiteSpace(candidate.Phone) && string.IsNullOrWhiteSpace(existing.Phone))
                return true;

            return false;
        }

        private static bool IsLikelyEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            var atIndex = value.IndexOf('@');
            if (atIndex <= 0 || atIndex >= value.Length - 1)
                return false;

            return value.IndexOf('.', atIndex + 1) > atIndex + 1;
        }

        private sealed class TenantUserContact
        {
            public int UserId { get; set; }
            public int TenantId { get; set; }
            public string? Url { get; set; }
            public string? Email { get; set; }
            public string? DisplayName { get; set; }
            public string? Username { get; set; }
            public string? Phone { get; set; }
            public string? Uid { get; set; }
            public string? EmailId { get; set; }
        }

        private sealed class TenantContactSelection
        {
            public int? UserId { get; set; }
            public int TenantId { get; set; }
            public string? Email { get; set; }
            public string? DisplayName { get; set; }
            public string? Phone { get; set; }
            public int Score { get; set; }
            public string? UrlKey { get; set; }
        }

        private static string? NormalizeUrlKey(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            var trimmed = url.Trim();

            if (!trimmed.Contains(' '))
            {
                if (!Uri.TryCreate(trimmed, UriKind.Absolute, out var absolute))
                {
                    if (!Uri.TryCreate($"https://{trimmed}", UriKind.Absolute, out absolute))
                        return trimmed.Trim('/').Trim('.').ToLowerInvariant();
                }

                trimmed = absolute.Host;
            }

            if (string.IsNullOrWhiteSpace(trimmed))
                return null;

            return trimmed.Trim().Trim('/').Trim('.').ToLowerInvariant();
        }

        private string? BuildTenantPortalUrl(string? subdomain)
        {
            if (string.IsNullOrWhiteSpace(subdomain))
                return null;

            var trimmedSubdomain = subdomain.Trim().Trim('.');
            if (string.IsNullOrEmpty(trimmedSubdomain))
                return null;

            var scheme = HttpContext?.Request?.Scheme;
            if (string.IsNullOrWhiteSpace(scheme))
                scheme = Uri.UriSchemeHttps;

            var rootDomain = multiTenancyOptions?.RootDomain;
            if (!string.IsNullOrWhiteSpace(rootDomain))
            {
                var normalizedRoot = rootDomain.Trim().Trim('.');
                if (!string.IsNullOrEmpty(normalizedRoot))
                    return $"{scheme}://{trimmedSubdomain}.{normalizedRoot}";
            }

            return $"{scheme}://{trimmedSubdomain}";
        }
    }
}
