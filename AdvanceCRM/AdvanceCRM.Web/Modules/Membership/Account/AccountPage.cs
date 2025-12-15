
namespace AdvanceCRM.Membership.Pages
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Administration.Repositories;
    using AdvanceCRM.Common.Endpoints;
    using AdvanceCRM.Common.Pages;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity.Abstractions;
    using ILocalTextLocalizer = Serenity.ITextLocalizer;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.NetworkInformation;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Globalization;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.Extensions.Configuration;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using AdvanceCRM.Web.Helpers;
    using AdvanceCRM.MultiTenancy;

    [Route("Account")]
    public partial class AccountController : Controller
    {
        public static bool UseAdminLTELoginBox = false;
        private readonly ISqlConnections _connections;


        private readonly IUserAccessor userAccessor;
        private readonly IPermissionService permissionService;
        private readonly IRequestContext requestContext;
        private readonly IMemoryCache memoryCache;
        private readonly ITypeSource typeSource;
        protected ITwoLevelCache Cache { get; }
        private readonly IUserRetrieveService userRetriever;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRetrieveService _userRetriever;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ILocalTextLocalizer Localizer;
        private readonly SubdomainService _subdomainService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ITenantAccessor _tenantAccessor;
        public AccountController(
            ISqlConnections connections,
            IAuthenticationService authenticationService,
            IUserRetrieveService userRetriever,
            IWebHostEnvironment env,
            IConfiguration config,
            ILocalTextLocalizer localizer,
            IDataProtectionProvider dataProtectionProvider,
            ITwoLevelCache cache,
            SubdomainService subdomainService,
            IServiceScopeFactory scopeFactory,
            ITenantAccessor tenantAccessor)
        {
            _connections = connections;
            _authenticationService = authenticationService;
            _userRetriever = userRetriever;
            _env = env;
            _config = config;
            Localizer = localizer;
            _dataProtectionProvider = dataProtectionProvider;
            Cache = cache;
            _subdomainService = subdomainService;
            _scopeFactory = scopeFactory;
            _tenantAccessor = tenantAccessor;
        }

        [HttpGet, Route("Login")]
        public IActionResult Login(string activated, string returnUrl)
        {
            //Getting MAC addresses
            var systemMacAddress = new List<string>();

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                systemMacAddress.Add(adapter.GetPhysicalAddress().ToString());
            }


            //Permitted MAC addresses - Add address manually withour hypens(-)
            List<string> permittedMacAddress = new List<string>();

            permittedMacAddress.Add("6014B372CE82"); //1
            permittedMacAddress.Add("001C423E2403");
            permittedMacAddress.Add("FCAA14D30044");
            permittedMacAddress.Add("001C42CBF380");
            permittedMacAddress.Add("A8A795AA0F77");
            permittedMacAddress.Add("54E1AD6B397F");
            permittedMacAddress.Add("F82FA8C8C265");


            //Checking if permitted is avail in system MAC's
            bool elig_status = false;
            for (int i = 0; i < permittedMacAddress.Count; i++)
            {
                if (systemMacAddress.Contains(permittedMacAddress[i]))
                {
                    elig_status = true;
                    break;
                }
            }

            if (!elig_status)
            {
                elig_status = LicenseHelper.IsLicenseValid(_env, _config);
            }

            var rtnstr = MVC.Views.Membership.Account.AccountLogin;

            if (elig_status == false)
            {
                return Redirect("/Activation");
            }
            else
            {
                ViewData["Activated"] = activated;
                ViewData["HideLeftNavigation"] = true;
            }

            ViewData["WelcomeMessage"] = ResolveWelcomeMessage();

            return View(rtnstr);
        }

        [HttpPost, JsonFilter, Route("Login")]
        public Result<ServiceResponse> Login(LoginRequest request)
        {
            return this.ExecuteMethod(() =>
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                //if (string.IsNullOrEmpty(request.Username))
                //    throw new ArgumentNullException("username");


                var username = request.Username;

                var licenseStatus = GetTenantLicenseStatus();
                if (licenseStatus != null && licenseStatus.IsExpired)
                {
                    DeactivateTenantUsers();
                    throw new ValidationError("LicenseExpired", "License", "Your trial has expired. Please purchase a plan to continue using AdvanceCRM.");
                }
                else if (licenseStatus != null)
                {
                    // If the license is now valid (renewed / purchased) but all users were previously
                    // deactivated due to expiry, reactivate them so they can log in again.
                    ReactivateTenantUsersIfLicenseRestored(licenseStatus);
                }

                var Starttm = DateTime.Now.TimeOfDay;
                var Endtm = DateTime.Now.TimeOfDay;
                var StartTmStr = string.Empty;
                var EndTmStr = string.Empty;

                UserRow Config;

                using (var connection = _connections.NewFor<UserRow>())
                {
                    var s = UserRow.Fields;
                    Config = connection.TryFirst<UserRow>(q => q
                        .SelectTableFields()
                        .Select(s.StartTime)
                        .Select(s.EndTime)
                        .Select(s.NonOperational)
                        .Where(s.Username == username)
                        );

                    if (Config != null)
                    {
                        StartTmStr = Config.StartTime;
                        EndTmStr = Config.EndTime;
                    }
                }

                if (Config != null)
                {
                    if (Config.NonOperational.HasValue && Config.NonOperational == true && request.Type != "Y")
                        throw new Exception("This user is Non Operational hence cannot Log In");
                }

                var userDefValidate = _userRetriever.ByUsername(username) as UserDefinition;
                bool isValid = false;
                if (userDefValidate != null)
                {
                    var calcHash = UserRepository.CalculateHash(request.Password, userDefValidate.PasswordSalt);
                    isValid = string.Equals(calcHash, userDefValidate.PasswordHash, StringComparison.OrdinalIgnoreCase);
                }

                if (!StartTmStr.IsNullOrEmpty())
                {
                    Starttm = Convert.ToDateTime(StartTmStr).TimeOfDay;
                    Endtm = Convert.ToDateTime(EndTmStr).TimeOfDay;

                    if ((TimeSpan)DateTime.Now.TimeOfDay >= Starttm && (TimeSpan)DateTime.Now.TimeOfDay <= Endtm)
                    {
                        if (isValid)
                        {
                            var userDef = userDefValidate;
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, userDef.UserId.ToString()),
                                new Claim(ClaimTypes.Name, userDef.Username),
                                new Claim("DisplayName", userDef.DisplayName ?? string.Empty),
                                new Claim("UserId", userDef.UserId.ToString()),
                                new Claim("CompanyId", userDef.CompanyId.ToString()),
                                new Claim("BranchId", userDef.BranchId?.ToString() ?? string.Empty),
                                new Claim("UpperLevel", userDef.UpperLevel.ToString())
                            };
                            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                            _authenticationService.SignInAsync(
                                HttpContext,
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                principal,
                                new AuthenticationProperties { IsPersistent = false })
                                .GetAwaiter().GetResult();
                            using (var connection = _connections.NewFor<UserRow>())
                            {
                                var c = UserRow.Fields;

                                var ContactsId = connection.TryFirst<UserRow>(q => q
                                    .SelectTableFields()
                                    .Select(c.UserId)
                                    .Where(c.Username == username)
                                );

                                connection.Insert<LogInOutLogRow>(
                                    new LogInOutLogRow
                                    {
                                        Date = DateTime.Now,
                                        Type = (Masters.AttendanceTypeMaster)1,
                                        UserId = ContactsId.UserId
                                    }
                                );
                            }

                            return new ServiceResponse();
                        }

                        throw new ValidationError("AuthenticationError", Texts.Validation.AuthenticationError.ToString(Localizer));
                    }
                    else
                    {
                        throw new Exception("You are not authorized to log in at this time \n Your authorized timing is between " + Starttm + " to " + Endtm + "\n Current time on Server: " + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString());
                    }
                }
                else
                {
                    if (isValid)
                    {
                        var userDef = userDefValidate;
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, userDef.UserId.ToString()),
                            new Claim(ClaimTypes.Name, userDef.Username),
                            new Claim("DisplayName", userDef.DisplayName ?? string.Empty),
                            new Claim("UserId", userDef.UserId.ToString()),
                            new Claim("CompanyId", userDef.CompanyId.ToString()),
                            new Claim("BranchId", userDef.BranchId?.ToString() ?? string.Empty),
                            new Claim("UpperLevel", userDef.UpperLevel.ToString())
                        };
                        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                        _authenticationService.SignInAsync(
                            HttpContext,
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            principal,
                            new AuthenticationProperties { IsPersistent = false })
                            .GetAwaiter().GetResult();
                        using (var connection = _connections.NewFor<UserRow>())
                        {
                            var c = UserRow.Fields;

                            var ContactsId = connection.TryFirst<UserRow>(q => q
                                .SelectTableFields()
                                .Select(c.UserId)
                                .Where(c.Username == username)
                            );

                            connection.Insert<LogInOutLogRow>(
                                new LogInOutLogRow
                                {
                                    Date = DateTime.Now,
                                    Type = (Masters.AttendanceTypeMaster)1,
                                    UserId = ContactsId.UserId
                                }
                            );
                        }

                        return new ServiceResponse();
                    }

                    throw new ValidationError("AuthenticationError", Texts.Validation.AuthenticationError.ToString(Localizer));
                }

            });
        }

        [HttpGet, Route("LicenseStatus")]
        public IActionResult LicenseStatus()
        {
            var status = GetTenantLicenseStatus();
            if (status == null)
                return Json(new TenantLicenseStatusResponse { HasLicense = false });

            return Json(status);
        }

        private TenantLicenseStatusResponse? GetTenantLicenseStatus()
        {
            if (_tenantAccessor == null)
                return null;

            var tenant = _tenantAccessor.CurrentTenant;
            if (tenant == null)
                return null;

            TenantRow tenantRow = null;
            var originalTenant = _tenantAccessor.CurrentTenant;

            try
            {
                _tenantAccessor.CurrentTenant = null;

                using (var connection = _connections.NewByKey("Default"))
                {
                    if (tenant.TenantId > 0)
                    {
                        tenantRow = connection.TryById<TenantRow>(tenant.TenantId);
                    }

                    if (tenantRow == null)
                    {
                        var fields = TenantRow.Fields;
                        var subdomain = tenant.Subdomain?.Trim();
                        if (!string.IsNullOrEmpty(subdomain))
                        {
                            tenantRow = connection.TryFirst<TenantRow>(q => q
                                .Select(fields.TenantId)
                                .Select(fields.Plan)
                                .Select(fields.LicenseStartDate)
                                .Select(fields.LicenseEndDate)
                                .Select(fields.PurchaseAmount)
                                .Select(fields.PurchaseCurrency)
                                .Select(fields.PurchasedUsers)
                                .Select(fields.PaymentOrderId)
                                .Select(fields.PaymentId)
                                .Where(fields.Subdomain == subdomain));
                        }
                    }
                }
            }
            finally
            {
                _tenantAccessor.CurrentTenant = originalTenant;
            }

            if (tenantRow == null)
                return null;

            var start = tenantRow.LicenseStartDate;
            var end = tenantRow.LicenseEndDate;
            var plan = tenantRow.Plan?.Trim();
            var purchaseAmount = tenantRow.PurchaseAmount;
            var purchaseCurrency = string.IsNullOrWhiteSpace(tenantRow.PurchaseCurrency) ? null : tenantRow.PurchaseCurrency.Trim();
            var purchasedUsers = tenantRow.PurchasedUsers;
            var paymentOrderId = string.IsNullOrWhiteSpace(tenantRow.PaymentOrderId) ? null : tenantRow.PaymentOrderId.Trim();
            var paymentId = string.IsNullOrWhiteSpace(tenantRow.PaymentId) ? null : tenantRow.PaymentId.Trim();

            DateTime? normalizedStart = start.HasValue ? DateTime.SpecifyKind(start.Value, DateTimeKind.Utc) : (DateTime?)null;
            DateTime? normalizedEnd = end.HasValue ? DateTime.SpecifyKind(end.Value, DateTimeKind.Utc) : (DateTime?)null;

            var serverUtcNow = DateTime.UtcNow;
            var today = serverUtcNow.Date;
            int? totalDays = null;
            int? remainingDays = null;

            if (start.HasValue && end.HasValue)
            {
                totalDays = (int)(end.Value.Date - start.Value.Date).TotalDays + 1;
            }

            if (end.HasValue)
            {
                var diff = (end.Value.Date - today).TotalDays;
                if (diff >= 0)
                {
                    var calculatedRemaining = (int)diff + 1;

                    if (totalDays.HasValue && calculatedRemaining > totalDays.Value)
                        calculatedRemaining = totalDays.Value;

                    remainingDays = calculatedRemaining;
                }
                else
                {
                    remainingDays = 0;
                }
            }

            var isExpired = end.HasValue && end.Value.Date < today;
            var isTrial = !string.IsNullOrEmpty(plan) && plan.IndexOf("trial", StringComparison.OrdinalIgnoreCase) >= 0;

            return new TenantLicenseStatusResponse
            {
                HasLicense = start.HasValue || end.HasValue,
                Plan = plan,
                LicenseStartDate = normalizedStart,
                LicenseEndDate = normalizedEnd,
                TotalDays = totalDays,
                RemainingDays = remainingDays,
                IsExpired = isExpired,
                IsTrial = isTrial,
                PurchaseAmount = purchaseAmount,
                PurchaseCurrency = purchaseCurrency,
                PurchasedUsers = purchasedUsers,
                PaymentOrderId = paymentOrderId,
                PaymentId = paymentId,
                ServerDateUtc = serverUtcNow
            };
        }

        private string ResolveWelcomeMessage()
        {
            // Requirement: show the SAME welcome message for all subdomains / tenants.
            // We ignore per-tenant company name and always return the standardized branding.
            // Source of truth is the Login.FormTitle local text (already set to "Welcome to Bizpluserp System").
            return Texts.Forms.Membership.Login.FormTitle.ToString(Localizer);
        }

        private string? TryGetTenantCompanyName()
        {
            var tenant = _tenantAccessor?.CurrentTenant;
            if (tenant == null)
                return null;

            if (!string.IsNullOrWhiteSpace(tenant.CompanyName))
                return tenant.CompanyName.Trim();

            try
            {
                using var connection = _connections.NewByKey("Default");

                if (tenant.TenantId > 0)
                {
                    var byId = connection.TryById<TenantRow>(tenant.TenantId);
                    if (!string.IsNullOrWhiteSpace(byId?.Name))
                        return byId.Name.Trim();
                }

                if (!string.IsNullOrWhiteSpace(tenant.Subdomain))
                {
                    var fields = TenantRow.Fields;
                    var bySubdomain = connection.TryFirst<TenantRow>(q => q
                        .Select(fields.TenantId)
                        .Select(fields.Name)
                        .Where(fields.Subdomain == tenant.Subdomain));

                    if (!string.IsNullOrWhiteSpace(bySubdomain?.Name))
                        return bySubdomain.Name.Trim();
                }
            }
            catch
            {
                // Ignore lookup failures and fall back to default welcome message.
            }

            return null;
        }

        private void DeactivateTenantUsers()
        {
            try
            {
                using (var connection = _connections.NewFor<UserRow>())
                {
                    var fields = UserRow.Fields;
                    new SqlUpdate(fields.TableName)
                        .Set(fields.IsActive, false)
                        .Execute(connection, ExpectedRows.Ignore);
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("Failed to deactivate tenant users after license expiration.", ex).Log("DeactivateTenantUsers");
            }
        }

        private void ReactivateTenantUsersIfLicenseRestored(TenantLicenseStatusResponse status)
        {
            if (status == null || status.IsExpired)
                return;

            try
            {
                using (var connection = _connections.NewFor<UserRow>())
                {
                    var fields = UserRow.Fields;

                    // Check if there are currently ANY active users. If at least one is active we assume
                    // normal user management is in effect and skip bulk reactivation.
                    var activeCount = connection.Count<UserRow>(new Criteria(fields.IsActive) == 1);
                    if (activeCount == 0)
                    {
                        new SqlUpdate(fields.TableName)
                            .Set(fields.IsActive, true)
                            .Execute(connection, ExpectedRows.Ignore);
                    }
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("Failed to reactivate tenant users after license renewal.", ex).Log("ReactivateTenantUsers");
            }
        }

        private ActionResult Error(string message)
        {
            return View(MVC.Views.Errors.ValidationError,
                new ValidationError(Texts.Validation.InvalidResetToken.ToString()));
        }

        [HttpGet, Route("Signout")]
        public ActionResult Signout()
        {
            try
            {
                using (var connection = _connections.NewFor<UserRow>())
                {
                    connection.Insert<LogInOutLogRow>(
                        new LogInOutLogRow
                        {
                            Date = DateTime.Now,
                            Type = (Masters.AttendanceTypeMaster)2,
                            UserId = Convert.ToInt32(Context.User.GetIdentifier())
                        }
                    );
                }

                _authenticationService.SignOutAsync(
                    HttpContext,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new AuthenticationProperties())
                    .GetAwaiter().GetResult();
                return new RedirectResult("~/");
            }
            catch (Exception)
            {
                _authenticationService.SignOutAsync(
                    HttpContext,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new AuthenticationProperties())
                    .GetAwaiter().GetResult();
                return new RedirectResult("~/");
            }

        }
    }
}
