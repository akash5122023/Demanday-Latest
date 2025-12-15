using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Net.Sockets;

using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;
using AdvanceCRM.Membership;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Settings;
using AdvanceCRM.Razorpay;
using AdvanceCRM.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Serenity.Data;
using Serenity.Services;
using Newtonsoft.Json;

namespace AdvanceCRM.Marketing
{
    [Route("api/public/razorpay")]
    [IgnoreAntiforgeryToken]
    public class RazorpayPublicController : Controller
    {
        private readonly IRazorpayOrderService razorpayOrderService;
        private readonly IConfiguration configuration;
        private readonly ISqlConnections sqlConnections;
        private readonly IRazorpayPlanService planService;
        private readonly ILogger<RazorpayPublicController> logger;
        private readonly ITenantAccessor tenantAccessor;

        private static readonly JsonSerializerOptions RazorpaySerializerOptions = RazorpayJsonOptions.CreateDefault();

        public RazorpayPublicController(
            IRazorpayOrderService razorpayOrderService,
            IConfiguration configuration,
            ISqlConnections sqlConnections,
            IRazorpayPlanService planService,
            ILogger<RazorpayPublicController> logger,
            ITenantAccessor tenantAccessor)
        {
            this.razorpayOrderService = razorpayOrderService;
            this.configuration = configuration;
            this.sqlConnections = sqlConnections ?? throw new ArgumentNullException(nameof(sqlConnections));
            this.planService = planService ?? throw new ArgumentNullException(nameof(planService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));

            // One-time key validation / logging (simple guard: only log once per app lifetime)
            TryValidateAndLogGatewayKeys();
        }

        private static bool keysValidationLogged;

        private void TryValidateAndLogGatewayKeys()
        {
            if (keysValidationLogged)
                return;

            keysValidationLogged = true; // best effort, race harmless

            try
            {
                var (keyId, keySecret) = GetConfiguredKeys();
                var usingService = razorpayOrderService != null && razorpayOrderService.IsEnabled;
                var effectiveKey = usingService ? razorpayOrderService.KeyId : keyId;

                if (string.IsNullOrWhiteSpace(effectiveKey))
                {
                    logger.LogWarning("[Razorpay][Init] No KeyId configured (payments disabled until set via environment variables Razorpay__KeyId / Razorpay__KeySecret).");
                    return;
                }

                if (string.IsNullOrWhiteSpace(keySecret) && (razorpayOrderService == null || !razorpayOrderService.IsEnabled))
                {
                    logger.LogWarning("[Razorpay][Init] KeyId present but KeySecret missing (configure Razorpay__KeySecret env var). KeyId={KeyMasked}", MaskKey(effectiveKey));
                    return;
                }

                logger.LogInformation("[Razorpay][Init] Gateway keys detected. KeyId={KeyMasked} Source={Source} (secret not logged)",
                    MaskKey(effectiveKey), usingService ? "Service" : "Config");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "[Razorpay][Init] Exception while validating gateway keys.");
            }
        }

        private static string MaskKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return "(empty)";
            key = key.Trim();
            if (key.Length <= 8)
                return key.Substring(0, Math.Min(3, key.Length)) + "***";
            return key.Substring(0, 4) + "***" + key.Substring(key.Length - 3);
        }

        /// <summary>
        /// Finalizes a purchase for an already created tenant (e.g. upgrading plan after initial free signup).
        /// Caller supplies Razorpay order/payment identifiers; we verify signature and persist purchase metadata.
        /// </summary>
        [HttpPost("finalize")]
        public async Task<IActionResult> FinalizePurchase([FromBody] RazorpayFinalizeRequest request, CancellationToken ct)
        {
            var correlationId = Guid.NewGuid().ToString("N");
            if (request == null)
                return BadRequest(new { success = false, error = "InvalidRequest", correlationId });

            if (request.TenantId <= 0)
                return BadRequest(new { success = false, error = "TenantRequired", correlationId });

            if (string.IsNullOrWhiteSpace(request.OrderId) || string.IsNullOrWhiteSpace(request.PaymentId) || string.IsNullOrWhiteSpace(request.Signature))
                return BadRequest(new { success = false, error = "PaymentIncomplete", correlationId });

            if (!HasGatewayCredentials())
                return StatusCode(503, new { success = false, error = "PaymentUnavailable", correlationId });

            try
            {
                // Verify signature first
                var trimmedOrderId = request.OrderId.Trim();
                var trimmedPaymentId = request.PaymentId.Trim();
                var trimmedSignature = request.Signature.Trim();

                if (!VerifyPaymentSignature(trimmedOrderId, trimmedPaymentId, trimmedSignature))
                {
                    logger.LogWarning("[{Correlation}] FinalizePurchase signature verification failed. TenantId={TenantId} OrderId={OrderId}", correlationId, request.TenantId, request.OrderId);
                    return BadRequest(new { success = false, error = "PaymentVerificationFailed", correlationId });
                }

                var originalTenant = tenantAccessor.CurrentTenant;
                try
                {
                    // Switch to host context
                    tenantAccessor.CurrentTenant = null;
                    using var connection = sqlConnections.NewByKey("Default");
                    var tFields = Administration.TenantRow.Fields;
                    var tenant = connection.TryFirst<Administration.TenantRow>(q => q
                        .SelectTableFields()
                        .Where(tFields.TenantId == request.TenantId));

                    if (tenant == null)
                    {
                        logger.LogWarning("[{Correlation}] FinalizePurchase tenant not found. TenantId={TenantId}", correlationId, request.TenantId);
                        return NotFound(new { success = false, error = "TenantNotFound", correlationId });
                    }

                    if (!string.IsNullOrWhiteSpace(tenant.PaymentId) && string.Equals(tenant.PaymentId, trimmedPaymentId, StringComparison.OrdinalIgnoreCase))
                    {
                        // Idempotent response
                        return Ok(new { success = true, correlationId, alreadyApplied = true });
                    }

                    // Fetch authoritative order info from Razorpay
                    RazorpayOrderDetails orderDetails;
                    try
                    {
                        orderDetails = await FetchOrderDetailsAsync(trimmedOrderId, ct).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning(ex, "[{Correlation}] Failed to retrieve order details during finalize. TenantId={TenantId} OrderId={OrderId}", correlationId, request.TenantId, request.OrderId);
                        return BadRequest(new { success = false, error = "PaymentVerificationFailed", correlationId });
                    }

                    if (orderDetails == null || string.IsNullOrWhiteSpace(orderDetails.Id) || orderDetails.Amount <= 0)
                        return BadRequest(new { success = false, error = "PaymentAmountInvalid", correlationId });

                    var notes = orderDetails.Notes ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    string notedPlan = null;
                    int? notedUsers = null;
                    int? notedPlanId = null;
                    if (notes.TryGetValue(RazorpayOrderNotes.Plan, out var planNote) && !string.IsNullOrWhiteSpace(planNote))
                        notedPlan = planNote.Trim();
                    if (notes.TryGetValue(RazorpayOrderNotes.Users, out var usersNote) && int.TryParse(usersNote, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedUsers) && parsedUsers > 0)
                        notedUsers = parsedUsers;
                    if (notes.TryGetValue(RazorpayOrderNotes.PlanId, out var planIdNote) && int.TryParse(planIdNote, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedPlanId) && parsedPlanId > 0)
                        notedPlanId = parsedPlanId;
                    List<string> notedModules = null;
                    if (notes.TryGetValue(RazorpayOrderNotes.Modules, out var modulesNote) && !string.IsNullOrWhiteSpace(modulesNote))
                        notedModules = ParseModulesCsv(modulesNote);

                    var previousPlanName = tenant.Plan;

                    var purchasedPlan = ResolvePurchasedPlan(connection, notedPlanId, notedPlan, tenant.Plan, correlationId, request.TenantId);

                    if (purchasedPlan == null && planService != null && !string.IsNullOrWhiteSpace(tenant.Plan))
                    {
                        try
                        {
                            var activePlans = planService.GetActivePlans();
                            var tenantPlan = tenant.Plan.Trim();
                            purchasedPlan = activePlans?.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p?.Name) &&
                                string.Equals(p.Name.Trim(), tenantPlan, StringComparison.OrdinalIgnoreCase));
                        }
                        catch (Exception planEx)
                        {
                            logger.LogWarning(planEx, "[{Correlation}] Unable to resolve tenant plan metadata during finalize fallback. TenantId={TenantId} PlanName={Plan}",
                                correlationId, request.TenantId, tenant.Plan);
                        }
                    }

                    // Determine currency and amount
                    var defaultCurrency = string.IsNullOrWhiteSpace(razorpayOrderService?.Currency)
                        ? "INR"
                        : razorpayOrderService.Currency.Trim().ToUpperInvariant();
                    var currency = string.IsNullOrWhiteSpace(orderDetails.Currency)
                        ? defaultCurrency
                        : orderDetails.Currency.Trim().ToUpperInvariant();
                    var purchaseAmount = orderDetails.Amount / 100m;
                    if (notes.TryGetValue(RazorpayOrderNotes.FinalAmountMinor, out var finalAmountNote) &&
                        decimal.TryParse(finalAmountNote, NumberStyles.Integer, CultureInfo.InvariantCulture, out var finalAmountMinor) && finalAmountMinor > 0)
                    {
                        purchaseAmount = finalAmountMinor / 100m;
                    }

                    // Update only the intended fields to avoid wiping other tenant metadata.
                    tenant.TrackAssignments = true;

                    // Basic sanity: if a plan was provided and tenant already has a different plan, we'll overwrite only if plan is blank or different intentionally
                    var normalizedPreviousPlan = string.IsNullOrWhiteSpace(previousPlanName)
                        ? null
                        : previousPlanName.Trim();
                    var incomingPlanName = !string.IsNullOrWhiteSpace(notedPlan)
                        ? notedPlan.Trim()
                        : purchasedPlan?.Name?.Trim();
                    var planChanged = !string.IsNullOrWhiteSpace(incomingPlanName) &&
                        !string.Equals(normalizedPreviousPlan, incomingPlanName, StringComparison.OrdinalIgnoreCase);

                    if (!string.IsNullOrWhiteSpace(incomingPlanName))
                        tenant.Plan = incomingPlanName;

                    var existingModulesCsv = tenant.Modules;
                    string planModulesCsv = null;
                    if (purchasedPlan != null)
                        planModulesCsv = ResolvePlanModulesCsv(purchasedPlan, connection, correlationId);

                    var planModulesList = string.IsNullOrWhiteSpace(planModulesCsv)
                        ? null
                        : ParseModulesCsv(planModulesCsv);

                    string mergedModules = planChanged ? null : existingModulesCsv;

                    if (planModulesList != null && planModulesList.Count > 0)
                        mergedModules = MergeModulesCsv(mergedModules, planModulesList);

                    if (notedModules != null && notedModules.Count > 0)
                    {
                        tenant.Modules = MergeModulesCsv(mergedModules, notedModules);
                    }
                    else if (planModulesList != null && planModulesList.Count > 0)
                    {
                        tenant.Modules = mergedModules;
                    }
                    else if (!planChanged)
                    {
                        tenant.Modules = existingModulesCsv;
                    }
                    else
                    {
                        tenant.Modules = null;
                    }

                    var resolvedPurchasedUsers = ResolvePurchasedUsers(
                        tenant.PurchasedUsers,
                        notedUsers,
                        purchasedPlan?.UserLimit,
                        planChanged);

                    if (resolvedPurchasedUsers.HasValue)
                        tenant.PurchasedUsers = resolvedPurchasedUsers;

                    var (resolvedAmount, resolvedCurrency) = ResolvePurchaseTotals(
                        tenant.PurchaseAmount,
                        tenant.PurchaseCurrency,
                        purchaseAmount,
                        currency,
                        correlationId);

                    tenant.PurchaseAmount = resolvedAmount;
                    tenant.PurchaseCurrency = resolvedCurrency;
                    tenant.PaymentOrderId = orderDetails.Id;
                    tenant.PaymentId = trimmedPaymentId;

                    // Optionally initialize license dates if not set
                    var (licenseStart, licenseEnd) = CalculateLicenseWindow(purchasedPlan);
                    tenant.LicenseStartDate = licenseStart;
                    tenant.LicenseEndDate = licenseEnd;

                    // Persist inside an explicit transaction so future enhancements (e.g. audit/payment record) remain atomic
                    using (var uow = new UnitOfWork(connection))
                    {
                        try
                        {
                            connection.UpdateById(tenant);
                            uow.Commit();
                        }
                        catch (Exception persistEx)
                        {
                            logger.LogError(persistEx, "[{Correlation}] Failed to persist tenant purchase update (FinalizePurchase). TenantId={TenantId} Plan={Plan} Amount={Amount} Currency={Currency}",
                                correlationId, request.TenantId, tenant.Plan, tenant.PurchaseAmount, tenant.PurchaseCurrency);
                            return StatusCode(500, new { success = false, error = "TenantPersistFailed", correlationId });
                        }
                    }

                    // Re-load to verify (defensive: catch silent triggers overwriting values)
                    Administration.TenantRow postUpdateTenant = null;
                    try
                    {
                        var tFields2 = Administration.TenantRow.Fields;
                        postUpdateTenant = connection.TryFirst<Administration.TenantRow>(q => q
                            .SelectTableFields()
                            .Where(tFields2.TenantId == tenant.TenantId.Value));
                    }
                    catch (Exception reloadEx)
                    {
                        logger.LogWarning(reloadEx, "[{Correlation}] Post-update tenant reload failed (FinalizePurchase). TenantId={TenantId}", correlationId, tenant.TenantId);
                    }

                    if (postUpdateTenant == null)
                    {
                        logger.LogWarning("[{Correlation}] Post-update verification could not find tenant (FinalizePurchase). TenantId={TenantId}", correlationId, tenant.TenantId);
                    }
                    else
                    {
                        // Minimal verification of critical fields
                        if (!string.Equals(postUpdateTenant.PaymentId, trimmedPaymentId, StringComparison.OrdinalIgnoreCase) ||
                            !string.Equals(postUpdateTenant.PaymentOrderId, orderDetails.Id, StringComparison.OrdinalIgnoreCase))
                        {
                            logger.LogWarning("[{Correlation}] Tenant post-update verification mismatch (FinalizePurchase). Expected PaymentId={PaymentId} / OrderId={OrderId} Got PaymentId={GotPaymentId} / OrderId={GotOrderId}",
                                correlationId, trimmedPaymentId, orderDetails.Id, postUpdateTenant.PaymentId, postUpdateTenant.PaymentOrderId);
                        }
                    }

                    logger.LogInformation("[{Correlation}] FinalizePurchase applied. TenantId={TenantId} Plan={Plan} PlanId={PlanId} Users={Users} Amount={Amount} Currency={Currency} LicenseStart={Start} LicenseEnd={End}",
                        correlationId, request.TenantId, tenant.Plan, notedPlanId, tenant.PurchasedUsers, tenant.PurchaseAmount, tenant.PurchaseCurrency, tenant.LicenseStartDate, tenant.LicenseEndDate);
                    return Ok(new { success = true, correlationId });
                }
                finally
                {
                    tenantAccessor.CurrentTenant = originalTenant;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[{Correlation}] Unexpected error in FinalizePurchase. TenantId={TenantId}", correlationId, request?.TenantId);
                return StatusCode(500, new { success = false, error = "FinalizeFailed", correlationId });
            }
        }

        [HttpPost("hosted-purchase")]
        public async Task<IActionResult> ApplyHostedPurchase([FromBody] RazorpayHostedPurchaseRequest request, CancellationToken ct)
        {
            var correlationId = Guid.NewGuid().ToString("N");

            if (request == null)
                return BadRequest(new { success = false, error = "InvalidRequest", correlationId });

            if (string.IsNullOrWhiteSpace(request.OrderId) || string.IsNullOrWhiteSpace(request.PaymentId) || string.IsNullOrWhiteSpace(request.Signature))
                return BadRequest(new { success = false, error = "PaymentIncomplete", correlationId });

            if (!HasGatewayCredentials())
                return StatusCode(503, new { success = false, error = "PaymentUnavailable", correlationId });

            try
            {
                var trimmedOrderId = request.OrderId.Trim();
                var trimmedPaymentId = request.PaymentId.Trim();
                var trimmedSignature = request.Signature.Trim();

                if (!VerifyPaymentSignature(trimmedOrderId, trimmedPaymentId, trimmedSignature))
                {
                    logger.LogWarning("[{Correlation}] Hosted purchase signature verification failed. TenantId={TenantId} Subdomain={Subdomain} OrderId={OrderId}",
                        correlationId, request.TenantId, request.TenantSubdomain, request.OrderId);
                    return BadRequest(new { success = false, error = "PaymentVerificationFailed", correlationId });
                }

                RazorpayOrderDetails orderDetails;
                try
                {
                    orderDetails = await FetchOrderDetailsAsync(trimmedOrderId, ct).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "[{Correlation}] Hosted purchase order fetch failed. TenantId={TenantId} OrderId={OrderId}",
                        correlationId, request.TenantId, request.OrderId);
                    return BadRequest(new { success = false, error = "PaymentVerificationFailed", correlationId });
                }

                if (orderDetails == null || string.IsNullOrWhiteSpace(orderDetails.Id) || orderDetails.Amount <= 0)
                    return BadRequest(new { success = false, error = "PaymentAmountInvalid", correlationId });

                var notes = orderDetails.Notes ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                string notedPlan = null;
                if (notes.TryGetValue(RazorpayOrderNotes.Plan, out var planNote) && !string.IsNullOrWhiteSpace(planNote))
                    notedPlan = planNote.Trim();

                int? notedPlanId = null;
                if (notes.TryGetValue(RazorpayOrderNotes.PlanId, out var planIdNote) &&
                    int.TryParse(planIdNote, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedPlanId) && parsedPlanId > 0)
                    notedPlanId = parsedPlanId;

                int? notedUsers = null;
                if (notes.TryGetValue(RazorpayOrderNotes.Users, out var usersNote) &&
                    int.TryParse(usersNote, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedUsers) && parsedUsers > 0)
                    notedUsers = parsedUsers;

                List<string> notedModules = null;
                if (notes.TryGetValue(RazorpayOrderNotes.Modules, out var modulesNote) && !string.IsNullOrWhiteSpace(modulesNote))
                    notedModules = ParseModulesCsv(modulesNote);

                var requestModules = NormalizeModuleList(request.Modules);
                if (requestModules.Count > 0)
                {
                    notedModules = notedModules == null || notedModules.Count == 0
                        ? requestModules
                        : NormalizeModuleList(notedModules.Concat(requestModules));
                }

                if (!notedPlanId.HasValue && request.PlanId.HasValue && request.PlanId.Value > 0)
                    notedPlanId = request.PlanId.Value;

                if (string.IsNullOrWhiteSpace(notedPlan) && !string.IsNullOrWhiteSpace(request.Plan))
                    notedPlan = request.Plan.Trim();

                if (!notedUsers.HasValue && request.Users.HasValue && request.Users.Value > 0)
                    notedUsers = request.Users.Value;

                var defaultCurrency = string.IsNullOrWhiteSpace(razorpayOrderService?.Currency)
                    ? "INR"
                    : razorpayOrderService.Currency.Trim().ToUpperInvariant();
                var requestedCurrency = string.IsNullOrWhiteSpace(request.Currency)
                    ? defaultCurrency
                    : request.Currency.Trim().ToUpperInvariant();
                var currency = string.IsNullOrWhiteSpace(orderDetails.Currency)
                    ? (!string.IsNullOrWhiteSpace(requestedCurrency) ? requestedCurrency : "INR")
                    : orderDetails.Currency.Trim().ToUpperInvariant();

                if (!string.IsNullOrWhiteSpace(currency))
                    currency = currency.Trim().ToUpperInvariant();

                decimal purchaseAmount = orderDetails.Amount / 100m;
                if (notes.TryGetValue(RazorpayOrderNotes.FinalAmountMinor, out var finalAmountNote) &&
                    int.TryParse(finalAmountNote, NumberStyles.Integer, CultureInfo.InvariantCulture, out var finalAmountMinor) && finalAmountMinor > 0)
                {
                    purchaseAmount = finalAmountMinor / 100m;
                }
                else if (request.AmountMinor.HasValue && request.AmountMinor.Value > 0)
                {
                    purchaseAmount = request.AmountMinor.Value / 100m;
                }
                else if (request.Amount.HasValue && request.Amount.Value > 0)
                {
                    purchaseAmount = request.Amount.Value;
                }

                purchaseAmount = Math.Round(purchaseAmount, 2, MidpointRounding.AwayFromZero);

                int? requestedTenantId = null;
                if (request.TenantId.HasValue && request.TenantId.Value > 0)
                    requestedTenantId = request.TenantId.Value;


                int? notedTenantId = null;
                if (notes.TryGetValue("tenantId", out var tenantIdNote) &&
                    int.TryParse(tenantIdNote, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedTenantId) && parsedTenantId > 0)
                {
                    notedTenantId = parsedTenantId;
                }

                var hostTenant = tenantAccessor?.CurrentTenant;
                if (!requestedTenantId.HasValue && hostTenant != null && hostTenant.TenantId > 0)
                    requestedTenantId = hostTenant.TenantId;
                if (!requestedTenantId.HasValue && notedTenantId.HasValue)
                    requestedTenantId = notedTenantId;

                var preferredSubdomain = NormalizeSubdomain(request.TenantSubdomain);
                if (string.IsNullOrWhiteSpace(preferredSubdomain) && hostTenant != null && !string.IsNullOrWhiteSpace(hostTenant.Subdomain))
                    preferredSubdomain = NormalizeSubdomain(hostTenant.Subdomain);
                if (string.IsNullOrWhiteSpace(preferredSubdomain) && notes.TryGetValue("tenant", out var notedTenant) && !string.IsNullOrWhiteSpace(notedTenant))
                    preferredSubdomain = NormalizeSubdomain(notedTenant);

                var hostCandidate = NormalizeHostCandidate(request.TenantHost);
                if (string.IsNullOrWhiteSpace(hostCandidate))
                    hostCandidate = NormalizeHostCandidate(request.TenantUrl);
                if (string.IsNullOrWhiteSpace(hostCandidate))

                    hostCandidate = NormalizeHostCandidate(HttpContext?.Request?.Host.Value);

                var fallbackSubdomain = !string.IsNullOrWhiteSpace(preferredSubdomain)
                    ? preferredSubdomain
                    : ExtractSubdomainFromHost(hostCandidate);

                Administration.TenantRow tenantRow = null;
                var originalTenant = tenantAccessor.CurrentTenant;
                try
                {
                    tenantAccessor.CurrentTenant = null;
                    using var connection = sqlConnections.NewByKey("Default");
                    var tFields = Administration.TenantRow.Fields;

                    if (requestedTenantId.HasValue)
                    {
                        tenantRow = connection.TryFirst<Administration.TenantRow>(q => q
                            .SelectTableFields()
                            .Where(tFields.TenantId == requestedTenantId.Value));
                    }

                    if (tenantRow == null && !string.IsNullOrWhiteSpace(preferredSubdomain))
                    {
                        tenantRow = connection.TryFirst<Administration.TenantRow>(q => q
                            .SelectTableFields()
                            .Where(tFields.Subdomain == preferredSubdomain));
                    }

                    if (tenantRow == null && !string.IsNullOrWhiteSpace(fallbackSubdomain))
                    {
                        tenantRow = connection.TryFirst<Administration.TenantRow>(q => q
                            .SelectTableFields()
                            .Where(tFields.Subdomain == fallbackSubdomain));
                    }

                    if (tenantRow == null)
                    {
                        logger.LogWarning("[{Correlation}] Hosted purchase tenant not found. TenantId={TenantId} Subdomain={Subdomain} Host={Host}",
                            correlationId, requestedTenantId, preferredSubdomain ?? fallbackSubdomain, hostCandidate);
                        return NotFound(new { success = false, error = "TenantNotFound", correlationId });
                    }

                    if (!string.IsNullOrWhiteSpace(tenantRow.PaymentId) &&
                        string.Equals(tenantRow.PaymentId, trimmedPaymentId, StringComparison.OrdinalIgnoreCase))
                    {
                        logger.LogInformation("[{Correlation}] Hosted purchase already applied earlier. TenantId={TenantId} PaymentId={PaymentId}",
                            correlationId, tenantRow.TenantId, trimmedPaymentId);
                        return Ok(new
                        {
                            success = true,
                            correlationId,
                            tenantId = tenantRow.TenantId,
                            licenseStart = tenantRow.LicenseStartDate,
                            licenseEnd = tenantRow.LicenseEndDate,
                            plan = tenantRow.Plan,
                            alreadyApplied = true
                        });
                    }

                    var previousPlanName = tenantRow?.Plan;

                    var purchasedPlan = ResolvePurchasedPlan(connection, notedPlanId, notedPlan, tenantRow?.Plan, correlationId, tenantRow?.TenantId);
                    var (licenseStart, licenseEnd) = CalculateLicenseWindow(purchasedPlan);

                    tenantRow.TrackAssignments = true;

                    var normalizedPreviousPlan = string.IsNullOrWhiteSpace(previousPlanName)
                        ? null
                        : previousPlanName.Trim();
                    var incomingPlanName = !string.IsNullOrWhiteSpace(notedPlan)
                        ? notedPlan.Trim()
                        : purchasedPlan?.Name?.Trim();
                    var planChanged = !string.IsNullOrWhiteSpace(incomingPlanName) &&
                        !string.Equals(normalizedPreviousPlan, incomingPlanName, StringComparison.OrdinalIgnoreCase);

                    if (!string.IsNullOrWhiteSpace(incomingPlanName))
                        tenantRow.Plan = incomingPlanName;

                    var existingTenantModules = tenantRow.Modules;
                    string planModulesCsv = null;
                    if (purchasedPlan != null)
                        planModulesCsv = ResolvePlanModulesCsv(purchasedPlan, connection, correlationId);

                    var planModulesList = string.IsNullOrWhiteSpace(planModulesCsv)
                        ? null
                        : ParseModulesCsv(planModulesCsv);

                    string mergedModules = planChanged ? null : existingTenantModules;

                    if (planModulesList != null && planModulesList.Count > 0)
                        mergedModules = MergeModulesCsv(mergedModules, planModulesList);

                    if (notedModules != null && notedModules.Count > 0)
                    {
                        tenantRow.Modules = MergeModulesCsv(mergedModules, notedModules);
                    }
                    else if (planModulesList != null && planModulesList.Count > 0)
                    {
                        tenantRow.Modules = mergedModules;
                    }
                    else if (!planChanged)
                    {
                        tenantRow.Modules = existingTenantModules;
                    }
                    else
                    {
                        tenantRow.Modules = null;
                    }

                    var resolvedTenantUsers = ResolvePurchasedUsers(
                        tenantRow.PurchasedUsers,
                        notedUsers,
                        purchasedPlan?.UserLimit,
                        planChanged);

                    if (resolvedTenantUsers.HasValue)
                        tenantRow.PurchasedUsers = resolvedTenantUsers;

                    var (resolvedHostedAmount, resolvedHostedCurrency) = ResolvePurchaseTotals(
                        tenantRow.PurchaseAmount,
                        tenantRow.PurchaseCurrency,
                        purchaseAmount,
                        currency,
                        correlationId);

                    tenantRow.PurchaseAmount = resolvedHostedAmount;
                    tenantRow.PurchaseCurrency = resolvedHostedCurrency;
                    tenantRow.PaymentOrderId = orderDetails.Id;
                    tenantRow.PaymentId = trimmedPaymentId;
                    tenantRow.LicenseStartDate = licenseStart;
                    tenantRow.LicenseEndDate = licenseEnd;
                    using (var uow = new UnitOfWork(connection))
                    {
                        try
                        {
                            connection.UpdateById(tenantRow);
                            uow.Commit();
                        }
                        catch (Exception persistEx)
                        {
                            logger.LogError(persistEx, "[{Correlation}] Failed to persist tenant purchase update (HostedPurchase). TenantId={TenantId} Plan={Plan} Amount={Amount} Currency={Currency}",
                                correlationId, tenantRow.TenantId, tenantRow.Plan, tenantRow.PurchaseAmount, tenantRow.PurchaseCurrency);
                            return StatusCode(500, new { success = false, error = "TenantPersistFailed", correlationId });
                        }
                    }

                    // Post-update verification
                    Administration.TenantRow postUpdateTenant = null;
                    try
                    {
                        var tFields2 = Administration.TenantRow.Fields;
                        postUpdateTenant = connection.TryFirst<Administration.TenantRow>(q => q
                            .SelectTableFields()
                            .Where(tFields2.TenantId == tenantRow.TenantId.Value));
                    }
                    catch (Exception reloadEx)
                    {
                        logger.LogWarning(reloadEx, "[{Correlation}] Post-update tenant reload failed (HostedPurchase). TenantId={TenantId}", correlationId, tenantRow.TenantId);
                    }
                    if (postUpdateTenant == null)
                    {
                        logger.LogWarning("[{Correlation}] Post-update verification could not find tenant (HostedPurchase). TenantId={TenantId}", correlationId, tenantRow.TenantId);
                    }
                    else if (!string.Equals(postUpdateTenant.PaymentId, trimmedPaymentId, StringComparison.OrdinalIgnoreCase) ||
                        !string.Equals(postUpdateTenant.PaymentOrderId, orderDetails.Id, StringComparison.OrdinalIgnoreCase))
                    {
                        logger.LogWarning("[{Correlation}] Tenant post-update verification mismatch (HostedPurchase). Expected PaymentId={PaymentId} / OrderId={OrderId} Got PaymentId={GotPaymentId} / OrderId={GotOrderId}",
                            correlationId, trimmedPaymentId, orderDetails.Id, postUpdateTenant.PaymentId, postUpdateTenant.PaymentOrderId);
                    }

                    logger.LogInformation("[{Correlation}] Hosted purchase applied. TenantId={TenantId} Plan={Plan} PlanId={PlanId} Users={Users} Amount={Amount} Currency={Currency} LicenseStart={Start} LicenseEnd={End}",
                        correlationId, tenantRow.TenantId, tenantRow.Plan, notedPlanId, tenantRow.PurchasedUsers, tenantRow.PurchaseAmount, tenantRow.PurchaseCurrency, tenantRow.LicenseStartDate, tenantRow.LicenseEndDate);

                    return Ok(new
                    {
                        success = true,
                        correlationId,
                        tenantId = tenantRow.TenantId,
                        licenseStart = tenantRow.LicenseStartDate,
                        licenseEnd = tenantRow.LicenseEndDate,
                        plan = tenantRow.Plan
                    });
                }
                finally
                {
                    tenantAccessor.CurrentTenant = originalTenant;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[{Correlation}] Hosted purchase processing failed. TenantId={TenantId}", correlationId, request?.TenantId);
                return StatusCode(500, new { success = false, error = "HostedPurchaseFailed", correlationId });
            }
        }

        [HttpGet("config")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
        public IActionResult GetConfig()
        {
            try
            {
                var plans = LoadActivePlans();
                try
                {
                    if (plans != null)
                    {
                        logger.LogInformation("[Razorpay][Config] Active plans count={Count}. Plans: {Plans}", plans.Count, plans.Select(p => new { p.Id, p.Name, p.IsActive, p.PricePerUser }));
                    }
                }
                catch (Exception logEx)
                {
                    logger.LogWarning(logEx, "Failed to log active plans snapshot in GetConfig.");
                }
                var planResponses = new List<RazorpayClientPlan>();

                if (plans == null || plans.Count == 0)
                {
                    logger.LogWarning("[Razorpay][Config] No active plans found. Returning NoActivePlans error config.");
                    return Ok(new
                    {
                        enabled = false,
                        error = "NoActivePlans",
                        plans = new List<RazorpayClientPlan>()
                    });
                }

                foreach (var plan in plans)
                {
                    if (plan?.Id == null || plan.Name == null || !plan.PricePerUser.HasValue)
                        continue;

                    var currency = string.IsNullOrWhiteSpace(plan.Currency)
                        ? null
                        : plan.Currency.Trim().ToUpperInvariant();

                    planResponses.Add(new RazorpayClientPlan
                    {
                        Id = plan.Id.Value,
                        Name = plan.Name.Trim(),
                        PricePerUser = Math.Round(plan.PricePerUser.Value, 2, MidpointRounding.AwayFromZero),
                        TrialDays = plan.TrialDays ?? 0,
                        UserLimit = plan.UserLimit ?? 0,
                        Currency = currency
                    });
                }

                var configuredKeys = GetConfiguredKeys();

                var serviceEnabled = razorpayOrderService != null && razorpayOrderService.IsEnabled;
                var hasConfiguredGateway = !string.IsNullOrWhiteSpace(configuredKeys.keyId) &&
                    !string.IsNullOrWhiteSpace(configuredKeys.keySecret);

                var currencyCode = ResolveCurrencyCode(plans);
                var displayCurrency = currencyCode;

                if (serviceEnabled && !string.IsNullOrWhiteSpace(razorpayOrderService.Currency))
                    displayCurrency = razorpayOrderService.Currency.Trim().ToUpperInvariant();

                var currencySymbol = ResolveCurrencySymbol(displayCurrency);
                var gatewayEnabled = serviceEnabled || hasConfiguredGateway;
                var resolvedKey = serviceEnabled ? razorpayOrderService.KeyId : configuredKeys.keyId;

                return Ok(new RazorpayPublicConfig
                {
                    Enabled = gatewayEnabled,
                    Key = gatewayEnabled ? resolvedKey : null,
                    Currency = displayCurrency,
                    CurrencySymbol = currencySymbol,
                    CurrencyCode = displayCurrency,
                    Plans = planResponses
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve Razorpay configuration for public checkout.");
                return StatusCode(500, new { error = "ConfigFetchFailed" });
            }
        }

        /// <summary>
        /// Lightweight health check for the public payment endpoint. Does NOT expose secrets.
        /// /api/public/razorpay/health
        /// Optional query: ?checkGateway=true to perform an authenticated probe (lists 1 order) to validate credentials & network.
        /// Never call with checkGateway=true at high frequency (rate limiting risk).
        /// </summary>
        [HttpGet("health")]
        [ResponseCache(NoStore = true, Duration = 0, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Health([FromQuery] bool checkGateway = false, [FromQuery] bool includeBody = false, CancellationToken ct = default)
        {
            var correlationId = Guid.NewGuid().ToString("N");
            var plans = LoadActivePlans();
            var configuredKeys = GetConfiguredKeys();
            var serviceEnabled = razorpayOrderService != null && razorpayOrderService.IsEnabled;
            var gatewayConfigured = !string.IsNullOrWhiteSpace(configuredKeys.keyId) && !string.IsNullOrWhiteSpace(configuredKeys.keySecret);

            bool probePerformed = false;
            bool gatewayReachable = false;
            int? gatewayStatus = null;
            string gatewayCategory = null;
            string gatewayMessage = null;
            string gatewayBodySnippet = null;

            if (checkGateway && gatewayConfigured)
            {
                probePerformed = true;
                try
                {
                    // Use raw HttpClient instead of creating an order (avoid side-effect). Probe orders list.
                    using var http = new HttpClient { BaseAddress = new Uri("https://api.razorpay.com/v1/") };
                    var authBytes = Encoding.ASCII.GetBytes($"{configuredKeys.keyId}:{configuredKeys.keySecret}");
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
                    // minimal list request (count param is supported by Razorpay; if not, still harmless)
                    using var resp = await http.GetAsync("orders?count=1", ct).ConfigureAwait(false);
                    gatewayStatus = (int)resp.StatusCode;
                    gatewayReachable = resp.IsSuccessStatusCode; // 200 expected
                    if (includeBody)
                    {
                        try
                        {
                            var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                            if (!string.IsNullOrWhiteSpace(body))
                            {
                                gatewayBodySnippet = body.Length > 500 ? body.Substring(0, 500) + "…" : body;
                            }
                        }
                        catch { /* ignore body read failures */ }
                    }
                    if (!resp.IsSuccessStatusCode)
                    {
                        gatewayMessage = $"Gateway responded HTTP {(int)resp.StatusCode}";
                        if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized || resp.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            gatewayCategory = "AuthFailed";
                        }
                        else if ((int)resp.StatusCode >= 500)
                        {
                            gatewayCategory = "Upstream5xx";
                        }
                        else
                        {
                            gatewayCategory = "NonSuccess";
                        }
                    }
                    else
                    {
                        gatewayCategory = "OK";
                    }
                }
                catch (HttpRequestException hre)
                {
                    gatewayCategory = ClassifyNetworkError(hre);
                    gatewayMessage = "Network failure while probing gateway";
                }
                catch (TaskCanceledException)
                {
                    gatewayCategory = "Timeout";
                    gatewayMessage = "Probe timed out";
                }
                catch (Exception ex)
                {
                    gatewayCategory = "Unexpected";
                    gatewayMessage = ex.GetType().Name;
                }
            }

            return Ok(new
            {
                ok = true,
                correlationId,
                timestampUtc = DateTime.UtcNow,
                appReady = true,
                activePlanCount = plans?.Count ?? 0,
                serviceEnabled,
                gatewayConfigured,
                keyPresent = !string.IsNullOrWhiteSpace(configuredKeys.keyId),
                secretPresent = !string.IsNullOrWhiteSpace(configuredKeys.keySecret),
                probePerformed,
                gatewayReachable,
                gatewayStatus,
                gatewayCategory,
                gatewayMessage,
                gatewayBodySnippet = includeBody ? gatewayBodySnippet : null
            });
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] RazorpayPublicOrderRequest request, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid().ToString("N");
            if (request == null)
            {
                logger.LogWarning("[{Correlation}] Razorpay order creation attempted with a null request body.", correlationId);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "InvalidRequest", CorrelationId = correlationId });
            }

            var plans = LoadActivePlans();
            if (plans.Count == 0)
            {
                logger.LogWarning("[{Correlation}] Razorpay order creation failed because no active plans are configured.", correlationId);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "PricingUnavailable", AvailablePlans = new List<RazorpayAvailablePlan>(), CorrelationId = correlationId });
            }

            var availablePlans = plans
                .Select(p => new RazorpayAvailablePlan
                {
                    Id = p.Id ?? 0,
                    Name = p.Name,
                    Price = p.PricePerUser ?? 0
                })
                .ToList();

            // Log snapshot of incoming request vs available plans
            try
            {
                logger.LogInformation("[{Correlation}][Razorpay][Order] Incoming request PlanId={PlanId}, Plan={Plan}, Users={Users}, Total={Total}. ActivePlans={Plans}",
                    correlationId, request.PlanId, request.Plan, request.Users, request.Total,
                    plans.Select(p => new { p.Id, p.Name, p.IsActive, p.PricePerUser }));
            }
            catch (Exception exLog)
            {
                logger.LogWarning(exLog, "Failed to log order request snapshot.");
            }

            var planRow = ResolveRequestedPlan(request, plans);
            if (planRow == null || !planRow.PricePerUser.HasValue)
            {
                logger.LogWarning("[{Correlation}] Razorpay order creation failed. Plan could not be resolved. PlanId: {PlanId}, Plan: {Plan}. ActivePlans={Plans}",
                    correlationId, request.PlanId, request.Plan, plans.Select(p => new { p.Id, p.Name, p.IsActive, p.PricePerUser }));
                return BadRequest(new RazorpayPublicOrderResponse {
                    Success = false,
                    Error = "PlanUnavailable",
                    AvailablePlans = availablePlans.ToList(),
                    CorrelationId = correlationId
                });
            }

            var planName = planRow.Name?.Trim();
            if (string.IsNullOrEmpty(planName))
            {
                logger.LogWarning("[{Correlation}] Razorpay order creation failed. Plan name missing for PlanId: {PlanId}", correlationId, planRow.Id);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "PlanUnavailable", CorrelationId = correlationId });
            }

            var pricePerUser = planRow.PricePerUser.Value;
            if (pricePerUser <= 0)
            {
                logger.LogWarning("[{Correlation}] Razorpay order creation failed. Plan price invalid for PlanId: {PlanId}", correlationId, planRow.Id);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "PlanUnavailable", CorrelationId = correlationId });
            }

            var users = request.Users.GetValueOrDefault(1);
            if (users < 1 || users > 9999)
            {
                logger.LogWarning("[{Correlation}] Razorpay order creation failed due to invalid user count. PlanId: {PlanId}, Users: {Users}", correlationId, planRow.Id, users);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "InvalidUserCount", CorrelationId = correlationId });
            }

            var planLimit = planRow.UserLimit ?? 0;
            if (planLimit > 0 && users > planLimit)
            {
                logger.LogInformation("[{Correlation}] Requested user count {Users} exceeds configured plan limit {PlanLimit}. Proceeding with requested count.",
                    correlationId, users, planLimit);
            }

            var total = pricePerUser * users;
            var clientTotal = request.Total ?? 0;
            if (request.Total.HasValue)
            {
                var expected = request.Total.Value;
                if (Math.Abs(expected - total) > 0.5m)
                {
                    logger.LogWarning("[{Correlation}] Razorpay order creation failed due to mismatched amount. ClientTotal={ClientTotal}, ServerTotal={ServerTotal}, Plan={PlanName} ({PlanId}), Users={Users}, PricePerUser={PricePerUser}",
                        correlationId, expected, total, planName, planRow.Id, users, pricePerUser);
                    return BadRequest(new RazorpayPublicOrderResponse
                    {
                        Success = false,
                        Error = "AmountMismatch",
                        AvailablePlans = availablePlans.ToList(),
                        ServerCalculatedTotal = total,
                        ClientProvidedTotal = expected,
                        CorrelationId = correlationId
                    });
                }
            }

            var totalMinorUnits = Convert.ToInt32(Math.Round(total * 100m, MidpointRounding.AwayFromZero));
            if (totalMinorUnits <= 0)
            {
                logger.LogWarning("[{Correlation}] Razorpay order creation failed because total minor units evaluated to zero or negative. PlanId: {PlanId}", correlationId, planRow.Id);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "InvalidAmount", CorrelationId = correlationId });
            }

            var couponCode = string.IsNullOrWhiteSpace(request.CouponCode) ? null : request.CouponCode.Trim();
            CouponCodeRow couponRow = null;
            var discountMinorUnits = 0;

            if (!string.IsNullOrEmpty(couponCode))
            {
                var validationResult = CouponHelper.Validate(couponCode, TryGetCouponByCode, DateTime.UtcNow);
                if (!validationResult.IsValid)
                {
                    var error = validationResult.Error ?? CouponHelper.CouponInvalid;
                    logger.LogWarning("[{Correlation}] Coupon validation failed for code {CouponCode} with error {Error}", correlationId, couponCode, error);
                    return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = error, CorrelationId = correlationId });
                }

                couponRow = validationResult.Coupon;
                discountMinorUnits = CouponHelper.CalculateDiscountMinorUnits(couponRow, totalMinorUnits);
            }

            var finalAmountMinorUnits = Math.Max(0, totalMinorUnits - discountMinorUnits);
            if (finalAmountMinorUnits <= 0)
            {
                logger.LogWarning("[{Correlation}] Coupon application reduced the payable amount to zero. Coupon: {Coupon}", correlationId, couponCode);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = CouponHelper.CouponAmountInvalid, CorrelationId = correlationId });
            }

            var originalAmount = totalMinorUnits / 100m;
            var discountAmount = discountMinorUnits / 100m;
            var finalAmount = finalAmountMinorUnits / 100m;

            var notes = new Dictionary<string, object>
            {
                [RazorpayOrderNotes.Plan] = planName,
                [RazorpayOrderNotes.PlanId] = planRow.Id,
                [RazorpayOrderNotes.Users] = users,
                ["total"] = total.ToString(CultureInfo.InvariantCulture),
                [RazorpayOrderNotes.BaseAmountMinor] = totalMinorUnits.ToString(CultureInfo.InvariantCulture),
                [RazorpayOrderNotes.FinalAmountMinor] = finalAmountMinorUnits.ToString(CultureInfo.InvariantCulture)
            };

            // Enrich notes for traceability (source application + tenant context if available)
            try
            {
                notes["sourceApp"] = "BizPlusERP";
                var currentTenant = tenantAccessor?.CurrentTenant;
                if (currentTenant != null)
                {
                    if (!string.IsNullOrWhiteSpace(currentTenant.Subdomain))
                        notes["tenant"] = currentTenant.Subdomain;
                    else if (currentTenant.TenantId > 0)
                        notes["tenantId"] = currentTenant.TenantId;
                }
            }
            catch { /* non-fatal enrichment */ }

            if (discountMinorUnits > 0)
            {
                notes[RazorpayOrderNotes.CouponDiscountMinor] = discountMinorUnits.ToString(CultureInfo.InvariantCulture);
            }

            if (couponRow != null)
            {
                notes[RazorpayOrderNotes.CouponCode] = couponRow.Code;
                if (!string.IsNullOrWhiteSpace(couponRow.DiscountType))
                    notes[RazorpayOrderNotes.CouponType] = couponRow.DiscountType.Trim();
                if (couponRow.DiscountValue.HasValue)
                    notes[RazorpayOrderNotes.CouponValue] = couponRow.DiscountValue.Value.ToString(CultureInfo.InvariantCulture);
            }

            try
            {

                var usingService = razorpayOrderService != null && razorpayOrderService.IsEnabled;
                RazorpayOrderResult order;
                string keyId;
                string currency;

                var resolvedCurrency = DeterminePlanCurrency(planRow, plans);

                if (usingService)
                {
                    order = await razorpayOrderService.CreateOrderAsync(finalAmountMinorUnits, resolvedCurrency, notes, cancellationToken).ConfigureAwait(false);
                    keyId = razorpayOrderService.KeyId;
                    currency = string.IsNullOrWhiteSpace(order.Currency) ? resolvedCurrency : order.Currency;
                }
                else
                {
                    var fallbackKeys = GetConfiguredKeys();
                    if (string.IsNullOrWhiteSpace(fallbackKeys.keyId) || string.IsNullOrWhiteSpace(fallbackKeys.keySecret))
                    {
                        logger.LogWarning("[{Correlation}] Razorpay order creation failed because gateway keys are not configured.", correlationId);
                        return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "PaymentUnavailable", CorrelationId = correlationId });
                    }

                    currency = string.IsNullOrWhiteSpace(resolvedCurrency) ? ResolveCurrencyCode(plans) : resolvedCurrency;
                    order = await CreateOrderWithConfiguredKeysAsync(finalAmountMinorUnits, currency, notes, fallbackKeys, cancellationToken).ConfigureAwait(false);
                    keyId = fallbackKeys.keyId;
                }

                logger.LogInformation("[{Correlation}] Created Razorpay order {OrderId} for plan {PlanName} with users {UserCount}.", correlationId, order?.Id, planName, users);

                return Ok(new RazorpayPublicOrderResponse
                {
                    Success = true,
                    OrderId = order.Id,
                    Amount = order.Amount,
                    Currency = string.IsNullOrWhiteSpace(order.Currency) ? currency : order.Currency,
                    Key = keyId,
                    CouponCode = couponRow?.Code,
                    OriginalAmount = originalAmount,
                    DiscountAmount = discountAmount,
                    FinalAmount = finalAmount,
                    CorrelationId = correlationId
                });
            }
            catch (ValidationError validationError)
            {
                var message = string.IsNullOrWhiteSpace(validationError.Message)
                    ? "We couldn't start the payment because the request was invalid."
                    : validationError.Message;

                logger.LogWarning(validationError, "[{Correlation}] Validation error occurred while creating Razorpay order for plan {PlanName}.", correlationId, planName);

                return BadRequest(new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = message,
                    AvailablePlans = availablePlans.ToList(),
                    CorrelationId = correlationId
                });
            }
            catch (RazorpayGatewayException gatewayEx)
            {
                var snippet = gatewayEx.GetBodySnippet();
                logger.LogWarning(gatewayEx,
                    "[{Correlation}] Razorpay gateway responded with HTTP {Status}. Code={Code} Description={Description} BodySnippet={Snippet}.",
                    correlationId,
                    (int)gatewayEx.StatusCode,
                    gatewayEx.ErrorCode,
                    gatewayEx.ErrorDescription,
                    snippet);

                var statusCode = (int)gatewayEx.StatusCode;
                    string mappingReason = "OriginalStatusPreserved";
                    if (statusCode < 400 || statusCode > 599)
                    {
                        statusCode = StatusCodes.Status502BadGateway;
                        mappingReason = "OutOfRangeMappedTo502";
                    }
                    else if (statusCode == StatusCodes.Status401Unauthorized || statusCode == StatusCodes.Status403Forbidden)
                    {
                        // Avoid leaking auth semantics directly but record mapping cause
                        statusCode = StatusCodes.Status502BadGateway;
                        mappingReason = "AuthOrForbiddenMappedTo502";
                    }

                var friendlyMessage = !string.IsNullOrWhiteSpace(gatewayEx.ErrorDescription)
                    ? gatewayEx.ErrorDescription
                    : "The payment gateway rejected the request. Please verify the Razorpay credentials and plan configuration.";

                return StatusCode(statusCode, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = friendlyMessage,
                    AvailablePlans = availablePlans.ToList(),
                    CorrelationId = correlationId,
                        GatewayOriginalStatus = (int)gatewayEx.StatusCode,
                        GatewayStatusMappingReason = mappingReason,
                    GatewayError = new RazorpayGatewayErrorInfo
                    {
                        Status = (int)gatewayEx.StatusCode,
                        Code = gatewayEx.ErrorCode,
                        Description = gatewayEx.ErrorDescription,
                        BodySnippet = snippet
                    }
                });
            }
            catch (InvalidOperationException ex)
            {
                logger.LogWarning(ex, "[{Correlation}] Razorpay order creation failed because the payment integration is not configured.", correlationId);
                return StatusCode(503, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = "Online payments are not available right now. Please contact support.",
                    AvailablePlans = availablePlans.ToList(),
                    CorrelationId = correlationId
                });
            }
            catch (HttpRequestException ex)
            {
                    string networkCategory = ClassifyNetworkError(ex);
                    logger.LogWarning(ex, "[{Correlation}] HTTP request to Razorpay failed ({Category}) during order creation for plan {PlanName}.", correlationId, networkCategory, planName);
                    return StatusCode(502, new RazorpayPublicOrderResponse
                    {
                        Success = false,
                        Error = "We couldn't reach the payment gateway. Please try again in a few moments.",
                        AvailablePlans = availablePlans.ToList(),
                        CorrelationId = correlationId,
                        NetworkErrorCategory = networkCategory
                    });
            }
            catch (TaskCanceledException ex)
            {
                logger.LogWarning(ex, "[{Correlation}] Razorpay order creation timed out for plan {PlanName}.", correlationId, planName);
                return StatusCode(504, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = "The payment request timed out. Please try again in a few moments.",
                    AvailablePlans = availablePlans.ToList(),
                    CorrelationId = correlationId
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[{Correlation}] Unexpected error while creating Razorpay order for plan {PlanName}.", correlationId, planName);
                return StatusCode(500, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = "An unexpected error occurred while starting the payment. Please try again later.",
                    AvailablePlans = availablePlans.ToList(),
                    CorrelationId = correlationId
                });
            }

        }

        [HttpPost("create-module-order")]
        public async Task<IActionResult> CreateModuleOrder([FromBody] RazorpayModuleOrderRequest request, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid().ToString("N");

            if (request == null || request.Modules == null || request.Modules.Count == 0)
            {
                logger.LogWarning("[{Correlation}] Module order creation attempted with no modules specified.", correlationId);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "ModulesRequired", CorrelationId = correlationId });
            }

            var requestedModules = NormalizeModuleList(request.Modules);
            if (requestedModules.Count == 0)
            {
                logger.LogWarning("[{Correlation}] Module order creation failed after normalization (empty module list).", correlationId);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "ModulesRequired", CorrelationId = correlationId });
            }

            List<ProductModuleRow> moduleRows;
            var originalTenant = tenantAccessor.CurrentTenant;
            try
            {
                tenantAccessor.CurrentTenant = null;
                using var connection = sqlConnections.NewByKey("Default");
                moduleRows = connection.List<ProductModuleRow>(q => q.SelectTableFields()) ?? new List<ProductModuleRow>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[{Correlation}] Module order creation failed while loading module pricing.", correlationId);
                return StatusCode(500, new RazorpayPublicOrderResponse { Success = false, Error = "ModulePricingUnavailable", CorrelationId = correlationId });
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }

            var matchedModules = new List<ProductModuleRow>();
            foreach (var key in requestedModules)
            {
                var match = moduleRows.FirstOrDefault(m => ModuleKeyMatches(m, key));
                if (match != null && !matchedModules.Any(existing => existing.Id == match.Id))
                {
                    matchedModules.Add(match);
                }
            }

            var missingModules = requestedModules
                .Where(key => !matchedModules.Any(m => ModuleKeyMatches(m, key)))
                .ToList();

            if (missingModules.Count > 0)
            {
                logger.LogWarning("[{Correlation}] Module order creation failed. Missing modules: {Modules}", correlationId, string.Join(", ", missingModules));
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "ModulesUnavailable", CorrelationId = correlationId });
            }

            if (matchedModules.Count == 0)
            {
                logger.LogWarning("[{Correlation}] Module order creation failed. No modules resolved after lookup.", correlationId);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "ModulesUnavailable", CorrelationId = correlationId });
            }

            decimal totalAmount = 0m;
            bool hasPositiveAmount = false;
            string resolvedCurrency = string.IsNullOrWhiteSpace(request.Currency) ? null : request.Currency.Trim().ToUpperInvariant();
            var moduleDisplayNames = new List<string>();

            foreach (var module in matchedModules)
            {
                if (!string.IsNullOrWhiteSpace(module.DisplayName))
                    moduleDisplayNames.Add(module.DisplayName.Trim());
                else if (!string.IsNullOrWhiteSpace(module.Name))
                    moduleDisplayNames.Add(module.Name.Trim());

                if (module.Price.HasValue)
                {
                    totalAmount += module.Price.Value;
                    if (module.Price.Value > 0)
                        hasPositiveAmount = true;
                }

                if (!string.IsNullOrWhiteSpace(module.Currency))
                {
                    var normalizedCurrency = module.Currency.Trim().ToUpperInvariant();
                    if (string.IsNullOrWhiteSpace(resolvedCurrency))
                    {
                        resolvedCurrency = normalizedCurrency;
                    }
                    else if (!string.Equals(resolvedCurrency, normalizedCurrency, StringComparison.Ordinal))
                    {
                        logger.LogWarning("[{Correlation}] Module order currency mismatch detected. Existing={Existing} Module={Module} ModuleId={ModuleId}",
                            correlationId, resolvedCurrency, normalizedCurrency, module.Id);
                        return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "ModuleCurrencyMismatch", CorrelationId = correlationId });
                    }
                }
            }

            if (!hasPositiveAmount || totalAmount <= 0)
            {
                logger.LogWarning("[{Correlation}] Module order creation failed due to missing pricing.", correlationId);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "ModulePricingUnavailable", CorrelationId = correlationId });
            }

            var totalMinorUnits = Convert.ToInt32(Math.Round(totalAmount * 100m, MidpointRounding.AwayFromZero));
            if (totalMinorUnits <= 0)
            {
                logger.LogWarning("[{Correlation}] Module order creation failed because total minor units resolved to zero.", correlationId);
                return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "InvalidAmount", CorrelationId = correlationId });
            }

            if (string.IsNullOrWhiteSpace(resolvedCurrency))
            {
                if (!string.IsNullOrWhiteSpace(razorpayOrderService?.Currency))
                    resolvedCurrency = razorpayOrderService.Currency.Trim().ToUpperInvariant();
                else
                    resolvedCurrency = GetConfiguredCurrency();
            }

            if (string.IsNullOrWhiteSpace(resolvedCurrency))
                resolvedCurrency = "INR";

            var normalizedModuleNames = NormalizeModuleList(moduleDisplayNames);

            var notes = new Dictionary<string, object>
            {
                [RazorpayOrderNotes.BaseAmountMinor] = totalMinorUnits.ToString(CultureInfo.InvariantCulture),
                [RazorpayOrderNotes.FinalAmountMinor] = totalMinorUnits.ToString(CultureInfo.InvariantCulture),
                [RazorpayOrderNotes.Modules] = string.Join(", ", normalizedModuleNames)
            };

            try
            {
                notes["sourceApp"] = "BizPlusERP";
                var currentTenant = tenantAccessor?.CurrentTenant;
                if (currentTenant != null)
                {
                    if (!string.IsNullOrWhiteSpace(currentTenant.Subdomain))
                        notes["tenant"] = currentTenant.Subdomain;
                    else if (currentTenant.TenantId > 0)
                        notes["tenantId"] = currentTenant.TenantId;
                }
            }
            catch
            {
                // Non-fatal enrichment failure
            }

            try
            {
                var usingService = razorpayOrderService != null && razorpayOrderService.IsEnabled;
                RazorpayOrderResult order;
                string keyId;
                string currency;

                if (usingService)
                {
                    order = await razorpayOrderService.CreateOrderAsync(totalMinorUnits, resolvedCurrency, notes, cancellationToken).ConfigureAwait(false);
                    keyId = razorpayOrderService.KeyId;
                    currency = string.IsNullOrWhiteSpace(order.Currency) ? resolvedCurrency : order.Currency;
                }
                else
                {
                    var fallbackKeys = GetConfiguredKeys();
                    if (string.IsNullOrWhiteSpace(fallbackKeys.keyId) || string.IsNullOrWhiteSpace(fallbackKeys.keySecret))
                    {
                        logger.LogWarning("[{Correlation}] Module order creation failed because gateway keys are not configured.", correlationId);
                        return BadRequest(new RazorpayPublicOrderResponse { Success = false, Error = "PaymentUnavailable", CorrelationId = correlationId });
                    }

                    currency = string.IsNullOrWhiteSpace(resolvedCurrency) ? "INR" : resolvedCurrency;
                    order = await CreateOrderWithConfiguredKeysAsync(totalMinorUnits, currency, notes, fallbackKeys, cancellationToken).ConfigureAwait(false);
                    keyId = fallbackKeys.keyId;
                }

                var moduleSummary = string.Join(", ", normalizedModuleNames);
                logger.LogInformation("[{Correlation}] Created Razorpay order {OrderId} for modules {Modules} Amount={Amount} Currency={Currency}.",
                    correlationId, order?.Id, moduleSummary, totalAmount, currency);

                return Ok(new RazorpayPublicOrderResponse
                {
                    Success = true,
                    OrderId = order.Id,
                    Amount = order.Amount,
                    Currency = string.IsNullOrWhiteSpace(order.Currency) ? currency : order.Currency,
                    Key = keyId,
                    OriginalAmount = totalAmount,
                    DiscountAmount = 0m,
                    FinalAmount = totalAmount,
                    CorrelationId = correlationId
                });
            }
            catch (ValidationError validationError)
            {
                var message = string.IsNullOrWhiteSpace(validationError.Message)
                    ? "We couldn't start the payment because the request was invalid."
                    : validationError.Message;

                logger.LogWarning(validationError, "[{Correlation}] Validation error occurred while creating Razorpay module order.", correlationId);

                return BadRequest(new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = message,
                    CorrelationId = correlationId
                });
            }
            catch (RazorpayGatewayException gatewayEx)
            {
                var snippet = gatewayEx.GetBodySnippet();
                logger.LogWarning(gatewayEx,
                    "[{Correlation}] Razorpay gateway responded with HTTP {Status} while creating module order. Code={Code} Description={Description} BodySnippet={Snippet}.",
                    correlationId,
                    (int)gatewayEx.StatusCode,
                    gatewayEx.ErrorCode,
                    gatewayEx.ErrorDescription,
                    snippet);

                var statusCode = (int)gatewayEx.StatusCode;
                string mappingReason = "OriginalStatusPreserved";
                if (statusCode < 400 || statusCode > 599)
                {
                    statusCode = StatusCodes.Status502BadGateway;
                    mappingReason = "OutOfRangeMappedTo502";
                }
                else if (statusCode == StatusCodes.Status401Unauthorized || statusCode == StatusCodes.Status403Forbidden)
                {
                    statusCode = StatusCodes.Status502BadGateway;
                    mappingReason = "AuthOrForbiddenMappedTo502";
                }

                var friendlyMessage = !string.IsNullOrWhiteSpace(gatewayEx.ErrorDescription)
                    ? gatewayEx.ErrorDescription
                    : "The payment gateway rejected the request. Please verify the Razorpay credentials and module pricing.";

                return StatusCode(statusCode, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = friendlyMessage,
                    CorrelationId = correlationId,
                    GatewayError = new RazorpayGatewayErrorInfo
                    {
                        Status = (int)gatewayEx.StatusCode,
                        Code = gatewayEx.ErrorCode,
                        Description = gatewayEx.ErrorDescription,
                        BodySnippet = snippet
                    },
                    GatewayOriginalStatus = (int)gatewayEx.StatusCode,
                    GatewayStatusMappingReason = mappingReason
                });
            }
            catch (InvalidOperationException ex)
            {
                logger.LogWarning(ex, "[{Correlation}] Module order creation failed because the payment integration is not configured.", correlationId);
                return StatusCode(503, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = "Online payments are not available right now. Please contact support.",
                    CorrelationId = correlationId
                });
            }
            catch (HttpRequestException ex)
            {
                var networkCategory = ClassifyNetworkError(ex);
                logger.LogWarning(ex, "[{Correlation}] HTTP request to Razorpay failed ({Category}) during module order creation.", correlationId, networkCategory);
                return StatusCode(502, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = "We couldn't reach the payment gateway. Please try again in a few moments.",
                    CorrelationId = correlationId,
                    NetworkErrorCategory = networkCategory
                });
            }
            catch (TaskCanceledException ex)
            {
                logger.LogWarning(ex, "[{Correlation}] Module order creation timed out.", correlationId);
                return StatusCode(504, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = "The payment request timed out. Please try again in a few moments.",
                    CorrelationId = correlationId
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[{Correlation}] Unexpected error while creating Razorpay module order.", correlationId);
                return StatusCode(500, new RazorpayPublicOrderResponse
                {
                    Success = false,
                    Error = "An unexpected error occurred while starting the payment. Please try again later.",
                    CorrelationId = correlationId
                });
            }
        }

        private static ProductPlanRow ResolveRequestedPlan(RazorpayPublicOrderRequest request, IList<ProductPlanRow> plans)
        {
            if (request == null || plans == null || plans.Count == 0)
                return null;

            ProductPlanRow planRow = null;

            if (request.PlanId.HasValue)
                planRow = plans.FirstOrDefault(p => p.Id == request.PlanId.Value);

            var planKey = request.Plan?.Trim();
            if (planRow == null && !string.IsNullOrEmpty(planKey))
            {
                if (int.TryParse(planKey, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedPlanId))
                    planRow = plans.FirstOrDefault(p => p.Id == parsedPlanId);

                if (planRow == null)
                    planRow = plans.FirstOrDefault(p => string.Equals(p.Name?.Trim(), planKey, StringComparison.OrdinalIgnoreCase));
            }

            return planRow;
        }

        private static async Task<RazorpayOrderResult> CreateOrderWithConfiguredKeysAsync(int amountInMinorUnits, string currency, IDictionary<string, object> notes, (string keyId, string keySecret) credentials, CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.razorpay.com/v1/")
            };

            var authBytes = Encoding.ASCII.GetBytes($"{credentials.keyId}:{credentials.keySecret}");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));

            var payload = new Dictionary<string, object>
            {
                ["amount"] = amountInMinorUnits,
                ["currency"] = currency,
                ["receipt"] = $"signup_{Guid.NewGuid():N}",
                ["payment_capture"] = 1
            };

            if (notes != null && notes.Count > 0)
                payload["notes"] = notes;

            using var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            using var response = await httpClient.PostAsync("orders", content, cancellationToken).ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var (errorCode, errorDescription) = RazorpayGatewayException.ParseErrorPayload(json);
                var snippet = json != null && json.Length > 600 ? json.Substring(0, 600) + "…" : json;
                System.Diagnostics.Debug.WriteLine($"[Razorpay][FallbackCreateOrder] Non-success HTTP {(int)response.StatusCode}. Code={errorCode} Description={errorDescription} BodySnippet={snippet}");
                throw new RazorpayGatewayException(response.StatusCode, errorDescription, json, errorCode, errorDescription);
            }

            var order = System.Text.Json.JsonSerializer.Deserialize<RazorpayOrderResult>(json, RazorpaySerializerOptions);

            if (order == null || string.IsNullOrWhiteSpace(order.Id))
                throw new InvalidOperationException("Unable to create a payment order with Razorpay.");

            return order;

        }

        private ProductPlanRow ResolvePurchasedPlan(IDbConnection connection, int? notedPlanId, string notedPlanName, string tenantPlanName, string correlationId, int? tenantId)
        {
            ProductPlanRow resolvedPlan = null;

            if (planService != null && (notedPlanId.HasValue || !string.IsNullOrWhiteSpace(notedPlanName)))
            {
                try
                {
                    if (notedPlanId.HasValue)
                        resolvedPlan = planService.TryGetActivePlanById(notedPlanId.Value);

                    if (resolvedPlan == null && !string.IsNullOrWhiteSpace(notedPlanName))
                    {
                        var activePlans = planService.GetActivePlans();
                        resolvedPlan = activePlans?.FirstOrDefault(p => !string.IsNullOrWhiteSpace(p?.Name) &&
                            string.Equals(p.Name.Trim(), notedPlanName, StringComparison.OrdinalIgnoreCase));
                    }
                }
                catch (Exception planEx)
                {
                    logger.LogWarning(planEx, "[{Correlation}] Unable to resolve purchased plan metadata using plan service. TenantId={TenantId} PlanId={PlanId} PlanName={Plan}",
                        correlationId, tenantId, notedPlanId, notedPlanName);
                }
            }

            if (resolvedPlan != null || connection == null)
                return resolvedPlan;

            var planFields = ProductPlanRow.Fields;

            if (notedPlanId.HasValue)
            {
                try
                {
                    resolvedPlan = connection.TryFirst<ProductPlanRow>(q => q
                        .SelectTableFields()
                        .Where(planFields.Id == notedPlanId.Value));
                }
                catch (Exception planEx)
                {
                    logger.LogWarning(planEx, "[{Correlation}] Unable to resolve purchased plan metadata by id during finalize. TenantId={TenantId} PlanId={PlanId}",
                        correlationId, tenantId, notedPlanId);
                }
            }

            if (resolvedPlan == null && !string.IsNullOrWhiteSpace(notedPlanName))
            {
                resolvedPlan = TryResolvePlanByName(connection, notedPlanName, correlationId, tenantId);
            }

            if (resolvedPlan == null && !string.IsNullOrWhiteSpace(tenantPlanName))
            {
                resolvedPlan = TryResolvePlanByName(connection, tenantPlanName, correlationId, tenantId);
            }

            return resolvedPlan;
        }

        private ProductPlanRow TryResolvePlanByName(IDbConnection connection, string planName, string correlationId, int? tenantId)
        {
            if (connection == null || string.IsNullOrWhiteSpace(planName))
                return null;

            var trimmed = planName.Trim();
            var planFields = ProductPlanRow.Fields;

            try
            {
                var plan = connection.TryFirst<ProductPlanRow>(q => q
                    .SelectTableFields()
                    .Where(planFields.Name == trimmed & planFields.IsActive == 1));

                if (plan != null)
                    return plan;

                return connection.TryFirst<ProductPlanRow>(q => q
                    .SelectTableFields()
                    .Where(planFields.Name == trimmed));
            }
            catch (Exception planEx)
            {
                logger.LogWarning(planEx, "[{Correlation}] Unable to resolve plan metadata by name. TenantId={TenantId} PlanName={Plan}",
                    correlationId, tenantId, trimmed);
                return null;
            }
        }

        private string ResolvePlanModulesCsv(ProductPlanRow plan, IDbConnection connection, string correlationId)
        {
            if (plan == null)
                return null;

            List<string> moduleNames = null;

            if (plan.ModuleNames != null && plan.ModuleNames.Count > 0)
                moduleNames = new List<string>(plan.ModuleNames);

            if ((moduleNames == null || moduleNames.Count == 0) && plan.Id.HasValue && plan.Id.Value > 0)
            {
                try
                {
                    var enrichedPlan = planService?.TryGetActivePlanById(plan.Id.Value);
                    if (enrichedPlan?.ModuleNames != null && enrichedPlan.ModuleNames.Count > 0)
                        moduleNames = new List<string>(enrichedPlan.ModuleNames);
                }
                catch (Exception ex)
                {
                    logger.LogDebug(ex, "[{Correlation}] Unable to enrich plan modules via plan service. PlanId={PlanId}",
                        correlationId, plan.Id);
                }
            }

            if ((moduleNames == null || moduleNames.Count == 0) && plan.Id.HasValue && plan.Id.Value > 0)
            {
                moduleNames = TryLoadPlanModuleNames(connection, plan.Id.Value, correlationId);
            }

            if (moduleNames == null || moduleNames.Count == 0)
                return null;

            var normalized = moduleNames
                .Select(x => (x ?? string.Empty).Trim())
                .Where(x => x.Length > 0)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            return normalized.Count == 0 ? null : string.Join(", ", normalized);
        }

        private static bool ModuleKeyMatches(ProductModuleRow module, string key)
        {
            if (module == null || string.IsNullOrWhiteSpace(key))
                return false;

            var trimmedKey = key.Trim();
            if (trimmedKey.Length == 0)
                return false;

            if (!string.IsNullOrWhiteSpace(module.Name) &&
                string.Equals(module.Name.Trim(), trimmedKey, StringComparison.OrdinalIgnoreCase))
                return true;

            if (!string.IsNullOrWhiteSpace(module.DisplayName) &&
                string.Equals(module.DisplayName.Trim(), trimmedKey, StringComparison.OrdinalIgnoreCase))
                return true;

            if (module.Id.HasValue &&
                string.Equals(module.Id.Value.ToString(CultureInfo.InvariantCulture), trimmedKey, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private static List<string> NormalizeModuleList(IEnumerable<string> modules)
        {
            if (modules == null)
                return new List<string>();

            return modules
                .Select(m => (m ?? string.Empty).Trim())
                .Where(m => m.Length > 0)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static List<string> ParseModulesCsv(string csv)
        {
            if (string.IsNullOrWhiteSpace(csv))
                return new List<string>();

            var separators = new[] { ',', ';', '|' };
            var parts = csv.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return NormalizeModuleList(parts);
        }

        private static string MergeModulesCsv(string existingModules, IEnumerable<string> additionalModules)
        {
            var baseList = ParseModulesCsv(existingModules);
            var additions = NormalizeModuleList(additionalModules);

            foreach (var module in additions)
            {
                if (!baseList.Contains(module, StringComparer.OrdinalIgnoreCase))
                    baseList.Add(module);
            }

            return baseList.Count == 0 ? null : string.Join(", ", baseList);
        }

        private List<string> TryLoadPlanModuleNames(IDbConnection connection, int planId, string correlationId)
        {
            if (connection == null)
                return new List<string>();

            var names = new List<string>();

            try
            {
                using var command = connection.CreateCommand();
                command.CommandText = @"SELECT pm.DisplayName
                    FROM ProductPlanModules ppm
                    INNER JOIN ProductModules pm ON pm.Id = ppm.ModuleId
                    WHERE ppm.PlanId = @PlanId AND pm.IsActive = 1
                    ORDER BY COALESCE(pm.SortOrder, 2147483647), pm.DisplayName";

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@PlanId";
                parameter.Value = planId;
                command.Parameters.Add(parameter);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        names.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "[{Correlation}] Failed to load plan module metadata from database. PlanId={PlanId}",
                    correlationId, planId);
            }

            return names;
        }

        private (DateTime Start, DateTime End) CalculateLicenseWindow(ProductPlanRow plan)
        {
            var start = DateTime.Today;

            int planTrialDays = (plan?.TrialDays).GetValueOrDefault(0);
            int? configuredDefault = null;
            try
            {
                configuredDefault = configuration?.GetValue<int?>("TrialSettings:DefaultDays");
            }
            catch
            {
            }

            int effectiveDays;
            if (planTrialDays > 0)
                effectiveDays = planTrialDays;
            else if (configuredDefault.HasValue && configuredDefault.Value > 0)
                effectiveDays = configuredDefault.Value;
            else
                effectiveDays = 7;

            if (effectiveDays < 1)
                effectiveDays = 1;
            if (effectiveDays > 365)
                effectiveDays = 365;

            var end = effectiveDays == 1 ? start : start.AddDays(effectiveDays - 1);
            return (start, end);
        }

        private (decimal? Amount, string Currency) ResolvePurchaseTotals(decimal? existingAmount, string existingCurrency,
            decimal additionalAmount, string additionalCurrency, string correlationId)
        {
            var normalizedExistingCurrency = NormalizeCurrency(existingCurrency);
            var normalizedAdditionalCurrency = NormalizeCurrency(additionalCurrency);

            decimal? resolvedAmount = existingAmount;
            var resolvedCurrency = normalizedExistingCurrency;

            if (additionalAmount > 0m)
            {
                if (resolvedAmount.HasValue && resolvedAmount.Value > 0m)
                {
                    if (string.IsNullOrEmpty(resolvedCurrency))
                    {
                        resolvedAmount = resolvedAmount.Value + additionalAmount;
                        resolvedCurrency = normalizedAdditionalCurrency;
                    }
                    else if (string.IsNullOrEmpty(normalizedAdditionalCurrency) ||
                        string.Equals(resolvedCurrency, normalizedAdditionalCurrency, StringComparison.Ordinal))
                    {
                        resolvedAmount = resolvedAmount.Value + additionalAmount;
                    }
                    else
                    {
                        logger.LogWarning("[{Correlation}] Purchase currency mismatch while aggregating totals. Existing={ExistingCurrency} New={NewCurrency}",
                            correlationId, resolvedCurrency, normalizedAdditionalCurrency);
                        resolvedAmount = additionalAmount;
                        resolvedCurrency = normalizedAdditionalCurrency;
                    }
                }
                else
                {
                    resolvedAmount = additionalAmount;
                    if (!string.IsNullOrEmpty(normalizedAdditionalCurrency))
                        resolvedCurrency = normalizedAdditionalCurrency;
                }
            }

            if (resolvedAmount.HasValue)
                resolvedAmount = Math.Round(resolvedAmount.Value, 2, MidpointRounding.AwayFromZero);

            if (string.IsNullOrWhiteSpace(resolvedCurrency))
                resolvedCurrency = null;

            return (resolvedAmount, resolvedCurrency);
        }

        private static string NormalizeCurrency(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return null;

            return currency.Trim().ToUpperInvariant();
        }

        private static int? ResolvePurchasedUsers(int? existingUsers, int? notedUsers, int? planUserLimit, bool planChanged)
        {
            if (planChanged)
            {
                int? replacementUsers = null;

                if (notedUsers.HasValue && notedUsers.Value > 0)
                    replacementUsers = notedUsers.Value;

                if (planUserLimit.HasValue && planUserLimit.Value > 0)
                {
                    if (replacementUsers.HasValue)
                        replacementUsers = Math.Max(replacementUsers.Value, planUserLimit.Value);
                    else
                        replacementUsers = planUserLimit.Value;
                }

                if (replacementUsers.HasValue)
                    return replacementUsers.Value;

                return existingUsers;
            }

            if (notedUsers.HasValue && notedUsers.Value > 0)
            {
                var additionalUsers = notedUsers.Value;
                var currentUsers = existingUsers.GetValueOrDefault();

                if (currentUsers > 0)
                {
                    var combined = (long)Math.Max(currentUsers, 0) + additionalUsers;
                    if (combined > int.MaxValue)
                        combined = int.MaxValue;

                    return (int)combined;
                }

                var initialUsers = additionalUsers;
                if (planUserLimit.HasValue && planUserLimit.Value > initialUsers)
                    initialUsers = planUserLimit.Value;

                return initialUsers;
            }

            if (planUserLimit.HasValue && planUserLimit.Value > 0 &&
                (!existingUsers.HasValue || existingUsers.Value <= 0))
            {
                return planUserLimit.Value;
            }

            return existingUsers;
        }

        private static string NormalizeSubdomain(string subdomain)
        {
            if (string.IsNullOrWhiteSpace(subdomain))
                return null;

            var trimmed = subdomain.Trim().Trim('.');

            if (string.IsNullOrWhiteSpace(trimmed))
                return null;

            return trimmed.ToLowerInvariant();
        }

        private static bool LooksLikeLocalhost(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
                return false;

            var candidate = host.Trim();
            if (candidate.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                return true;

            if (candidate.EndsWith(".localhost", StringComparison.OrdinalIgnoreCase))
                return true;

            if (candidate.StartsWith("127.", StringComparison.OrdinalIgnoreCase) || candidate.StartsWith("::1", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;

        }

        private static string NormalizeHostCandidate(string hostOrUrl)
        {
            if (string.IsNullOrWhiteSpace(hostOrUrl))
                return null;

            var candidate = hostOrUrl.Trim();

            if (candidate.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                candidate.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                if (Uri.TryCreate(candidate, UriKind.Absolute, out var uri))
                    candidate = uri.Host;
            }

            var slashIndex = candidate.IndexOf('/');
            if (slashIndex >= 0)
                candidate = candidate.Substring(0, slashIndex);

            var colonIndex = candidate.IndexOf(':');
            if (colonIndex >= 0)
                candidate = candidate.Substring(0, colonIndex);

            candidate = candidate.Trim().Trim('.');
            return string.IsNullOrWhiteSpace(candidate) ? null : candidate;
        }

        private static string ExtractSubdomainFromHost(string host)
        {
            var normalized = NormalizeHostCandidate(host);
            if (string.IsNullOrWhiteSpace(normalized))
                return null;


            if (LooksLikeLocalhost(normalized))
                return null;


            var segments = normalized.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 0)
                return null;

            if (segments.Length == 1)

                return null;

            if (string.Equals(segments[0], "www", StringComparison.OrdinalIgnoreCase))
            {
                if (segments.Length <= 2)
                    return null;

                return NormalizeSubdomain(segments[1]);
            }

            if (segments.Length <= 2)
                return null;

            return NormalizeSubdomain(segments[0]);

        }

        private CouponCodeRow TryGetCouponByCode(string couponCode)
        {
            if (string.IsNullOrWhiteSpace(couponCode))
                return null;

            var trimmed = couponCode.Trim();
            var originalTenant = tenantAccessor.CurrentTenant;

            try
            {
                tenantAccessor.CurrentTenant = null;

                using var connection = sqlConnections.NewFor<CouponCodeRow>();
                var fields = CouponCodeRow.Fields;

                return connection.TryFirst<CouponCodeRow>(q => q
                    .SelectTableFields()
                    .Where(fields.Code == trimmed));
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }
        }

        private IList<ProductPlanRow> LoadActivePlans()
        {
            var originalTenant = tenantAccessor.CurrentTenant;

            try
            {
                tenantAccessor.CurrentTenant = null;
                return planService.GetActivePlans();
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }
        }

        private bool HasGatewayCredentials()
        {
            if (razorpayOrderService != null && razorpayOrderService.IsEnabled)
                return true;

            var (keyId, keySecret) = GetConfiguredKeys();
            return !string.IsNullOrWhiteSpace(keyId) && !string.IsNullOrWhiteSpace(keySecret);
        }

        private bool VerifyPaymentSignature(string orderId, string paymentId, string signature)
        {
            if (razorpayOrderService != null && razorpayOrderService.IsEnabled)
                return razorpayOrderService.VerifySignature(orderId, paymentId, signature);

            var (_, keySecret) = GetConfiguredKeys();
            if (string.IsNullOrWhiteSpace(keySecret))
                return false;

            try
            {
                var payload = $"{orderId}|{paymentId}";
                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(keySecret));
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                var computed = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
                return string.Equals(computed, signature?.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "[Razorpay] Fallback signature verification failed.");
                return false;
            }
        }

        private async Task<RazorpayOrderDetails> FetchOrderDetailsAsync(string orderId, CancellationToken ct)
        {
            if (razorpayOrderService != null && razorpayOrderService.IsEnabled)
                return await razorpayOrderService.GetOrderAsync(orderId, ct).ConfigureAwait(false);

            var (keyId, keySecret) = GetConfiguredKeys();
            if (string.IsNullOrWhiteSpace(keyId) || string.IsNullOrWhiteSpace(keySecret))
                throw new InvalidOperationException("Razorpay gateway credentials are not configured.");

            using var httpClient = new HttpClient { BaseAddress = new Uri("https://api.razorpay.com/v1/") };
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{keyId}:{keySecret}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            using var response = await httpClient.GetAsync($"orders/{orderId}", ct).ConfigureAwait(false);
            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var (errorCode, errorDescription) = RazorpayGatewayException.ParseErrorPayload(body);
                throw new RazorpayGatewayException(response.StatusCode,
                    errorDescription ?? "Unable to retrieve Razorpay order details.",
                    body,
                    errorCode,
                    errorDescription);
            }

            var details = System.Text.Json.JsonSerializer.Deserialize<RazorpayOrderDetails>(body, RazorpaySerializerOptions);
            if (details == null || string.IsNullOrWhiteSpace(details.Id))
                throw new InvalidOperationException("Unable to retrieve Razorpay order details.");

            if (details.Notes == null)
                details.Notes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            else
                details.Notes = new Dictionary<string, string>(details.Notes, StringComparer.OrdinalIgnoreCase);

            return details;
        }

        private (string keyId, string keySecret) GetConfiguredKeys()
        {
            // 1) Primary: environment / appsettings binding
            var section = configuration?.GetSection(RazorpaySettings.SectionKey);
            var keyId = section?.GetValue<string>("KeyId");
            var keySecret = section?.GetValue<string>("KeySecret");

            if (!string.IsNullOrWhiteSpace(keyId)) keyId = keyId.Trim();
            if (!string.IsNullOrWhiteSpace(keySecret)) keySecret = keySecret.Trim();

            if (!string.IsNullOrWhiteSpace(keyId) && !string.IsNullOrWhiteSpace(keySecret))
                return (keyId!, keySecret!);

            // 2) Fallback: pull from central SaaS settings table (host context)
            var originalTenant = tenantAccessor.CurrentTenant;
            try
            {
                tenantAccessor.CurrentTenant = null;
                using var connection = sqlConnections.NewByKey("Default");
                try
                {
                    var dict = connection.Query("SELECT [Key],[Value] FROM SassApplicationSetting WHERE [Key] IN ('Razorpay.KeyId','Razorpay.KeySecret')");
                    foreach (var row in dict)
                    {
                        var k = row.Key as string;
                        var v = row.Value as string ?? string.Empty;
                        if (string.Equals(k, "Razorpay.KeyId", System.StringComparison.OrdinalIgnoreCase))
                            keyId = v;
                        else if (string.Equals(k, "Razorpay.KeySecret", System.StringComparison.OrdinalIgnoreCase))
                            keySecret = v;
                    }
                }
                catch { /* table may not exist yet */ }
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }

            if (!string.IsNullOrWhiteSpace(keyId)) keyId = keyId.Trim(); else keyId = string.Empty;
            if (!string.IsNullOrWhiteSpace(keySecret)) keySecret = keySecret.Trim(); else keySecret = string.Empty;
            return (keyId, keySecret);
        }

        private string GetConfiguredCurrency()
        {
            var section = configuration?.GetSection(RazorpaySettings.SectionKey);
            var currency = section?.GetValue<string>("Currency");
            return string.IsNullOrWhiteSpace(currency) ? string.Empty : currency.Trim().ToUpperInvariant();
        }

        private string DeterminePlanCurrency(ProductPlanRow plan, IList<ProductPlanRow> plans)
        {
            if (plan != null && !string.IsNullOrWhiteSpace(plan.Currency))
                return plan.Currency.Trim().ToUpperInvariant();

            return ResolveCurrencyCode(plans);
        }

        private string ResolveCurrencyCode(IList<ProductPlanRow> plans)
        {
            var planCurrency = plans?
                .Where(p => !string.IsNullOrWhiteSpace(p.Currency))
                .Select(p => p.Currency.Trim())
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(planCurrency))
                return planCurrency.ToUpperInvariant();

            if (!string.IsNullOrWhiteSpace(razorpayOrderService?.Currency))
                return razorpayOrderService.Currency.Trim().ToUpperInvariant();

            var configuredCurrency = GetConfiguredCurrency();
            if (!string.IsNullOrWhiteSpace(configuredCurrency))
                return configuredCurrency.Trim().ToUpperInvariant();

            return "INR";
        }

        private static string ResolveCurrencySymbol(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                return "₹";

            try
            {
                var region = CultureInfo
                    .GetCultures(CultureTypes.SpecificCultures)
                    .Select(c => new RegionInfo(c.LCID))
                    .FirstOrDefault(r => string.Equals(r.ISOCurrencySymbol, currencyCode, StringComparison.OrdinalIgnoreCase));

                if (region != null)
                    return region.CurrencySymbol;
            }
            catch
            {
            }

            return currencyCode;
        }

        private sealed class RazorpayPublicConfig
        {
            [JsonProperty("enabled")]
            [JsonPropertyName("enabled")]
            public bool Enabled { get; set; }

            [JsonProperty("key")]
            [JsonPropertyName("key")]
            public string Key { get; set; }

            [JsonProperty("currency")]
            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonProperty("currencySymbol")]
            [JsonPropertyName("currencySymbol")]
            public string CurrencySymbol { get; set; }

            [JsonProperty("currencyCode")]
            [JsonPropertyName("currencyCode")]
            public string CurrencyCode { get; set; }

            [JsonProperty("plans")]
            [JsonPropertyName("plans")]
            public IList<RazorpayClientPlan> Plans { get; set; } = new List<RazorpayClientPlan>();
        }


        public sealed class RazorpayHostedPurchaseRequest
        {
            [JsonProperty("tenantId")]
            [JsonPropertyName("tenantId")]
            public int? TenantId { get; set; }

            [JsonProperty("tenantSubdomain")]
            [JsonPropertyName("tenantSubdomain")]
            public string TenantSubdomain { get; set; }

            [JsonProperty("tenantHost")]
            [JsonPropertyName("tenantHost")]
            public string TenantHost { get; set; }

            [JsonProperty("tenantUrl")]
            [JsonPropertyName("tenantUrl")]
            public string TenantUrl { get; set; }

            [JsonProperty("plan")]
            [JsonPropertyName("plan")]
            public string Plan { get; set; }

            [JsonProperty("planId")]
            [JsonPropertyName("planId")]
            public int? PlanId { get; set; }

            [JsonProperty("users")]
            [JsonPropertyName("users")]
            public int? Users { get; set; }

            [JsonProperty("orderId")]
            [JsonPropertyName("orderId")]
            public string OrderId { get; set; }

            [JsonProperty("paymentId")]
            [JsonPropertyName("paymentId")]
            public string PaymentId { get; set; }

            [JsonProperty("signature")]
            [JsonPropertyName("signature")]
            public string Signature { get; set; }

            [JsonProperty("couponCode")]
            [JsonPropertyName("couponCode")]
            public string CouponCode { get; set; }

            [JsonProperty("amountMinor")]
            [JsonPropertyName("amountMinor")]
            public int? AmountMinor { get; set; }

            [JsonProperty("amount")]
            [JsonPropertyName("amount")]
            public decimal? Amount { get; set; }

            [JsonProperty("currency")]
            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonProperty("modules")]
            [JsonPropertyName("modules")]
            public List<string> Modules { get; set; } = new List<string>();
        }

        public sealed class RazorpayPublicOrderRequest

        {
            [JsonProperty("planId")]
            [JsonPropertyName("planId")]
            public int? PlanId { get; set; }

            public string Plan { get; set; }

            public int? Users { get; set; }

            public decimal? Total { get; set; }

            public string CouponCode { get; set; }
        }

        public sealed class RazorpayModuleOrderRequest
        {
            [JsonProperty("modules")]
            [JsonPropertyName("modules")]
            public List<string> Modules { get; set; } = new List<string>();

            [JsonProperty("currency")]
            [JsonPropertyName("currency")]
            public string Currency { get; set; }
        }

    private sealed class RazorpayPublicOrderResponse
        {
            [JsonProperty("success")]
            [JsonPropertyName("success")]
            public bool Success { get; set; }

            [JsonProperty("error")]
            [JsonPropertyName("error")]
            public string Error { get; set; }

            [JsonProperty("orderId")]
            [JsonPropertyName("orderId")]
            public string OrderId { get; set; }

            [JsonProperty("amount")]
            [JsonPropertyName("amount")]
            public int Amount { get; set; }

            [JsonProperty("currency")]
            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonProperty("key")]
            [JsonPropertyName("key")]
            public string Key { get; set; }

            [JsonPropertyName("couponCode")]
            public string CouponCode { get; set; }

            [JsonPropertyName("originalAmount")]
            public decimal OriginalAmount { get; set; }

            [JsonPropertyName("discountAmount")]
            public decimal DiscountAmount { get; set; }

            [JsonPropertyName("finalAmount")]
            public decimal FinalAmount { get; set; }

            [JsonProperty("availablePlans")]
            [JsonPropertyName("availablePlans")]
            public List<RazorpayAvailablePlan> AvailablePlans { get; set; }

            [JsonProperty("serverCalculatedTotal")]
            [JsonPropertyName("serverCalculatedTotal")]
            public decimal? ServerCalculatedTotal { get; set; }

            [JsonProperty("clientProvidedTotal")]
            [JsonPropertyName("clientProvidedTotal")]
            public decimal? ClientProvidedTotal { get; set; }

            [JsonProperty("correlationId")]
            [JsonPropertyName("correlationId")]
            public string CorrelationId { get; set; }

            [JsonProperty("gatewayError")]
            [JsonPropertyName("gatewayError")]
            public RazorpayGatewayErrorInfo GatewayError { get; set; }

            [JsonProperty("gatewayOriginalStatus")]
            [JsonPropertyName("gatewayOriginalStatus")]
            public int? GatewayOriginalStatus { get; set; }

            [JsonProperty("gatewayStatusMappingReason")]
            [JsonPropertyName("gatewayStatusMappingReason")]
            public string GatewayStatusMappingReason { get; set; }

            [JsonProperty("networkErrorCategory")]
            [JsonPropertyName("networkErrorCategory")]
            public string NetworkErrorCategory { get; set; }
        }

        public sealed class RazorpayAvailablePlan
        {
            [JsonProperty("id")]
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonProperty("price")]
            [JsonPropertyName("price")]
            public decimal Price { get; set; }
        }

        private static string ClassifyNetworkError(HttpRequestException ex)
        {
            if (ex == null) return "Unknown";
            var inner = ex.InnerException;
            if (inner is SocketException sock)
            {
                return sock.SocketErrorCode switch
                {
                    SocketError.HostNotFound => "DNSLookupFailed",
                    SocketError.HostUnreachable => "HostUnreachable",
                    SocketError.NetworkDown => "NetworkDown",
                    SocketError.TimedOut => "SocketTimeout",
                    _ => "SocketError"
                };
            }
            if (inner is TaskCanceledException)
                return "RequestCanceled";
            return ex.StatusCode.HasValue ? $"HttpStatus_{(int)ex.StatusCode.Value}" : "HttpRequestException";
        }

        public sealed class RazorpayFinalizeRequest
        {
            [JsonProperty("tenantId")]
            [JsonPropertyName("tenantId")]
            public int TenantId { get; set; }

            [JsonProperty("orderId")]
            [JsonPropertyName("orderId")]
            public string OrderId { get; set; }

            [JsonProperty("paymentId")]
            [JsonPropertyName("paymentId")]
            public string PaymentId { get; set; }

            [JsonProperty("signature")]
            [JsonPropertyName("signature")]
            public string Signature { get; set; }
        }

        public sealed class RazorpayGatewayErrorInfo
        {
            [JsonProperty("status")]
            [JsonPropertyName("status")]
            public int? Status { get; set; }

            [JsonProperty("code")]
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonProperty("description")]
            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonProperty("bodySnippet")]
            [JsonPropertyName("bodySnippet")]
            public string BodySnippet { get; set; }
        }

        // (Reverted) Custom converter for flexible notes removed as per user request.
    }
}
