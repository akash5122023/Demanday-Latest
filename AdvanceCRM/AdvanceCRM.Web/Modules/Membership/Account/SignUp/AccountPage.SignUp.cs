using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using Serenity.Web;
using AdvanceCRM.Administration;
using AdvanceCRM.Administration.Repositories;
using AdvanceCRM.Common;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Settings;
using AdvanceCRM.MultiTenancy;
using System;
using System.IO;
using System.Diagnostics;

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using MicrosoftSqlException = Microsoft.Data.SqlClient.SqlException;
using MicrosoftSqlError = Microsoft.Data.SqlClient.SqlError;
using LegacySqlException = System.Data.SqlClient.SqlException;
using LegacySqlError = System.Data.SqlClient.SqlError;



namespace AdvanceCRM.Membership.Pages
{
    public partial class AccountController : Controller
    {
        [HttpGet, Route("SignUp")]
        public ActionResult SignUp()
        {
            // Public signup is disabled for single-tenant deployments.
            return RedirectToAction("Login");
        }

        [HttpPost, Route("SignUp"), JsonRequest]
        public Result<ServiceResponse> SignUp(SignUpRequest request,
            [FromServices] IEmailSender emailSender,
            [FromServices] IOptions<EnvironmentSettings> options = null,
            [FromServices] ITenantAccessor tenantAccessor = null,
            [FromServices] IRazorpayOrderService razorpayOrderService = null)
        {
            // Public signup is disabled; reject any direct API calls.
            throw new ValidationError("SignUpDisabled", "SignUp",
                "Online signup is disabled for this deployment. Please contact the system administrator.");

            TenantInfo? originalTenant = null;
            if (tenantAccessor != null)
            {
                originalTenant = tenantAccessor.CurrentTenant;
                tenantAccessor.CurrentTenant = null;
            }

            try
            {
            return this.UseConnection("Default", connection =>
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new ArgumentNullException(nameof(request.Email));

                var normalizedEmail = request.Email.Trim();
                if (string.IsNullOrEmpty(normalizedEmail))
                    throw new ArgumentNullException(nameof(request.Email));

                request.Email = normalizedEmail;

                request.Plan = string.IsNullOrWhiteSpace(request.Plan) ? null : request.Plan.Trim();

                var requestedUsers = request.Users.GetValueOrDefault(1);
                if (requestedUsers < 1)
                    requestedUsers = 1;
                request.Users = requestedUsers;

                // Plan is now optional for signup. If provided, validate it; otherwise proceed without a plan.
                ProductPlanRow planRow = null;
                if (!string.IsNullOrWhiteSpace(request.Plan))
                {
                    var planFields = ProductPlanRow.Fields;
                    var activePlans = connection.List<ProductPlanRow>(q => q
                        .SelectTableFields()
                        .Where(planFields.IsActive == 1));

                    planRow = activePlans.FirstOrDefault(x =>
                        string.Equals(x.Name?.Trim(), request.Plan, StringComparison.OrdinalIgnoreCase));

                    if (planRow == null)
                        throw new ValidationError("PlanUnavailable", "Plan", "Selected plan is currently unavailable.");

                    var planUserLimit = planRow.UserLimit ?? 0;
                    if (planUserLimit > 0 && requestedUsers > planUserLimit)
                    {
                        throw new ValidationError("UserLimitExceeded", "Users",
                            $"Selected plan allows creating up to {planUserLimit} active users.");
                    }

                    request.Plan = planRow.Name?.Trim();
                }
                else
                {
                    request.Plan = null; // ensure empty string does not propagate
                }

                var normalizedCouponFromRequest = string.IsNullOrWhiteSpace(request.CouponCode)
                    ? null
                    : request.CouponCode.Trim();
                request.CouponCode = normalizedCouponFromRequest;

                decimal? purchaseAmount = null;
                string purchaseCurrency = null;
                var paymentOrderId = string.IsNullOrWhiteSpace(request.PaymentOrderId) ? null : request.PaymentOrderId.Trim();
                var paymentId = string.IsNullOrWhiteSpace(request.PaymentId) ? null : request.PaymentId.Trim();
                var paymentSignature = string.IsNullOrWhiteSpace(request.PaymentSignature) ? null : request.PaymentSignature.Trim();
                var paymentCurrency = string.IsNullOrWhiteSpace(request.PaymentCurrency) ? null : request.PaymentCurrency.Trim().ToUpperInvariant();

                int? paymentAmountMinorUnits = null;
                if (!string.IsNullOrWhiteSpace(request.PaymentAmount) &&
                    int.TryParse(request.PaymentAmount.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedPaymentAmount))
                {
                    paymentAmountMinorUnits = parsedPaymentAmount;
                }

                var hasPaymentDetails = paymentOrderId != null || paymentId != null || paymentSignature != null || paymentAmountMinorUnits.HasValue;

                CouponCodeRow appliedCouponRow = null;

                if (hasPaymentDetails)
                {
                    if (razorpayOrderService == null || !razorpayOrderService.IsEnabled)
                        throw new ValidationError("PaymentUnavailable", "Plan", "Online payments are currently unavailable. Please contact support.");

                    if (string.IsNullOrWhiteSpace(request.Plan))
                        throw new ValidationError("PlanRequired", "Plan", "A plan must be selected to complete the purchase.");

                    if (paymentOrderId == null || paymentId == null || paymentSignature == null)
                        throw new ValidationError("PaymentIncomplete", "Plan", "Payment confirmation is incomplete. Please retry the payment.");

                    if (!razorpayOrderService.VerifySignature(paymentOrderId, paymentId, paymentSignature))
                        throw new ValidationError("PaymentVerificationFailed", "Plan", "We couldn't verify your payment. Please contact support.");

                    if (!paymentAmountMinorUnits.HasValue || paymentAmountMinorUnits.Value <= 0)
                        throw new ValidationError("PaymentAmountInvalid", "Plan", "Invalid payment amount received from the payment gateway.");

                    var planPricePerUser = planRow.PricePerUser ?? 0m;
                    if (planPricePerUser <= 0)
                        throw new ValidationError("PlanPriceMissing", "Plan", "Selected plan is not configured with a valid price.");

                    var baseTotal = planPricePerUser * requestedUsers;
                    var baseAmountMinorUnits = Convert.ToInt32(Math.Round(baseTotal * 100m, MidpointRounding.AwayFromZero));

                    RazorpayOrderDetails orderDetails;
                    try
                    {
                        orderDetails = razorpayOrderService
                            .GetOrderAsync(paymentOrderId, CancellationToken.None)
                            .GetAwaiter().GetResult();
                    }
                    catch (Exception)
                    {
                        throw new ValidationError("PaymentVerificationFailed", "Plan", "We couldn't verify your payment details with the payment gateway.");
                    }

                    if (orderDetails == null)
                        throw new ValidationError("PaymentVerificationFailed", "Plan", "We couldn't verify your payment details with the payment gateway.");

                    var orderAmountMinorUnits = orderDetails.Amount;
                    if (orderAmountMinorUnits <= 0)
                        throw new ValidationError("PaymentAmountInvalid", "Plan", "Invalid payment amount received from the payment gateway.");

                    if (paymentAmountMinorUnits.Value != orderAmountMinorUnits)
                        throw new ValidationError("PaymentAmountMismatch", "Plan", "Payment amount does not match the selected plan and user count.");

                    var orderNotes = orderDetails.Notes ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                    if (orderNotes.TryGetValue(RazorpayOrderNotes.Users, out var notedUsers) &&
                        int.TryParse(notedUsers, NumberStyles.Integer, CultureInfo.InvariantCulture, out var notedUsersValue) &&
                        notedUsersValue != requestedUsers)
                    {
                        throw new ValidationError("PaymentAmountMismatch", "Plan", "Payment amount does not match the selected plan and user count.");
                    }

                    if (orderNotes.TryGetValue(RazorpayOrderNotes.BaseAmountMinor, out var notedBaseAmount) &&
                        int.TryParse(notedBaseAmount, NumberStyles.Integer, CultureInfo.InvariantCulture, out var notedBaseMinorUnits) &&
                        notedBaseMinorUnits != baseAmountMinorUnits)
                    {
                        throw new ValidationError("PaymentAmountMismatch", "Plan", "Payment amount does not match the selected plan and user count.");
                    }

                    var finalAmountMinorUnits = orderAmountMinorUnits;

                    if (orderNotes.TryGetValue(RazorpayOrderNotes.FinalAmountMinor, out var notedFinalAmount) &&
                        int.TryParse(notedFinalAmount, NumberStyles.Integer, CultureInfo.InvariantCulture, out var notedFinalMinorUnits))
                    {
                        if (notedFinalMinorUnits != orderAmountMinorUnits)
                            throw new ValidationError("PaymentAmountMismatch", "Plan", "Payment amount does not match the selected plan and user count.");

                        finalAmountMinorUnits = notedFinalMinorUnits;
                    }

                    string couponFromOrder = null;
                    if (orderNotes.TryGetValue(RazorpayOrderNotes.CouponCode, out var couponNote) && !string.IsNullOrWhiteSpace(couponNote))
                        couponFromOrder = couponNote.Trim();

                    if (!string.IsNullOrEmpty(couponFromOrder))
                    {
                        var couponFields = CouponCodeRow.Fields;
                        appliedCouponRow = connection.TryFirst<CouponCodeRow>(q => q
                            .SelectTableFields()
                            .Where(couponFields.Code == couponFromOrder));

                        var couponValidation = CouponHelper.ValidateCouponForCheckout(appliedCouponRow, DateTime.UtcNow);
                        if (couponValidation != null)
                            throw new ValidationError(couponValidation, "Coupon", "Coupon is no longer valid for this purchase.");

                        var expectedDiscountMinorUnits = CouponHelper.CalculateDiscountMinorUnits(appliedCouponRow, baseAmountMinorUnits);
                        if (expectedDiscountMinorUnits >= baseAmountMinorUnits)
                            throw new ValidationError(CouponHelper.CouponAmountInvalid, "Coupon", "Coupon discount is invalid for this order.");

                        if (orderNotes.TryGetValue(RazorpayOrderNotes.CouponDiscountMinor, out var notedDiscount) &&
                            int.TryParse(notedDiscount, NumberStyles.Integer, CultureInfo.InvariantCulture, out var notedDiscountMinorUnits) &&
                            notedDiscountMinorUnits != expectedDiscountMinorUnits)
                        {
                            throw new ValidationError("PaymentAmountMismatch", "Plan", "Payment amount does not match the selected plan and discount.");
                        }

                        var expectedFinalMinorUnits = Math.Max(0, baseAmountMinorUnits - expectedDiscountMinorUnits);
                        if (expectedFinalMinorUnits != finalAmountMinorUnits)
                            throw new ValidationError("PaymentAmountMismatch", "Plan", "Payment amount does not match the selected plan and discount.");

                        if (string.IsNullOrEmpty(normalizedCouponFromRequest) ||
                            !string.Equals(normalizedCouponFromRequest, couponFromOrder, StringComparison.OrdinalIgnoreCase))
                        {
                            request.CouponCode = couponFromOrder;
                        }
                    }
                    else if (baseAmountMinorUnits != finalAmountMinorUnits)
                    {
                        throw new ValidationError("PaymentAmountMismatch", "Plan", "Payment amount does not match the selected plan and user count.");
                    }

                    var resolvedCurrency = string.IsNullOrWhiteSpace(planRow.Currency)
                        ? razorpayOrderService.Currency ?? "INR"
                        : planRow.Currency.Trim().ToUpperInvariant();

                    var orderCurrency = string.IsNullOrWhiteSpace(orderDetails.Currency)
                        ? resolvedCurrency
                        : orderDetails.Currency.Trim().ToUpperInvariant();

                    if (!string.IsNullOrWhiteSpace(paymentCurrency) &&
                        !string.Equals(paymentCurrency.Trim(), orderCurrency, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new ValidationError("PaymentCurrencyMismatch", "Plan", "Payment currency does not match the selected plan.");
                    }

                    purchaseAmount = finalAmountMinorUnits / 100m;
                    purchaseCurrency = orderCurrency;
                    paymentCurrency = orderCurrency;
                }
                else
                {
                    paymentOrderId = null;
                    paymentId = null;
                    paymentSignature = null;
                    paymentCurrency = null;
                    paymentAmountMinorUnits = null;
                }

                // Determine the admin password that will be provisioned for the new tenant.
                // Prefer the configured default so that onboarding emails can communicate
                // the expected credentials, but fall back to the historical admin@123 value
                // if no configuration entry is found.
                var configuredAdminPassword = _config["TrialSettings:DefaultAdminPassword"];
                var adminPassword = string.IsNullOrWhiteSpace(configuredAdminPassword)
                    ? "admin@123"
                    : configuredAdminPassword.Trim();
                // We still validate pattern if user tried to send one, but it won't be used
                if (!string.IsNullOrWhiteSpace(request.Password))
                {
                    try { UserRepository.ValidatePassword(request.Password, Localizer); } catch { /* ignore */ }
                }

                if (string.IsNullOrWhiteSpace(request.DisplayName))
                    throw new ArgumentNullException(nameof(request.DisplayName));

                var displayName = request.DisplayName.TrimToEmpty();
                request.DisplayName = displayName;

                if (string.IsNullOrWhiteSpace(request.Company))
                    throw new ArgumentNullException(nameof(request.Company));

                var company = request.Company.TrimToEmpty();
                request.Company = company;

                var subdomainInput = (request.Subdomain ?? string.Empty).Trim();

                if (string.IsNullOrWhiteSpace(subdomainInput))
                {
                    throw new ValidationError("SubdomainRequired", "Subdomain", "Subdomain is required.");
                }

                var normalizedSubdomain = NormalizeSubdomain(subdomainInput);

                if (normalizedSubdomain == null ||
                    !string.Equals(subdomainInput, normalizedSubdomain, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ValidationError("SubdomainInvalid", "Subdomain", "Subdomain can only include letters, numbers and hyphens, and must start and end with a letter or number.");
                }

                EnsureUniqueSignUpIdentifiers(connection, company, normalizedSubdomain, normalizedEmail);

                if (string.IsNullOrWhiteSpace(request.MobileNumber))
                    throw new ArgumentNullException(nameof(request.MobileNumber));

                request.Subdomain = subdomainInput;

                var (licenseStart, licenseEnd) = CalculateLicensePeriod(planRow);

                using (var uow = new UnitOfWork(connection))
                {
                    string salt = null;
                    var hash = UserRepository.GenerateHash(adminPassword, ref salt);
                    var email = request.Email;
                    var username = request.Email;

                    var fld = UserRow.Fields;
                    var userId = (int)connection.InsertAndGetID(new UserRow
                    {
                        Username = username,
                        Source = "sign",
                        DisplayName = displayName,
                        Email = email,
                        Plan = request.Plan,
                        Phone = request.MobileNumber.TrimToEmpty(),
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        IsActive = false,
                        InsertDate = DateTime.Now,
                        InsertUserId = 1,
                        LastDirectoryUpdate = DateTime.Now
                    });

                    // Synchronously provision tenant and get url
                    var result = ProvisionTenantAndGetUrl(userId, company, request.Plan, displayName, email, hash, salt, adminPassword, licenseStart, licenseEnd, normalizedSubdomain, purchaseAmount, purchaseCurrency, requestedUsers, paymentOrderId, paymentId);
                    var tenantId = result.tenantId;
                    var url = result.url;

                    // Update user with tenantId and url
                    connection.UpdateById(new UserRow
                    {
                        UserId = userId,
                        TenantId = tenantId,
                        Url = url
                    });

                    var externalUrl = options?.Value.SiteExternalUrl ??
                        Request.GetBaseUri().ToString();

                    var tenantBaseUrl = string.IsNullOrWhiteSpace(url) ? externalUrl : url;
                    var loginUrl = UriHelper.Combine(tenantBaseUrl, "Account/Login");

                    var emailModel = new ActivateEmailModel
                    {
                        Username = username,
                        DisplayName = displayName,
                        TenantUrl = tenantBaseUrl,
                        LoginUrl = loginUrl,
                        // Keep admin username as 'admin' for tenant root login
                        AdminUsername = "admin",
                        AdminPassword = adminPassword
                    };

                    var emailSubject = Texts.Forms.Membership.SignUp.ActivateEmailSubject.ToString(Localizer);
                    var emailBody = TemplateHelper.RenderViewToString(HttpContext.RequestServices,
                        MVC.Views.Membership.Account.SignUp.AccountActivateEmail, emailModel);

                    if (emailSender is null)
                        throw new ArgumentNullException(nameof(emailSender));

                    var activationSubject = emailSubject;
                    var activationBody = emailBody;
                    var activationRecipient = email;

                    if (appliedCouponRow?.Id != null)
                    {
                        var couponFields = CouponCodeRow.Fields;
                        var update = new SqlUpdate(couponFields.TableName)
                            .Set(couponFields.UsedCount, (appliedCouponRow.UsedCount ?? 0) + 1)
                            .Where(couponFields.Id == appliedCouponRow.Id.Value);

                        if (appliedCouponRow.MaxUsageCount.HasValue)
                            update.Where(couponFields.UsedCount < appliedCouponRow.MaxUsageCount.Value);

                        var affected = update.Execute(uow.Connection);

                        if (affected == 0)
                            throw new ValidationError(CouponHelper.CouponUsageLimitReached, "Coupon", "Coupon usage limit has been reached.");
                    }

                    uow.Commit();
                    UserRetrieveService.RemoveCachedUser(Cache, userId, username);

                    QueueActivationEmail(emailSender, activationSubject, activationBody, activationRecipient);

                    return new ServiceResponse();
                }
                });
            }
            finally
            {
                if (tenantAccessor != null)
                    tenantAccessor.CurrentTenant = originalTenant;
            }
        }

        // Helper method to provision tenant and return (tenantId, url)
        private (int tenantId, string url) ProvisionTenantAndGetUrl(int userId, string companyName, string plan, string displayName, string email, string passwordHash, string passwordSalt, string plainPassword, DateTime licenseStartDate, DateTime licenseEndDate, string requestedSubdomain, decimal? purchaseAmount, string purchaseCurrency, int? purchasedUsers, string paymentOrderId, string paymentId)
        {
            var sub = !string.IsNullOrEmpty(requestedSubdomain)
                ? requestedSubdomain
                : NormalizeSubdomain(companyName);

            const int dnsStatusMaxLength = 2000;
            string dnsStatus = ResolveDnsStatus(companyName, sub);
            if (dnsStatus?.Length > dnsStatusMaxLength)
                dnsStatus = dnsStatus.Substring(0, dnsStatusMaxLength);

            var port = GenerateFreePort();
            var tenantDb = $"Tenant_{userId}";
            int tenantId;
            string url = null;
            using (var scope = _scopeFactory.CreateScope())
            {
                var connections = scope.ServiceProvider.GetRequiredService<ISqlConnections>();
                using (var connection = connections.NewByKey("Default"))
                {
                    var tenantRow = new TenantRow
                    {
                        Name = companyName,
                        Subdomain = sub,
                        DbName = tenantDb,
                        Port = port,
                        Plan = plan,
                        Modules = null,
                        LicenseStartDate = licenseStartDate,
                        LicenseEndDate = licenseEndDate,
                        DnsStatus = dnsStatus,
                        PurchaseAmount = purchaseAmount,
                        PurchaseCurrency = purchaseCurrency,
                        PurchasedUsers = purchasedUsers,
                        PaymentOrderId = paymentOrderId,
                        PaymentId = paymentId
                    };
                    try
                    {
                        tenantId = Convert.ToInt32(connection.InsertAndGetID(tenantRow));
                    }
                    catch (Exception ex)
                    {
                        if (!IsMissingDnsStatusColumn(ex))
                            throw;
                        var warning = new StringBuilder("Tenants table is missing the DnsStatus column. Apply the latest migrations to persist DNS provisioning status.");
                        if (!string.IsNullOrWhiteSpace(dnsStatus))
                        {
                            warning.Append(" Latest status: ").Append(dnsStatus);
                        }
                        new InvalidOperationException(warning.ToString(), ex).Log("ProvisionTenant");
                        tenantId = InsertTenantWithoutDnsStatus(connection, companyName, sub, tenantDb, port, plan, licenseStartDate, licenseEndDate, purchaseAmount, purchaseCurrency, purchasedUsers, paymentOrderId, paymentId);
                    }
                    var domain = _config.GetSection("Cloudflare")["RootDomain"];
                    if (!string.IsNullOrEmpty(sub) && !string.IsNullOrEmpty(domain))
                        url = $"https://{sub}.{domain}/";
                }
                try
                {
                    SeedTenantDefaults(connections, tenantDb, companyName, displayName, email, plan);
                }
                catch (Exception)
                {
                    // SeedTenantDefaults already logs and wraps the original exception,
                    // so just rethrow to keep the existing behavior for callers.
                    throw;
                }

                SeedTenantAdminUser(connections, tenantDb, displayName, email, passwordHash, passwordSalt, plan, plainPassword);
            }

            // Linux script (unchanged)
            var scriptPath = "/opt/advancecrm/provision.sh";
            if (OperatingSystem.IsLinux())
            {
                if (System.IO.File.Exists(scriptPath))
                {
                    Process.Start(scriptPath, $"{port} {tenantDb}");
                }
                else
                {
                    new FileNotFoundException($"Provisioning script not found at {scriptPath}").Log("ProvisionTenant");
                }
            }
            return (tenantId, url);
        }

        private void EnsureUniqueSignUpIdentifiers(IDbConnection connection, string company, string normalizedSubdomain, string email)
        {
            if (connection.Exists<TenantRow>(TenantRow.Fields.Name == company))
                throw new ValidationError("CompanyNameInUse", "Company", "Company name already exists.");

            if (connection.Exists<TenantRow>(TenantRow.Fields.Subdomain == normalizedSubdomain))
                throw new ValidationError("SubdomainInUse", "Subdomain", "Subdomain already exists.");

            if (connection.Exists<UserRow>(
                    UserRow.Fields.Username == email |
                    UserRow.Fields.Email == email))
            {
                throw new ValidationError("EmailInUse", Texts.Validation.EmailInUse.ToString(Localizer));
            }
        }

        private string NormalizeSubdomain(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            var sub = value.Trim().ToLowerInvariant();
            sub = Regex.Replace(sub, @"[^a-z0-9-]", string.Empty);
            sub = sub.Trim('-');

            if (sub.Length > 63)
                sub = sub.Substring(0, 63);

            return string.IsNullOrEmpty(sub) ? null : sub;
        }

        private string ResolveDnsStatus(string companyName, string subdomain)
        {
            const string defaultPendingMessage = "Pending: Subdomain provisioning scheduled.";

            if (string.IsNullOrEmpty(subdomain))
                return "Skipped: No valid subdomain generated.";

            if (_subdomainService == null)
                return "Skipped: DNS provisioning service unavailable.";

            try
            {
                var provisioningTask = _subdomainService.CreateSubdomainAsync(subdomain);
                var completedTask = Task.WhenAny(provisioningTask, Task.Delay(TimeSpan.FromSeconds(8))).GetAwaiter().GetResult();

                if (completedTask == provisioningTask)
                {
                    var status = provisioningTask.GetAwaiter().GetResult();
                    return string.IsNullOrWhiteSpace(status) ? "Created" : status.Trim();
                }

                _ = provisioningTask.ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        var exception = task.Exception?.Flatten().InnerExceptions.FirstOrDefault() ?? task.Exception;
                        if (exception != null)
                        {
                            var failure = BuildDnsFailureStatus(companyName, subdomain, exception);
                            new InvalidOperationException(failure, exception).Log("ProvisionTenant");
                        }
                    }
                    else if (!task.IsCanceled)
                    {
                        var backgroundStatus = task.Result;
                        if (!string.IsNullOrWhiteSpace(backgroundStatus))
                            new InvalidOperationException($"DNS provisioning completed asynchronously with status: {backgroundStatus.Trim()}.").Log("ProvisionTenant");
                    }
                }, TaskScheduler.Default);

                return defaultPendingMessage;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException aggregate && aggregate.InnerExceptions.Count == 1)
                    ex = aggregate.InnerExceptions[0];

                var failureStatus = BuildDnsFailureStatus(companyName, subdomain, ex);
                new InvalidOperationException(failureStatus, ex).Log("ProvisionTenant");
                return failureStatus;
            }
        }

        private void QueueActivationEmail(IEmailSender emailSender, string subject, string body, string recipient)
        {
            if (emailSender == null || string.IsNullOrWhiteSpace(recipient))
                return;

            Task.Run(() =>
            {
                try
                {
                    emailSender.Send(subject: subject, body: body, mailTo: recipient);
                }
                catch (Exception ex)
                {
                    new InvalidOperationException("Failed to send activation email.", ex).Log("SignUp");
                }
            });
        }

        private (DateTime Start, DateTime End) CalculateLicensePeriod(ProductPlanRow plan)
        {
            // Always calculate an inclusive trial period where both start and end days count.
            // Priority:
            //   1. Plan specific TrialDays if > 0
            //   2. appsettings.json TrialSettings:DefaultDays if > 0
            //   3. Hard fallback = 7 days (so we never end up with 1 day unless explicitly configured)
            var start = DateTime.Today;

            int planTrialDays = (plan?.TrialDays).GetValueOrDefault(0);
            int? configuredDefault = null;
            try
            {
                configuredDefault = _config?.GetValue<int?>("TrialSettings:DefaultDays");
            }
            catch { /* ignore config access issues */ }

            int effectiveDays;
            if (planTrialDays > 0)
                effectiveDays = planTrialDays;
            else if (configuredDefault.HasValue && configuredDefault.Value > 0)
                effectiveDays = configuredDefault.Value;
            else
                effectiveDays = 7; // safe fallback so trial is never 1 day unintentionally

            // Guard against nonsense values
            if (effectiveDays < 1)
                effectiveDays = 1;
            if (effectiveDays > 365)
                effectiveDays = 365; // arbitrary cap to prevent accidental huge trials

            var end = effectiveDays == 1 ? start : start.AddDays(effectiveDays - 1);
            return (start, end);
        }

        private void SeedTenantAdminUser(ISqlConnections connections, string tenantDb, string displayName, string email, string passwordHash, string passwordSalt, string plan, string plainPassword = null)
        {
            if (string.IsNullOrWhiteSpace(tenantDb) || connections == null)
                return;

            try
            {
                string adminSalt;
                string adminHash;

                if (!string.IsNullOrEmpty(plainPassword))
                {
                    adminSalt = null;
                    adminHash = UserRepository.GenerateHash(plainPassword, ref adminSalt);
                }
                else
                {
                    adminHash = passwordHash ?? string.Empty;
                    adminSalt = passwordSalt ?? string.Empty;
                }

                var normalizedDisplayName = string.IsNullOrWhiteSpace(displayName)
                    ? string.Empty
                    : displayName.Trim();

                var normalizedEmail = string.IsNullOrWhiteSpace(email)
                    ? null
                    : email.Trim();

                object EmailParameter(object value) => value ?? DBNull.Value;

                const string defaultSeedAdminEmail = "admin@dummy.com";

                var connectionInfo = connections.TryGetConnectionString("Default");
                var baseConnectionString = connectionInfo?.ConnectionString;
                if (string.IsNullOrWhiteSpace(baseConnectionString))
                    return;

                var tenantConnectionString = TenantAwareSqlConnections.BuildTenantConnectionString(baseConnectionString, tenantDb);
                using var sqlConnection = new SqlConnection(tenantConnectionString);
                sqlConnection.Open();

                var now = DateTime.Now;
                var planParameterValue = string.IsNullOrWhiteSpace(plan) ? (object)DBNull.Value : plan.Trim();

                int? adminUserId = null;

                // Prefer record explicitly using the admin username
                using (var findByUsername = sqlConnection.CreateCommand())
                {
                    findByUsername.CommandText = "SELECT TOP (1) UserId FROM [dbo].[Users] WHERE Username = @Username ORDER BY UserId;";
                    findByUsername.Parameters.AddWithValue("@Username", "admin");
                    var result = findByUsername.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        adminUserId = Convert.ToInt32(result);
                }

                // Fall back to the historically seeded dummy email if username didn't match
                if (!adminUserId.HasValue)
                {
                    using var findByEmail = sqlConnection.CreateCommand();
                    findByEmail.CommandText = @"SELECT TOP (1) UserId FROM [dbo].[Users]
WHERE Email = @DefaultEmail OR EmailId = @DefaultEmail
ORDER BY UserId;";
                    findByEmail.Parameters.AddWithValue("@DefaultEmail", defaultSeedAdminEmail);
                    var result = findByEmail.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        adminUserId = Convert.ToInt32(result);
                }

                // As a final fallback use the first user row if any
                if (!adminUserId.HasValue)
                {
                    using var findFirstUser = sqlConnection.CreateCommand();
                    findFirstUser.CommandText = "SELECT TOP (1) UserId FROM [dbo].[Users] ORDER BY UserId;";
                    var result = findFirstUser.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        adminUserId = Convert.ToInt32(result);
                }

                if (adminUserId.HasValue)
                {
                    using var updateCommand = sqlConnection.CreateCommand();
                    updateCommand.CommandText = @"UPDATE [dbo].[Users]
SET Username = 'admin',
    DisplayName = @DisplayName,
    Email = @Email,
    EmailId = @EmailId,
    PasswordHash = @PasswordHash,
    PasswordSalt = @PasswordSalt,
    Plan = @Plan,
    Source = @Source,
    IsActive = 1,
    LastDirectoryUpdate = @Now
WHERE UserId = @UserId;";
                    updateCommand.Parameters.AddWithValue("@DisplayName", normalizedDisplayName);
                    updateCommand.Parameters.AddWithValue("@Email", EmailParameter(normalizedEmail));
                    updateCommand.Parameters.AddWithValue("@EmailId", EmailParameter(normalizedEmail));
                    updateCommand.Parameters.AddWithValue("@PasswordHash", adminHash ?? string.Empty);
                    updateCommand.Parameters.AddWithValue("@PasswordSalt", adminSalt ?? string.Empty);
                    updateCommand.Parameters.AddWithValue("@Plan", planParameterValue);
                    updateCommand.Parameters.AddWithValue("@Source", "sign");
                    updateCommand.Parameters.AddWithValue("@Now", now);
                    updateCommand.Parameters.AddWithValue("@UserId", adminUserId.Value);
                    updateCommand.ExecuteNonQuery();
                }
                else
                {
                    using var insertCommand = sqlConnection.CreateCommand();
                    insertCommand.CommandText = @"INSERT INTO [dbo].[Users]
(Username, DisplayName, Email, EmailId, PasswordHash, PasswordSalt, Plan, Source, IsActive, InsertDate, LastDirectoryUpdate)
VALUES ('admin', @DisplayName, @Email, @EmailId, @PasswordHash, @PasswordSalt, @Plan, @Source, 1, @Now, @Now);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    insertCommand.Parameters.AddWithValue("@DisplayName", normalizedDisplayName);
                    insertCommand.Parameters.AddWithValue("@Email", EmailParameter(normalizedEmail));
                    insertCommand.Parameters.AddWithValue("@EmailId", EmailParameter(normalizedEmail));
                    insertCommand.Parameters.AddWithValue("@PasswordHash", adminHash ?? string.Empty);
                    insertCommand.Parameters.AddWithValue("@PasswordSalt", adminSalt ?? string.Empty);
                    insertCommand.Parameters.AddWithValue("@Plan", planParameterValue);
                    insertCommand.Parameters.AddWithValue("@Source", "sign");
                    insertCommand.Parameters.AddWithValue("@Now", now);

                    var newUserId = Convert.ToInt32(insertCommand.ExecuteScalar());

                    using (var auditCommand = sqlConnection.CreateCommand())
                    {
                        auditCommand.CommandText = @"UPDATE [dbo].[Users]
SET InsertUserId = @UserId,
    UpdateUserId = @UserId,
    LastDirectoryUpdate = @Now
WHERE UserId = @UserId;";
                        auditCommand.Parameters.AddWithValue("@UserId", newUserId);
                        auditCommand.Parameters.AddWithValue("@Now", now);
                        auditCommand.ExecuteNonQuery();
                    }

                    using var roleCommand = sqlConnection.CreateCommand();
                    roleCommand.CommandText = @"DECLARE @RoleId INT = (SELECT TOP 1 RoleId FROM [dbo].[Roles] ORDER BY RoleId);
IF @RoleId IS NOT NULL
BEGIN
    INSERT INTO [dbo].[UserRoles] (UserId, RoleId) VALUES (@UserId, @RoleId);
END";
                    roleCommand.Parameters.AddWithValue("@UserId", newUserId);
                    roleCommand.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(normalizedEmail))
                {
                    using var ensureAdminEmailCommand = sqlConnection.CreateCommand();
                    ensureAdminEmailCommand.CommandText = @"UPDATE [dbo].[Users]
SET Email = @Email,
    EmailId = @Email
WHERE (Username = 'admin' OR Email = @DefaultEmail OR EmailId = @DefaultEmail)
  AND (Email <> @Email OR Email IS NULL OR EmailId <> @Email OR EmailId IS NULL);";
                    ensureAdminEmailCommand.Parameters.AddWithValue("@Email", normalizedEmail);
                    ensureAdminEmailCommand.Parameters.AddWithValue("@DefaultEmail", defaultSeedAdminEmail);
                    ensureAdminEmailCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException($"Failed to seed admin user for tenant database '{tenantDb}'.", ex).Log("ProvisionTenant");
            }
        }
        private void SeedTenantDefaults(ISqlConnections connections, string tenantDb, string companyName, string adminDisplayName, string adminEmail, string plan)
        {
            if (string.IsNullOrWhiteSpace(tenantDb) || connections == null)
                return;

            try
            {
                var connectionInfo = connections.TryGetConnectionString("Default");
                var baseConnectionString = connectionInfo?.ConnectionString;
                if (string.IsNullOrWhiteSpace(baseConnectionString))
                    throw new InvalidOperationException("The default connection string is not configured. Unable to provision the tenant database.");

                EnsureTenantDatabaseCreated(baseConnectionString, tenantDb);
                RunTenantMigrations(baseConnectionString, tenantDb);

                var tenantConnectionString = TenantAwareSqlConnections.BuildTenantConnectionString(baseConnectionString, tenantDb);

                EnsureTenantSchemaReady(tenantConnectionString);

                ResetTenantIdentitySeeds(tenantConnectionString);

                using var templateConnection = new SqlConnection(baseConnectionString);
                templateConnection.Open();

                using var tenantConnection = new SqlConnection(tenantConnectionString);
                tenantConnection.Open();

                using var transaction = tenantConnection.BeginTransaction();

                var tablesToCopy = new StaticTableDefinition[]
                {
                    new("dbo", "State", keepIdentity: true),
                    new("dbo", "City", keepIdentity: true),
                    new("dbo", "CompanyDetails", keepIdentity: true)
                };

                foreach (var table in tablesToCopy)
                    CopyStaticTable(templateConnection, tenantConnection, transaction, table);

                EnsureTenantCompanyRecord(tenantConnection, transaction, companyName, adminDisplayName, adminEmail, plan);

                ClearTenantBranchPhoneNumbers(tenantConnection, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                var wrapped = new InvalidOperationException($"Failed to seed default tenant data for database '{tenantDb}'.", ex);
                wrapped.Log("ProvisionTenant");
                throw wrapped;
            }
        }

        private static void EnsureTenantDatabaseCreated(string baseConnectionString, string tenantDb)
        {
            if (string.IsNullOrWhiteSpace(baseConnectionString) || string.IsNullOrWhiteSpace(tenantDb))
                return;

            var masterBuilder = new SqlConnectionStringBuilder(baseConnectionString);
            if (masterBuilder.ContainsKey("AttachDBFilename"))
                masterBuilder.Remove("AttachDBFilename");
            masterBuilder.InitialCatalog = "master";
            masterBuilder["Database"] = "master";

            using var masterConnection = new SqlConnection(masterBuilder.ConnectionString);
            masterConnection.Open();

            using var command = masterConnection.CreateCommand();
            command.CommandText = @"
DECLARE @dbName sysname = @TenantDatabase;
IF DB_ID(@dbName) IS NULL
BEGIN
    DECLARE @sql NVARCHAR(MAX) = N'CREATE DATABASE [' + REPLACE(@dbName, N']', N']]') + N']';
    EXEC (@sql);
END;

DECLARE @alterSql NVARCHAR(MAX) = N'ALTER DATABASE [' + REPLACE(@dbName, N']', N']]') + N'] SET READ_WRITE WITH ROLLBACK IMMEDIATE;';
EXEC (@alterSql);

SET @alterSql = N'ALTER DATABASE [' + REPLACE(@dbName, N']', N']]') + N'] SET MULTI_USER WITH ROLLBACK IMMEDIATE;';
EXEC (@alterSql);";
            var parameter = command.Parameters.Add("@TenantDatabase", SqlDbType.NVarChar, 128);
            parameter.Value = tenantDb;
            command.ExecuteNonQuery();
        }

        private static void RunTenantMigrations(string baseConnectionString, string tenantDb)
        {
            if (string.IsNullOrWhiteSpace(baseConnectionString) || string.IsNullOrWhiteSpace(tenantDb))
                return;

            var tenantConnectionString = TenantAwareSqlConnections.BuildTenantConnectionString(baseConnectionString, tenantDb);
            var migrationConnectionString = SanitizeConnectionStringForMigrations(tenantConnectionString);
            var assembly = typeof(DataMigrations).Assembly;
            var assemblyLocation = assembly.Location;
            var migrationsPath = string.IsNullOrEmpty(assemblyLocation)
                ? AppContext.BaseDirectory
                : Path.GetDirectoryName(assemblyLocation);

            var conventionSet = new DefaultConventionSet(defaultSchemaName: null, migrationsPath);

            using var serviceProvider = new ServiceCollection()
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .AddSingleton<IConventionSet>(conventionSet)
                .Configure<TypeFilterOptions>(options =>
                {
                    options.Namespace = "AdvanceCRM.Migrations.DefaultDB";
                })
                .Configure<ProcessorOptions>(options =>
                {
                    options.Timeout = TimeSpan.FromMinutes(5);
                })
                .ConfigureRunner(builder =>
                {
                    builder.AddSqlServer();
                    builder.WithGlobalConnectionString(migrationConnectionString);
                    builder.WithMigrationsIn(assembly);
                })
                .BuildServiceProvider(false);

            using var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            try
            {
                runner.MigrateUp();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to run migrations for tenant database '{tenantDb}'.", ex);
            }
        }

        private static string SanitizeConnectionStringForMigrations(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return connectionString;

            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                RemoveTrustServerCertificate(builder);
                return builder.ConnectionString;
            }
            catch (ArgumentException)
            {
                var genericBuilder = new DbConnectionStringBuilder
                {
                    ConnectionString = connectionString
                };

                RemoveTrustServerCertificate(genericBuilder);
                return genericBuilder.ConnectionString;
            }
        }

        private static void RemoveTrustServerCertificate(IDictionary builder)
        {
            if (builder == null)
                return;

            var keysToRemove = new List<string>();
            foreach (DictionaryEntry entry in builder)
            {
                var key = entry.Key?.ToString();
                if (string.Equals(key, "TrustServerCertificate", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "Trust Server Certificate", StringComparison.OrdinalIgnoreCase))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                builder.Remove(key);
            }
        }

        private static void EnsureTenantSchemaReady(string tenantConnectionString)
        {
            if (string.IsNullOrWhiteSpace(tenantConnectionString))
                throw new InvalidOperationException("Tenant connection string is not available.");

            using var connection = new SqlConnection(tenantConnectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM sys.tables WHERE name = @TableName AND schema_id = SCHEMA_ID('dbo');";
            command.Parameters.AddWithValue("@TableName", "Users");

            var result = Convert.ToInt32(command.ExecuteScalar());
            if (result <= 0)
                throw new InvalidOperationException("Tenant database provisioning failed. Expected table 'dbo.Users' was not created by the migration process.");
        }

        private static void ResetTenantIdentitySeeds(string tenantConnectionString)
        {
            if (string.IsNullOrWhiteSpace(tenantConnectionString))
                return;

            using var connection = new SqlConnection(tenantConnectionString);
            connection.Open();

            var tables = new List<string>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT '[' + s.name + '].[' + t.name + ']' AS TableName FROM sys.identity_columns ic JOIN sys.tables t ON ic.object_id = t.object_id JOIN sys.schemas s ON t.schema_id = s.schema_id";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                    tables.Add(reader.GetString(0));
            }

            foreach (var table in tables)
            {
                using (var checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = $"SELECT TOP 1 1 FROM {table}";
                    var hasRows = checkCommand.ExecuteScalar();
                    if (hasRows != null)
                        continue;
                }

                using var reseed = connection.CreateCommand();
                var quotedTable = table.Replace("'", "''");
                reseed.CommandText = $"DBCC CHECKIDENT ('{quotedTable}', RESEED, 0)";
                reseed.ExecuteNonQuery();
            }
        }

        private static void CopyStaticTable(SqlConnection sourceConnection, SqlConnection tenantConnection, SqlTransaction transaction, StaticTableDefinition table)
        {
            if (sourceConnection == null || tenantConnection == null || table == null)
                return;

            var fullName = $"[{table.Schema}].[{table.Table}]";

            using (var checkCommand = tenantConnection.CreateCommand())
            {
                checkCommand.Transaction = transaction;
                checkCommand.CommandText = $"SELECT TOP 1 1 FROM {fullName}";
                var exists = checkCommand.ExecuteScalar();
                if (exists != null)
                    return;
            }

            var columns = GetColumnNames(sourceConnection, transaction, table.Schema, table.Table);
            if (columns.Count == 0)
                return;

            using var selectCommand = sourceConnection.CreateCommand();
            selectCommand.CommandText = $"SELECT {string.Join(",", columns.Select(c => "[" + c + "]"))} FROM {fullName}";
            using var reader = selectCommand.ExecuteReader();
            if (!reader.HasRows)
                return;

            var options = SqlBulkCopyOptions.Default;
            if (table.KeepIdentity)
                options |= SqlBulkCopyOptions.KeepIdentity;

            using var bulkCopy = new SqlBulkCopy(tenantConnection, options, transaction);
            bulkCopy.DestinationTableName = fullName;
            foreach (var column in columns)
                bulkCopy.ColumnMappings.Add(column, column);
            bulkCopy.WriteToServer(reader);
        }

        private static List<string> GetColumnNames(SqlConnection connection, SqlTransaction transaction, string schema, string table)
        {
            var result = new List<string>();
            using var command = connection.CreateCommand();
            if (transaction != null && transaction.Connection == connection)
                command.Transaction = transaction;
            command.CommandText = @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @Schema AND TABLE_NAME = @Table ORDER BY ORDINAL_POSITION";
            command.Parameters.AddWithValue("@Schema", schema);
            command.Parameters.AddWithValue("@Table", table);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                result.Add(reader.GetString(0));
            return result;
        }

        private static void EnsureTenantCompanyRecord(SqlConnection tenantConnection, SqlTransaction transaction, string companyName, string adminDisplayName, string adminEmail, string plan)
        {
            if (tenantConnection == null || transaction == null)
                return;

            if (string.IsNullOrWhiteSpace(companyName))
                return;

            var trimmedName = companyName.Trim();
            if (trimmedName.Length == 0)
                return;

            var normalizedDisplayName = string.IsNullOrWhiteSpace(adminDisplayName) ? trimmedName : adminDisplayName.Trim();
            if (string.IsNullOrWhiteSpace(normalizedDisplayName))
                normalizedDisplayName = trimmedName;

            var normalizedEmail = string.IsNullOrWhiteSpace(adminEmail) ? null : adminEmail.Trim();
            var normalizedPlan = string.IsNullOrWhiteSpace(plan) ? null : plan.Trim();
            var planValue = normalizedPlan != null && normalizedPlan.Length > 250 ? normalizedPlan.Substring(0, 250) : normalizedPlan;
            var emailValue = normalizedEmail != null && normalizedEmail.Length > 200 ? normalizedEmail.Substring(0, 200) : normalizedEmail;

            var companyNameValue = trimmedName.Length > 250 ? trimmedName.Substring(0, 250) : trimmedName;
            var companyAddressValue = normalizedDisplayName.Length > 500 ? normalizedDisplayName.Substring(0, 500) : normalizedDisplayName;
            const string defaultPhone = "N/A";

            var companyColumns = GetColumnNames(tenantConnection, transaction, "dbo", "CompanyDetails");
            bool HasCompanyColumn(string columnName) =>
                companyColumns.Any(x => string.Equals(x, columnName, StringComparison.OrdinalIgnoreCase));
            using (var selectCompanyId = tenantConnection.CreateCommand())
            {
                selectCompanyId.Transaction = transaction;
                selectCompanyId.CommandText = "SELECT TOP (1) Id FROM [dbo].[CompanyDetails] ORDER BY Id;";
                var existingId = selectCompanyId.ExecuteScalar();

                if (existingId == null || existingId == DBNull.Value)
                {
                    var columns = new List<string> { "[Name]" };
                    var values = new List<string> { "@CompanyName" };

                    if (HasCompanyColumn("Slogan"))
                    {
                        columns.Add("[Slogan]");
                        values.Add("@CompanySlogan");
                    }

                    if (HasCompanyColumn("Address"))
                    {
                        columns.Add("[Address]");
                        values.Add("@CompanyAddress");
                    }

                    if (HasCompanyColumn("Phone"))
                    {
                        columns.Add("[Phone]");
                        values.Add("@CompanyPhone");
                    }

                    if (HasCompanyColumn("EmailId"))
                    {
                        columns.Add("[EmailId]");
                        values.Add("@CompanyEmail");
                    }

                    using var insertCompany = tenantConnection.CreateCommand();
                    insertCompany.Transaction = transaction;
                    insertCompany.CommandText = $"INSERT INTO [dbo].[CompanyDetails] ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)}); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    AddParameter(insertCompany, "@CompanyName", companyNameValue);
                    if (HasCompanyColumn("Slogan"))
                        AddParameter(insertCompany, "@CompanySlogan", planValue);
                    if (HasCompanyColumn("Address"))
                        AddParameter(insertCompany, "@CompanyAddress", companyAddressValue);
                    if (HasCompanyColumn("Phone"))
                        AddParameter(insertCompany, "@CompanyPhone", defaultPhone);
                    if (HasCompanyColumn("EmailId"))
                        AddParameter(insertCompany, "@CompanyEmail", emailValue);
                    insertCompany.ExecuteScalar();
                }
                else
                {
                    var companyId = Convert.ToInt32(existingId);
                    var assignments = new List<string> { "[Name] = @CompanyName" };

                    if (HasCompanyColumn("Slogan"))
                        assignments.Add("[Slogan] = @CompanySlogan");

                    if (HasCompanyColumn("Address"))
                        assignments.Add("[Address] = CASE WHEN LTRIM(RTRIM(ISNULL([Address], ''))) = '' THEN @CompanyAddress ELSE [Address] END");

                    if (HasCompanyColumn("Phone"))
                        assignments.Add("[Phone] = CASE WHEN LTRIM(RTRIM(ISNULL([Phone], ''))) = '' THEN @CompanyPhone ELSE [Phone] END");

                    if (HasCompanyColumn("EmailId"))
                        assignments.Add("[EmailId] = @CompanyEmail");

                    using var updateCompany = tenantConnection.CreateCommand();
                    updateCompany.Transaction = transaction;
                    updateCompany.CommandText = $"UPDATE [dbo].[CompanyDetails] SET {string.Join(", ", assignments)} WHERE Id = @CompanyId;";
                    AddParameter(updateCompany, "@CompanyName", companyNameValue);
                    if (HasCompanyColumn("Slogan"))
                        AddParameter(updateCompany, "@CompanySlogan", planValue);
                    if (HasCompanyColumn("Address"))
                        AddParameter(updateCompany, "@CompanyAddress", companyAddressValue);
                    if (HasCompanyColumn("Phone"))
                        AddParameter(updateCompany, "@CompanyPhone", defaultPhone);
                    if (HasCompanyColumn("EmailId"))
                        AddParameter(updateCompany, "@CompanyEmail", emailValue);
                    AddParameter(updateCompany, "@CompanyId", companyId);
                    updateCompany.ExecuteNonQuery();
                }
            }
        }

        private static void ClearTenantBranchPhoneNumbers(SqlConnection tenantConnection, SqlTransaction transaction)
        {
            if (tenantConnection == null || transaction == null)
                return;

            var branchColumns = GetColumnNames(tenantConnection, transaction, "dbo", "Branch");
            if (branchColumns.Count == 0)
                return;

            if (!branchColumns.Any(x => string.Equals(x, "Phone", StringComparison.OrdinalIgnoreCase)))
                return;

            using var clearPhones = tenantConnection.CreateCommand();
            clearPhones.Transaction = transaction;
            clearPhones.CommandText = "UPDATE [dbo].[Branch] SET [Phone] = NULL;";
            clearPhones.ExecuteNonQuery();
        }

        private sealed class StaticTableDefinition
        {
            public StaticTableDefinition(string schema, string table, bool keepIdentity)
            {
                Schema = schema;
                Table = table;
                KeepIdentity = keepIdentity;
            }

            public string Schema { get; }
            public string Table { get; }
            public bool KeepIdentity { get; }
        }
        private async Task ProvisionTenantAsync(int userId, string companyName, string plan, DateTime licenseStartDate, DateTime licenseEndDate,
            decimal? purchaseAmount = null, string purchaseCurrency = null, int? purchasedUsers = null, string paymentOrderId = null, string paymentId = null)
        {
            try
            {
                var sub = companyName?.Trim().ToLowerInvariant();
                sub = Regex.Replace(sub ?? string.Empty, @"[^a-z0-9-]", string.Empty);
                sub = sub.Trim('-');
                if (sub.Length > 63)
                    sub = sub.Substring(0, 63);
                sub = string.IsNullOrEmpty(sub) ? null : sub;

                const int dnsStatusMaxLength = 2000;

                string dnsStatus;

                if (!string.IsNullOrEmpty(sub))
                {
                    try
                    {

                        var status = await _subdomainService.CreateSubdomainAsync(sub);
                        dnsStatus = string.IsNullOrWhiteSpace(status) ? "Created" : status.Trim();
                    }
                    catch (Exception ex)
                    {
                        dnsStatus = BuildDnsFailureStatus(companyName, sub, ex);
                        new InvalidOperationException(dnsStatus, ex).Log("ProvisionTenant");

                    }
                }
                else
                {
                    dnsStatus = "Skipped: No valid subdomain generated.";
                }


                if (dnsStatus?.Length > dnsStatusMaxLength)
                    dnsStatus = dnsStatus.Substring(0, dnsStatusMaxLength);


                var port = GenerateFreePort();
                var tenantDb = $"Tenant_{userId}";

                using (var scope = _scopeFactory.CreateScope())
                {
                    var connections = scope.ServiceProvider.GetRequiredService<ISqlConnections>();
                    UserRow userRow = null;
                    using (var connection = connections.NewByKey("Default"))
                    {
                        // insert tenant record and get identity as int
                        var tenantRow = new TenantRow
                        {
                            Name = companyName,
                            Subdomain = sub,
                            DbName = tenantDb,
                            Port = port,
                            Plan = plan,
                            Modules = null,
                            LicenseStartDate = licenseStartDate,
                            LicenseEndDate = licenseEndDate,
                            DnsStatus = dnsStatus,
                            PurchaseAmount = purchaseAmount,
                            PurchaseCurrency = purchaseCurrency,
                            PurchasedUsers = purchasedUsers,
                            PaymentOrderId = paymentOrderId,
                            PaymentId = paymentId
                        };

                        int tenantId;
                        try
                        {
                            tenantId = Convert.ToInt32(connection.InsertAndGetID(tenantRow));
                        }
                        catch (Exception ex)
                        {
                            if (!IsMissingDnsStatusColumn(ex))
                                throw;

                            var warning = new StringBuilder("Tenants table is missing the DnsStatus column. Apply the latest migrations to persist DNS provisioning status.");
                            if (!string.IsNullOrWhiteSpace(dnsStatus))
                            {
                                warning.Append(" Latest status: ").Append(dnsStatus);
                            }

                            new InvalidOperationException(warning.ToString(), ex).Log("ProvisionTenant");


                            tenantId = InsertTenantWithoutDnsStatus(connection, companyName, sub, tenantDb, port, plan, licenseStartDate, licenseEndDate, purchaseAmount, purchaseCurrency, purchasedUsers, paymentOrderId, paymentId);
                        }


                        // assign user to tenant and store generated url
                        var domain = _config.GetSection("Cloudflare")["RootDomain"];
                        string url = null;
                        if (!string.IsNullOrEmpty(sub) && !string.IsNullOrEmpty(domain))
                            url = $"https://{sub}.{domain}/";
                        connection.UpdateById(new UserRow
                        {
                            UserId = userId,
                            TenantId = tenantId,
                            Url = url
                        });
                        userRow = connection.TryById<UserRow>(userId);
                        var tenantDisplayName = userRow?.DisplayName;
                        SeedTenantDefaults(connections, tenantDb, companyName, tenantDisplayName ?? string.Empty, userRow?.Email, plan);
                    }
                    if (userRow != null)
                    {
                        var seedPlan = string.IsNullOrWhiteSpace(userRow.Plan) ? plan : userRow.Plan;
                        SeedTenantAdminUser(connections, tenantDb, userRow.DisplayName, userRow.Email, userRow.PasswordHash, userRow.PasswordSalt, seedPlan);
                    }

                }

                // invoke provisioning script to start instance and create service (Linux only)
                var scriptPath = "/opt/advancecrm/provision.sh";
                if (OperatingSystem.IsLinux())
                {
                    if (System.IO.File.Exists(scriptPath))

                    {
                        Process.Start(scriptPath, $"{port} {tenantDb}");
                    }
                    else
                    {
                        new FileNotFoundException($"Provisioning script not found at {scriptPath}").Log("ProvisionTenant");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Log("ProvisionTenant");
            }
        }

        private static bool IsMissingDnsStatusColumn(Exception exception)
        {
            if (exception == null)
                return false;

            var visited = new HashSet<Exception>();
            var stack = new Stack<Exception>();
            stack.Push(exception);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current == null || !visited.Add(current))
                    continue;

                if (current is MicrosoftSqlException microsoftException && ContainsMissingDnsStatusMicrosoft(microsoftException))
                    return true;

                if (current is LegacySqlException legacyException && ContainsMissingDnsStatusLegacy(legacyException))
                    return true;

                if (IsMissingDnsStatusMessage(current.Message))
                    return true;

                if (current is AggregateException aggregateException)
                {
                    foreach (var inner in aggregateException.InnerExceptions)
                    {
                        if (inner != null)
                            stack.Push(inner);
                    }
                }

                if (current.InnerException != null)
                    stack.Push(current.InnerException);
            }

            return false;
        }

        private static bool ContainsMissingDnsStatusMicrosoft(MicrosoftSqlException exception)
        {
            if (exception == null)
                return false;

            foreach (MicrosoftSqlError error in exception.Errors)
            {
                if (IsMissingDnsStatusError(error.Number, error.Message))
                    return true;
            }

            return false;
        }

        private static bool ContainsMissingDnsStatusLegacy(LegacySqlException exception)
        {
            if (exception == null)
                return false;

            foreach (LegacySqlError error in exception.Errors)
            {
                if (IsMissingDnsStatusError(error.Number, error.Message))
                    return true;
            }

            return false;
        }

        private static bool IsMissingDnsStatusError(int number, string message)
        {
            if (number == 207 &&
                !string.IsNullOrWhiteSpace(message) &&
                message.IndexOf("DnsStatus", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        private static bool IsMissingDnsStatusMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return false;

            if (message.IndexOf("DnsStatus", StringComparison.OrdinalIgnoreCase) < 0)
                return false;

            return message.IndexOf("Invalid column", StringComparison.OrdinalIgnoreCase) >= 0 ||
                message.IndexOf("Unknown column", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static int InsertTenantWithoutDnsStatus(IDbConnection connection, string companyName, string subdomain, string tenantDb, int port, string plan, DateTime licenseStartDate, DateTime licenseEndDate, decimal? purchaseAmount, string purchaseCurrency, int? purchasedUsers, string paymentOrderId, string paymentId)
        {
            if (connection is null)
                throw new ArgumentNullException(nameof(connection));

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO [dbo].[Tenants] ([Name], [Subdomain], [DbName], [Port], [Plan], [Modules], [LicenseStartDate], [LicenseEndDate], [PurchaseAmount], [PurchaseCurrency], [PurchasedUsers], [PaymentOrderId], [PaymentId]) VALUES (@Name, @Subdomain, @DbName, @Port, @Plan, @Modules, @LicenseStartDate, @LicenseEndDate, @PurchaseAmount, @PurchaseCurrency, @PurchasedUsers, @PaymentOrderId, @PaymentId);" +
                    "\nSELECT CAST(SCOPE_IDENTITY() AS INT);";

                AddParameter(command, "@Name", companyName);
                AddParameter(command, "@Subdomain", string.IsNullOrEmpty(subdomain) ? null : subdomain);
                AddParameter(command, "@DbName", tenantDb);
                AddParameter(command, "@Port", port);
                AddParameter(command, "@Plan", plan);
                AddParameter(command, "@Modules", DBNull.Value);
                AddParameter(command, "@LicenseStartDate", licenseStartDate);
                AddParameter(command, "@LicenseEndDate", licenseEndDate);
                AddParameter(command, "@PurchaseAmount", purchaseAmount);
                AddParameter(command, "@PurchaseCurrency", purchaseCurrency);
                AddParameter(command, "@PurchasedUsers", purchasedUsers);
                AddParameter(command, "@PaymentOrderId", paymentOrderId);
                AddParameter(command, "@PaymentId", paymentId);

                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        private static void AddParameter(IDbCommand command, string name, object value)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

        private static string BuildDnsFailureStatus(string tenantName, string subdomain, Exception exception)
        {
            var safeSubdomain = string.IsNullOrEmpty(subdomain) ? "<none>" : subdomain;
            var baseMessage = $"Failed to create Cloudflare DNS record for tenant '{tenantName}' using subdomain '{safeSubdomain}'.";

            if (exception is CloudflareProvisioningException cloudflareEx)
            {
                var builder = new StringBuilder(baseMessage);

                if (cloudflareEx.StatusCode.HasValue)
                {
                    builder.Append(" Cloudflare API responded with HTTP ")
                        .Append((int)cloudflareEx.StatusCode.Value)
                        .Append(' ')
                        .Append(cloudflareEx.StatusCode.Value)
                        .Append('.');

                    var hint = string.IsNullOrWhiteSpace(cloudflareEx.DiagnosticHint)
                        ? null
                        : cloudflareEx.DiagnosticHint.Trim();

                    if (!string.IsNullOrEmpty(hint))
                    {
                        builder.Append(' ').Append(hint);
                    }
                    else if (cloudflareEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        builder.Append(" Verify that the API token has DNS edit permissions or configure a valid API key and email.");
                    }
                }
                else
                {
                    builder.Append(" Cloudflare API returned an unexpected error.");
                }

                var response = string.IsNullOrWhiteSpace(cloudflareEx.ResponseContent)
                    ? null
                    : cloudflareEx.ResponseContent.Trim();

                if (!string.IsNullOrEmpty(response))
                {
                    builder.Append(" Response: ").Append(response);
                }
                else if (!string.IsNullOrWhiteSpace(cloudflareEx.Message))
                {
                    builder.Append(' ').Append(cloudflareEx.Message.Trim());
                }

                return builder.ToString();
            }

            var exceptionMessage = exception?.Message;
            if (!string.IsNullOrWhiteSpace(exceptionMessage))
                return $"{baseMessage} {exceptionMessage.Trim()}";

            return baseMessage;
        }

        private static int GenerateFreePort()
        {
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();
            var used = new HashSet<int>(ipProps.GetActiveTcpListeners().Select(p => p.Port));
            var rnd = new Random();
            int port;
            do
            {
                port = rnd.Next(1024, 65535);
            } while (used.Contains(port));
            return port;
        }

        [HttpGet, Route("Activate")]
        public ActionResult Activate(string t,
            [FromServices] ISqlConnections sqlConnections)
        {
            using (var connection = sqlConnections.NewByKey("Default"))
            using (var uow = new UnitOfWork(connection))
            {
                int userId;
                try
                {
                    var bytes = HttpContext.RequestServices
                        .GetDataProtector("Activate").Unprotect(Convert.FromBase64String(t));

                    using (var ms = new MemoryStream(bytes))
                    using (var br = new BinaryReader(ms))
                    {
                        var dt = DateTime.FromBinary(br.ReadInt64());
                        if (dt < DateTime.UtcNow)
                            return Error(Texts.Validation.InvalidActivateToken.ToString(Localizer));

                        userId = br.ReadInt32();
                    }
                }
                catch (Exception)
                {
                    return Error(Texts.Validation.InvalidActivateToken.ToString(Localizer));
                }

                var user = uow.Connection.TryById<UserRow>(userId);
                if (user == null || user.IsActive != false)
                    return Error(Texts.Validation.InvalidActivateToken.ToString(Localizer));

                uow.Connection.UpdateById(new UserRow
                {
                    UserId = user.UserId.Value,
                    IsActive = true
                });

                Cache.InvalidateOnCommit(uow, UserRow.Fields);
                uow.Commit();

                return new RedirectResult("~/Account/Login?activated=" + Uri.EscapeDataString(user.Email));
            }
        }
    }
}









