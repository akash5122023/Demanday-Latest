using AdvanceCRM.Administration;
using AdvanceCRM.Demanday;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serenity;
using Serenity.Abstractions;
using Serenity.Data;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdvanceCRM.EmailVerification.Pages
{
    /// <summary>
    /// Email Verification tool page. The verification / trace / bulk endpoints below are
    /// wired up as stubs for now — the actual verification API will be plugged in later,
    /// so only the marked "TODO: call real API" sections need to change at that point.
    /// </summary>
    [PageAuthorize("EmailVerification:Read")]
    public class EmailVerificationController : Controller
    {
        [Route("EmailVerification")]
        public ActionResult Index()
        {
            return View("~/Modules/EmailVerification/EmailVerificationIndex.cshtml");
        }

        // Single email verification via ZeroBounce. The API key lives in
        // appsettings.json under "EmailVerification:ZeroBounceApiKey".
        //
        // Two guards wrap the actual API call:
        //  1) Shared cache — if the email was already verified by anyone, the stored Valid/Invalid
        //     result is returned for free (no API call, no quota spent).
        //  2) Per-user quota — a fresh (uncached) verification consumes one of the user's allowance;
        //     when the allowance is exhausted the request is refused.
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/Verify")]
        public async Task<JsonResult> Verify([FromForm] string email,
            [FromServices] IConfiguration configuration,
            [FromServices] IHttpClientFactory httpClientFactory,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IUserAccessor userAccessor,
            [FromServices] IPermissionService permissions)
        {
            if (!CanVerify(permissions))
                return new JsonResult(new EmailVerificationResult
                {
                    Success = false,
                    Message = "You do not have permission to run verifications. You can search and view existing results only."
                });

            if (string.IsNullOrWhiteSpace(email))
                return new JsonResult(new EmailVerificationResult
                {
                    Success = false,
                    Message = "Please enter an email address."
                });

            var trimmed = email.Trim();
            var key = trimmed.ToLowerInvariant();
            var userId = GetCurrentUserId(userAccessor);

            // 1) Already verified by someone? Return the shared result without spending a check.
            var cached = FindCached(sqlConnections, key);
            if (cached != null)
            {
                var q = GetQuota(sqlConnections, configuration, userId, ensureRow: false);
                return new JsonResult(new EmailVerificationResult
                {
                    Success = true,
                    Email = trimmed,
                    Status = string.IsNullOrEmpty(cached.Status) ? "unknown" : cached.Status,
                    Message = BuildVerifyMessage(cached.Status, cached.SubStatus),
                    FromCache = true,
                    VerifiedDate = cached.VerifiedDate?.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture),
                    Allowed = q.Allowed,
                    Used = q.Used,
                    Remaining = q.Remaining
                });
            }

            // 2) Not cached — enforce the per-user search limit before hitting the API.
            var quota = GetQuota(sqlConnections, configuration, userId, ensureRow: false);
            if (quota.Remaining <= 0)
                return new JsonResult(new EmailVerificationResult
                {
                    Success = false,
                    Email = trimmed,
                    LimitReached = true,
                    Allowed = quota.Allowed,
                    Used = quota.Used,
                    Remaining = 0,
                    Message = "You have reached your verification limit (" + quota.Used + " / " +
                        quota.Allowed + "). Please contact an administrator to increase it."
                });

            var apiKey = GetApiKey(sqlConnections, configuration);
            if (string.IsNullOrWhiteSpace(apiKey))
                return new JsonResult(new EmailVerificationResult
                {
                    Success = false,
                    Email = trimmed,
                    Message = ApiKeyMissingMessage,
                    Allowed = quota.Allowed,
                    Used = quota.Used,
                    Remaining = quota.Remaining
                });

            try
            {
                var url = "https://api.zerobounce.net/v2/validate?api_key=" +
                          Uri.EscapeDataString(apiKey) +
                          "&email=" + Uri.EscapeDataString(trimmed) +
                          "&ip_address=";

                var client = httpClientFactory.CreateClient();
                using var apiResponse = await client.GetAsync(url);
                var body = await apiResponse.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(body);
                var root = doc.RootElement;

                // ZeroBounce reports key/credit problems as an "error" string (or "Message").
                // Those are not real verifications, so they neither spend quota nor get cached.
                var apiError = GetJsonString(root, "error") ?? GetJsonString(root, "Message");
                if (!string.IsNullOrEmpty(apiError))
                    return new JsonResult(new EmailVerificationResult
                    {
                        Success = false,
                        Email = trimmed,
                        Message = apiError,
                        Allowed = quota.Allowed,
                        Used = quota.Used,
                        Remaining = quota.Remaining
                    });

                // status: valid | invalid | catch-all | unknown | spamtrap | abuse | do_not_mail
                var status = GetJsonString(root, "status");
                var subStatus = GetJsonString(root, "sub_status");
                status = string.IsNullOrEmpty(status) ? "unknown" : status;

                // A real verification happened: spend one from the quota and store the result so
                // every other user benefits from it next time.
                var after = ConsumeQuota(sqlConnections, configuration, userId);
                SaveCached(sqlConnections, key, status, subStatus,
                    BuildVerifyMessage(status, subStatus), userId);

                return new JsonResult(new EmailVerificationResult
                {
                    Success = true,
                    Email = trimmed,
                    Status = status,
                    Message = BuildVerifyMessage(status, subStatus),
                    FromCache = false,
                    Allowed = after.Allowed,
                    Used = after.Used,
                    Remaining = after.Remaining
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new EmailVerificationResult
                {
                    Success = false,
                    Email = trimmed,
                    Message = "Verification request failed: " + ex.Message,
                    Allowed = quota.Allowed,
                    Used = quota.Used,
                    Remaining = quota.Remaining
                });
            }
        }

        // ZeroBounce only exposes validation, not mail-route tracing, so Trace runs the same
        // verification and reports the deliverability result (subject to the same cache + quota).
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/Trace")]
        public Task<JsonResult> Trace([FromForm] string email,
            [FromServices] IConfiguration configuration,
            [FromServices] IHttpClientFactory httpClientFactory,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IUserAccessor userAccessor,
            [FromServices] IPermissionService permissions)
        {
            return Verify(email, configuration, httpClientFactory, sqlConnections, userAccessor, permissions);
        }

        // Reports the signed-in user's current quota (used / allowed / remaining) and whether they
        // may manage other users' quotas. The page shows this and refreshes it after each verify.
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/QuotaStatus")]
        public JsonResult QuotaStatus(
            [FromServices] IConfiguration configuration,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IUserAccessor userAccessor,
            [FromServices] IPermissionService permissions)
        {
            var userId = GetCurrentUserId(userAccessor);
            var q = GetQuota(sqlConnections, configuration, userId, ensureRow: false);
            return new JsonResult(new QuotaStatusResult
            {
                Success = true,
                Allowed = q.Allowed,
                Used = q.Used,
                Remaining = q.Remaining,
                CanManageQuota = permissions != null && permissions.HasPermission(ManageQuotaPermission),
                CanVerify = CanVerify(permissions)
            });
        }

        private static bool CanVerify(IPermissionService permissions)
            => permissions != null && permissions.HasPermission(VerifyPermission);

        private static string BuildVerifyMessage(string status, string subStatus)
        {
            var detail = string.IsNullOrWhiteSpace(subStatus)
                ? ""
                : " (" + subStatus.Replace('_', ' ') + ")";

            switch ((status ?? "").ToLowerInvariant())
            {
                case "valid": return "Deliverable — the mailbox exists and accepts mail.";
                case "invalid": return "Undeliverable — the mailbox does not exist." + detail;
                case "catch-all": return "Catch-all domain — accepts any address, so existence can't be confirmed.";
                case "spamtrap": return "Spam trap — do not mail this address.";
                case "abuse": return "Abuse address — the recipient marks mail as spam.";
                case "do_not_mail": return "Do not mail — role/disposable/toxic address." + detail;
                case "unknown": return "Could not be verified (mail server did not give a clear answer)." + detail;
                default:
                    return string.IsNullOrEmpty(detail) ? "Verification completed." : detail.Trim();
            }
        }

        private static string GetJsonString(JsonElement root, string propertyName)
        {
            if (root.ValueKind == JsonValueKind.Object &&
                root.TryGetProperty(propertyName, out var prop) &&
                prop.ValueKind == JsonValueKind.String)
                return prop.GetString();
            return null;
        }

        // Shortest term we will run a query for. The page searches as the user types, so
        // a single character would scan both contact tables on nearly every keystroke.
        private const int MinSearchLength = 2;

        // Row cap per source table, so a broad term like "@" can't pull the whole table.
        private const int PerSourceLimit = 50;

        /// <summary>
        /// As-you-type contact lookup: matches the term anywhere inside Email and returns
        /// hits from both DemandayContacts and DemandayTeleMarketingContacts.
        /// </summary>
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/SearchContacts")]
        public JsonResult SearchContacts([FromForm] string email,
            [FromServices] ISqlConnections sqlConnections)
        {
            var term = email?.Trim();
            if (string.IsNullOrEmpty(term) || term.Length < MinSearchLength)
                return new JsonResult(new ContactSearchResult { Success = true });

            // Escape LIKE wildcards so a term such as "100%" or "a_b" is matched literally.
            var pattern = EscapeLikePattern(term);
            var result = new ContactSearchResult { Success = true };

            // If the term is itself a full email that has already been verified by anyone, surface
            // that shared Valid/Invalid result so the user knows the outcome before spending a check.
            if (LooksLikeEmail(term))
            {
                var cached = FindCached(sqlConnections, term.ToLowerInvariant());
                if (cached != null)
                {
                    result.CacheHit = true;
                    result.CachedEmail = cached.Email;
                    result.CachedStatus = string.IsNullOrEmpty(cached.Status) ? "unknown" : cached.Status;
                    result.CachedMessage = BuildVerifyMessage(cached.Status, cached.SubStatus);
                    result.CachedVerifiedDate = cached.VerifiedDate?.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture);
                }
            }

            using (var connection = sqlConnections.NewFor<DemandayContactsRow>())
            {
                var f = DemandayContactsRow.Fields;
                // Take one extra row purely to detect that the cap was hit.
                var rows = connection.List<DemandayContactsRow>(q => q
                    .Select(f.Id).Select(f.CampaignId).Select(f.FirstName).Select(f.LastName)
                    .Select(f.Email).Select(f.CompanyName).Select(f.Title)
                    .Select(f.WorkPhone).Select(f.Country)
                    .Where(new Criteria(f.Email).Contains(pattern))
                    .OrderBy(f.Email)
                    .Take(PerSourceLimit + 1));

                if (rows.Count > PerSourceLimit)
                {
                    result.Truncated = true;
                    rows = rows.Take(PerSourceLimit).ToList();
                }

                result.Items.AddRange(rows.Select(r => new ContactSearchItem
                {
                    Source = "ETContact",
                    Id = r.Id,
                    CampaignId = r.CampaignId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Email = r.Email,
                    CompanyName = r.CompanyName,
                    Title = r.Title,
                    WorkPhone = r.WorkPhone,
                    Country = r.Country
                }));
            }

            using (var connection = sqlConnections.NewFor<DemandayTeleMarketingContactsRow>())
            {
                var f = DemandayTeleMarketingContactsRow.Fields;
                var rows = connection.List<DemandayTeleMarketingContactsRow>(q => q
                    .Select(f.Id).Select(f.CampaignId).Select(f.FirstName).Select(f.LastName)
                    .Select(f.Email).Select(f.CompanyName).Select(f.Title)
                    .Select(f.WorkPhone).Select(f.Country)
                    .Where(new Criteria(f.Email).Contains(pattern))
                    .OrderBy(f.Email)
                    .Take(PerSourceLimit + 1));

                if (rows.Count > PerSourceLimit)
                {
                    result.Truncated = true;
                    rows = rows.Take(PerSourceLimit).ToList();
                }

                result.Items.AddRange(rows.Select(r => new ContactSearchItem
                {
                    Source = "TM ETContact",
                    Id = r.Id,
                    CampaignId = r.CampaignId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Email = r.Email,
                    CompanyName = r.CompanyName,
                    Title = r.Title,
                    WorkPhone = r.WorkPhone,
                    Country = r.Country
                }));
            }

            // Tag every returned contact with its known verification result (from single or bulk
            // verifications) so the user sees "verified / not verified" right in the search grid.
            AttachCachedStatuses(sqlConnections, result.Items);

            if (result.Truncated)
                result.Message = "Showing the first " + PerSourceLimit +
                    " matches per module. Refine the search to narrow it down.";

            return new JsonResult(result);
        }

        // Looks up the shared cache once for all the search hits and stamps each item with its
        // stored status/message, so the grid can show which emails are already verified.
        private static void AttachCachedStatuses(ISqlConnections sqlConnections, List<ContactSearchItem> items)
        {
            var keys = items
                .Select(i => i.Email?.Trim().ToLowerInvariant())
                .Where(e => !string.IsNullOrEmpty(e))
                .Distinct()
                .ToList();
            if (keys.Count == 0)
                return;

            using var connection = sqlConnections.NewFor<EmailVerificationResultRow>();
            var f = EmailVerificationResultRow.Fields;
            var cached = connection.List<EmailVerificationResultRow>(q => q
                .SelectTableFields()
                .Where(new Criteria(f.Email).In(keys)));

            var byEmail = cached
                .Where(c => !string.IsNullOrEmpty(c.Email))
                .GroupBy(c => c.Email.ToLowerInvariant())
                .ToDictionary(g => g.Key, g => g.First());

            foreach (var it in items)
            {
                var key = it.Email?.Trim().ToLowerInvariant();
                if (key != null && byEmail.TryGetValue(key, out var c))
                {
                    it.CachedStatus = string.IsNullOrEmpty(c.Status) ? "unknown" : c.Status;
                    it.CachedMessage = c.Message ?? BuildVerifyMessage(c.Status, c.SubStatus);
                    it.CachedVerifiedDate = c.VerifiedDate?.ToString("dd MMM yyyy HH:mm", CultureInfo.InvariantCulture);
                }
            }
        }

        // SQL Server treats %, _, [ and ] as LIKE metacharacters. Wrapping each in brackets
        // makes it match literally without needing an ESCAPE clause.
        private static string EscapeLikePattern(string value)
        {
            return value
                .Replace("[", "[[]")
                .Replace("%", "[%]")
                .Replace("_", "[_]");
        }

        // Permission that unlocks the quota-management panel and its endpoints.
        private const string ManageQuotaPermission = "EmailVerification:ManageQuota";

        // Permission required to run actual verifications (single / trace / bulk). Users without
        // it may still open the page, search contacts and see already-known statuses.
        private const string VerifyPermission = "EmailVerification:Verify";

        // Quota granted to a user who has no explicit row yet. Overridable via
        // appsettings.json -> "EmailVerification:DefaultQuota".
        private const int DefaultQuotaFallback = 50;

        // ---- Admin: per-user quota management (gated by EmailVerification:ManageQuota) ----

        /// <summary>Lists every active user with their current allowed / used verification counts.</summary>
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/ListQuota")]
        public JsonResult ListQuota(
            [FromServices] IConfiguration configuration,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IPermissionService permissions)
        {
            if (permissions == null || !permissions.HasPermission(ManageQuotaPermission))
                return new JsonResult(new QuotaListResult { Success = false, Message = "You are not allowed to manage quotas." });

            var defaultQuota = GetDefaultQuota(sqlConnections, configuration);
            var result = new QuotaListResult { Success = true, CanManageQuota = true };

            using var connection = sqlConnections.NewFor<UserRow>();

            var uf = UserRow.Fields;
            var users = connection.List<UserRow>(q => q
                .Select(uf.UserId).Select(uf.Username).Select(uf.DisplayName)
                .Where(new Criteria(uf.IsActive) == 1)
                .OrderBy(uf.Username));

            var quotas = connection.List<EmailVerificationQuotaRow>(q => q.SelectTableFields());
            var byUser = quotas.Where(x => x.UserId != null)
                .GroupBy(x => x.UserId.Value)
                .ToDictionary(g => g.Key, g => g.First());

            foreach (var u in users)
            {
                if (u.UserId == null)
                    continue;
                byUser.TryGetValue(u.UserId.Value, out var qr);
                result.Items.Add(new QuotaAdminItem
                {
                    UserId = u.UserId.Value,
                    Username = u.Username,
                    DisplayName = u.DisplayName,
                    AllowedCount = qr?.AllowedCount ?? defaultQuota,
                    UsedCount = qr?.UsedCount ?? 0
                });
            }

            return new JsonResult(result);
        }

        /// <summary>Sets a user's allowed verification count (and optionally resets their used count).</summary>
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/SetQuota")]
        public JsonResult SetQuota([FromForm] int userId, [FromForm] int allowedCount, [FromForm] bool resetUsed,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IPermissionService permissions)
        {
            if (permissions == null || !permissions.HasPermission(ManageQuotaPermission))
                return new JsonResult(new SimpleResult { Success = false, Message = "You are not allowed to manage quotas." });

            if (userId <= 0)
                return new JsonResult(new SimpleResult { Success = false, Message = "Invalid user." });
            if (allowedCount < 0)
                allowedCount = 0;

            using var connection = sqlConnections.NewFor<EmailVerificationQuotaRow>();
            var f = EmailVerificationQuotaRow.Fields;
            var row = connection.TryFirst<EmailVerificationQuotaRow>(q => q
                .SelectTableFields()
                .Where(new Criteria(f.UserId) == userId));

            if (row == null)
            {
                connection.InsertAndGetID(new EmailVerificationQuotaRow
                {
                    UserId = userId,
                    AllowedCount = allowedCount,
                    UsedCount = 0
                });
            }
            else
            {
                row.AllowedCount = allowedCount;
                if (resetUsed)
                    row.UsedCount = 0;
                connection.UpdateById(row);
            }

            return new JsonResult(new SimpleResult { Success = true, Message = "Saved." });
        }

        // ---- Admin: API setup (ZeroBounce key + default quota), gated by ManageQuota ----

        /// <summary>Returns the current runtime settings so the admin "API Setup" form can show them.</summary>
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/GetSettings")]
        public JsonResult GetSettings(
            [FromServices] IConfiguration configuration,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IPermissionService permissions)
        {
            if (permissions == null || !permissions.HasPermission(ManageQuotaPermission))
                return new JsonResult(new SettingsResult { Success = false, Message = "You are not allowed to manage settings." });

            var row = GetSettingsRow(sqlConnections);
            var dbKey = row?.ApiKey;
            var configKey = configuration?["EmailVerification:ZeroBounceApiKey"];

            string source;
            if (!string.IsNullOrWhiteSpace(dbKey)) source = "database";
            else if (!string.IsNullOrWhiteSpace(configKey)) source = "config";
            else source = "none";

            return new JsonResult(new SettingsResult
            {
                Success = true,
                // Only the DB key is editable here; a key that lives in appsettings is not echoed back.
                ApiKey = dbKey,
                HasApiKey = source != "none",
                Source = source,
                DefaultQuota = row?.DefaultQuota ?? GetDefaultQuota(sqlConnections, configuration),
                CanManage = true
            });
        }

        /// <summary>Saves (or clears) the ZeroBounce API key and default quota from the admin form.</summary>
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/SaveSettings")]
        public JsonResult SaveSettings([FromForm] string apiKey, [FromForm] int? defaultQuota,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IUserAccessor userAccessor,
            [FromServices] IPermissionService permissions)
        {
            if (permissions == null || !permissions.HasPermission(ManageQuotaPermission))
                return new JsonResult(new SimpleResult { Success = false, Message = "You are not allowed to manage settings." });

            // A blank key clears the DB override (verification then falls back to appsettings, if any).
            var key = string.IsNullOrWhiteSpace(apiKey) ? null : apiKey.Trim();
            var quota = (defaultQuota.HasValue && defaultQuota.Value >= 0) ? defaultQuota : null;
            var userId = GetCurrentUserId(userAccessor);

            using var connection = sqlConnections.NewFor<EmailVerificationSettingsRow>();
            var existing = connection.TryFirst<EmailVerificationSettingsRow>(q => q.SelectTableFields());

            if (existing == null)
            {
                connection.InsertAndGetID(new EmailVerificationSettingsRow
                {
                    ApiKey = key,
                    DefaultQuota = quota,
                    UpdatedByUserId = userId,
                    UpdatedDate = DateTime.Now
                });
            }
            else
            {
                existing.ApiKey = key;
                existing.DefaultQuota = quota;
                existing.UpdatedByUserId = userId;
                existing.UpdatedDate = DateTime.Now;
                connection.UpdateById(existing);
            }

            return new JsonResult(new SimpleResult
            {
                Success = true,
                Message = key == null ? "API key cleared." : "Settings saved."
            });
        }

        // ---- Shared cache + quota helpers ----

        // Runtime settings row (ZeroBounce key + default quota) as set from the admin "API Setup"
        // form. Null when the form has never been saved.
        private static EmailVerificationSettingsRow GetSettingsRow(ISqlConnections sqlConnections)
        {
            using var connection = sqlConnections.NewFor<EmailVerificationSettingsRow>();
            return connection.TryFirst<EmailVerificationSettingsRow>(q => q.SelectTableFields());
        }

        // Effective API key: the value saved from the admin form wins; otherwise fall back to
        // appsettings.json so existing server configs keep working.
        private static string GetApiKey(ISqlConnections sqlConnections, IConfiguration configuration)
        {
            var row = GetSettingsRow(sqlConnections);
            if (row != null && !string.IsNullOrWhiteSpace(row.ApiKey))
                return row.ApiKey.Trim();
            return configuration?["EmailVerification:ZeroBounceApiKey"];
        }

        // Shown when no key is available anywhere, pointing the admin at the form.
        private const string ApiKeyMissingMessage =
            "Verification API key is not configured. An administrator can add it from " +
            "Email Verification → Manage Quotas → API Setup.";

        private static int GetDefaultQuota(ISqlConnections sqlConnections, IConfiguration configuration)
        {
            var row = GetSettingsRow(sqlConnections);
            if (row != null && row.DefaultQuota.HasValue && row.DefaultQuota.Value >= 0)
                return row.DefaultQuota.Value;
            var raw = configuration?["EmailVerification:DefaultQuota"];
            if (int.TryParse(raw, out var v) && v >= 0)
                return v;
            return DefaultQuotaFallback;
        }

        private static int? GetCurrentUserId(IUserAccessor userAccessor)
        {
            var id = userAccessor?.User?.GetIdentifier();
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out var uid))
                return uid;
            return null;
        }

        private static bool LooksLikeEmail(string s)
        {
            if (string.IsNullOrEmpty(s) || s.IndexOf(' ') >= 0)
                return false;
            var at = s.IndexOf('@');
            return at > 0 && at < s.Length - 1 && s.IndexOf('.', at) > at;
        }

        private static EmailVerificationResultRow FindCached(ISqlConnections sqlConnections, string emailKey)
        {
            using var connection = sqlConnections.NewFor<EmailVerificationResultRow>();
            var f = EmailVerificationResultRow.Fields;
            return connection.TryFirst<EmailVerificationResultRow>(q => q
                .SelectTableFields()
                .Where(new Criteria(f.Email) == emailKey));
        }

        private static void SaveCached(ISqlConnections sqlConnections, string emailKey,
            string status, string subStatus, string message, int? userId)
        {
            using var connection = sqlConnections.NewFor<EmailVerificationResultRow>();
            var f = EmailVerificationResultRow.Fields;
            var existing = connection.TryFirst<EmailVerificationResultRow>(q => q
                .SelectTableFields()
                .Where(new Criteria(f.Email) == emailKey));

            if (existing != null)
            {
                existing.Status = status;
                existing.SubStatus = subStatus;
                existing.Message = message;
                existing.VerifiedByUserId = userId;
                existing.VerifiedDate = DateTime.Now;
                connection.UpdateById(existing);
            }
            else
            {
                connection.InsertAndGetID(new EmailVerificationResultRow
                {
                    Email = emailKey,
                    Status = status,
                    SubStatus = subStatus,
                    Message = message,
                    VerifiedByUserId = userId,
                    VerifiedDate = DateTime.Now
                });
            }
        }

        private static QuotaSnapshot GetQuota(ISqlConnections sqlConnections, IConfiguration configuration,
            int? userId, bool ensureRow)
        {
            var defaultQuota = GetDefaultQuota(sqlConnections, configuration);
            if (userId == null)
                return new QuotaSnapshot { Allowed = defaultQuota, Used = 0, Remaining = defaultQuota };

            using var connection = sqlConnections.NewFor<EmailVerificationQuotaRow>();
            var f = EmailVerificationQuotaRow.Fields;
            var row = connection.TryFirst<EmailVerificationQuotaRow>(q => q
                .SelectTableFields()
                .Where(new Criteria(f.UserId) == userId.Value));

            if (row == null)
            {
                if (ensureRow)
                {
                    row = new EmailVerificationQuotaRow { UserId = userId, AllowedCount = defaultQuota, UsedCount = 0 };
                    connection.InsertAndGetID(row);
                }
                else
                {
                    return new QuotaSnapshot { Allowed = defaultQuota, Used = 0, Remaining = defaultQuota };
                }
            }

            var allowed = row.AllowedCount ?? defaultQuota;
            var used = row.UsedCount ?? 0;
            return new QuotaSnapshot { Allowed = allowed, Used = used, Remaining = Math.Max(0, allowed - used) };
        }

        // Adds one to the user's used count (creating the row on first use), returning the new state.
        private static QuotaSnapshot ConsumeQuota(ISqlConnections sqlConnections, IConfiguration configuration, int? userId)
        {
            var defaultQuota = GetDefaultQuota(sqlConnections, configuration);
            if (userId == null)
                return new QuotaSnapshot { Allowed = defaultQuota, Used = 0, Remaining = defaultQuota };

            using var connection = sqlConnections.NewFor<EmailVerificationQuotaRow>();
            var f = EmailVerificationQuotaRow.Fields;
            var row = connection.TryFirst<EmailVerificationQuotaRow>(q => q
                .SelectTableFields()
                .Where(new Criteria(f.UserId) == userId.Value));

            if (row == null)
                row = new EmailVerificationQuotaRow { UserId = userId, AllowedCount = defaultQuota, UsedCount = 0 };

            var allowed = row.AllowedCount ?? defaultQuota;
            var used = (row.UsedCount ?? 0) + 1;
            row.AllowedCount = allowed;
            row.UsedCount = used;

            if (row.Id == null)
                connection.InsertAndGetID(row);
            else
                connection.UpdateById(row);

            return new QuotaSnapshot { Allowed = allowed, Used = used, Remaining = Math.Max(0, allowed - used) };
        }

        // Adds <count> to the user's used count (creating the row on first use), returning the new
        // state. Used by bulk verification, where one upload verifies many emails at once.
        private static QuotaSnapshot ConsumeQuotaBy(ISqlConnections sqlConnections, IConfiguration configuration,
            int? userId, int count)
        {
            var defaultQuota = GetDefaultQuota(sqlConnections, configuration);
            if (userId == null || count <= 0)
                return GetQuota(sqlConnections, configuration, userId, ensureRow: false);

            using var connection = sqlConnections.NewFor<EmailVerificationQuotaRow>();
            var f = EmailVerificationQuotaRow.Fields;
            var row = connection.TryFirst<EmailVerificationQuotaRow>(q => q
                .SelectTableFields()
                .Where(new Criteria(f.UserId) == userId.Value));

            if (row == null)
                row = new EmailVerificationQuotaRow { UserId = userId, AllowedCount = defaultQuota, UsedCount = 0 };

            var allowed = row.AllowedCount ?? defaultQuota;
            var used = (row.UsedCount ?? 0) + count;
            row.AllowedCount = allowed;
            row.UsedCount = used;

            if (row.Id == null)
                connection.InsertAndGetID(row);
            else
                connection.UpdateById(row);

            return new QuotaSnapshot { Allowed = allowed, Used = used, Remaining = Math.Max(0, allowed - used) };
        }

        private class QuotaSnapshot
        {
            public int Allowed;
            public int Used;
            public int Remaining;
        }

        // Bulk email verification (file upload).
        // TODO: accept the uploaded file, hand it to the real bulk API and return a job id.
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/BulkVerify")]
        public async Task<JsonResult> BulkVerify([FromForm] IFormFile file,
            [FromServices] IConfiguration configuration,
            [FromServices] IHttpClientFactory httpClientFactory,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IPermissionService permissions)
        {
            if (!CanVerify(permissions))
                return new JsonResult(new BulkVerificationResult
                {
                    Success = false,
                    Message = "You do not have permission to run bulk verification."
                });

            if (file == null || file.Length == 0)
                return new JsonResult(new BulkVerificationResult
                {
                    Success = false,
                    Message = "Please choose a file with an 'Email Address' column."
                });

            var apiKey = GetApiKey(sqlConnections, configuration);
            if (string.IsNullOrWhiteSpace(apiKey))
                return new JsonResult(new BulkVerificationResult
                {
                    Success = false,
                    Message = ApiKeyMissingMessage
                });

            try
            {
                // ZeroBounce bulk only accepts CSV/TXT — it rejects Excel with
                // "File format is not supported, please use only CSV or TXT files."
                // Our Bulk Template is an .xlsx, so convert any Excel upload to CSV here;
                // CSV/TXT uploads are forwarded as-is.
                Stream uploadStream;
                string uploadName;
                if (IsExcelFile(file.FileName))
                {
                    uploadStream = ConvertExcelToCsv(file);
                    uploadName = System.IO.Path.GetFileNameWithoutExtension(file.FileName) + ".csv";
                }
                else
                {
                    uploadStream = file.OpenReadStream();
                    uploadName = file.FileName;
                }

                // ZeroBounce bulk is asynchronous: upload the file, then poll for status and
                // download the results. The upload sheet is [Firstname | Email] with a header row,
                // so the email lives in column 2 and the first name in column 1.
                using var form = new MultipartFormDataContent();
                form.Add(new StringContent(apiKey), "api_key");
                form.Add(new StringContent("2"), "email_address_column");
                form.Add(new StringContent("1"), "first_name_column");
                form.Add(new StringContent("true"), "has_header_row");
                form.Add(new StreamContent(uploadStream), "file", uploadName);

                var client = httpClientFactory.CreateClient();
                using var apiResponse = await client.PostAsync("https://bulkapi.zerobounce.net/v2/sendfile", form);
                var body = await apiResponse.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(body);
                var root = doc.RootElement;

                if (root.TryGetProperty("success", out var okProp) &&
                    okProp.ValueKind == JsonValueKind.True)
                {
                    return new JsonResult(new BulkVerificationResult
                    {
                        Success = true,
                        FileId = GetJsonString(root, "file_id"),
                        Message = GetJsonString(root, "message") ?? "File accepted."
                    });
                }

                return new JsonResult(new BulkVerificationResult
                {
                    Success = false,
                    Message = GetJsonString(root, "message") ?? GetJsonString(root, "error") ?? "Upload was rejected."
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new BulkVerificationResult
                {
                    Success = false,
                    Message = "Bulk upload failed: " + ex.Message
                });
            }
        }

        private static bool IsExcelFile(string fileName)
        {
            var ext = System.IO.Path.GetExtension(fileName ?? "").ToLowerInvariant();
            return ext == ".xlsx" || ext == ".xls" || ext == ".xlsm";
        }

        // Reads the first worksheet of an uploaded Excel file and returns a CSV stream
        // preserving the [Firstname | Email] layout ZeroBounce is configured to read.
        private static Stream ConvertExcelToCsv(IFormFile file)
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var sb = new System.Text.StringBuilder();
            using (var input = file.OpenReadStream())
            using (var package = new OfficeOpenXml.ExcelPackage(input))
            {
                var ws = package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : null;
                if (ws?.Dimension != null)
                {
                    int rowStart = ws.Dimension.Start.Row, rowEnd = ws.Dimension.End.Row;
                    int colStart = ws.Dimension.Start.Column, colEnd = ws.Dimension.End.Column;

                    for (int r = rowStart; r <= rowEnd; r++)
                    {
                        var cells = new List<string>();
                        for (int c = colStart; c <= colEnd; c++)
                            cells.Add(CsvEscape(ws.Cells[r, c].Text));
                        sb.Append(string.Join(",", cells)).Append("\r\n");
                    }
                }
            }

            var bytes = new System.Text.UTF8Encoding(false).GetBytes(sb.ToString());
            return new MemoryStream(bytes);
        }

        private static string CsvEscape(string value)
        {
            value = value ?? "";
            if (value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0)
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        // Downloads the finished ZeroBounce results and upserts each email's Valid/Invalid
        // outcome into the shared cache. Returns how many rows were saved. Column positions
        // are resolved from the header, so extra ZeroBounce columns don't matter.
        private static async Task<int> ImportBulkResultsToCache(string fileId, string apiKey,
            HttpClient client, ISqlConnections sqlConnections, int? userId)
        {
            var url = "https://bulkapi.zerobounce.net/v2/getfile?api_key=" +
                      Uri.EscapeDataString(apiKey) + "&file_id=" + Uri.EscapeDataString(fileId);

            var csv = await client.GetStringAsync(url);
            var rows = ParseCsv(csv);
            if (rows.Count < 2)
                return 0;

            var header = rows[0];
            int emailIdx = FindColumn(header, "email address", "email");
            int statusIdx = FindColumn(header, "zb status", "status");
            int subStatusIdx = FindColumn(header, "zb sub status", "zb substatus", "sub status");
            if (emailIdx < 0 || statusIdx < 0)
                return 0;

            using var connection = sqlConnections.NewFor<EmailVerificationResultRow>();
            var f = EmailVerificationResultRow.Fields;
            var now = DateTime.Now;
            var saved = 0;

            for (int i = 1; i < rows.Count; i++)
            {
                var cols = rows[i];
                if (emailIdx >= cols.Count)
                    continue;

                var email = (cols[emailIdx] ?? "").Trim();
                if (string.IsNullOrEmpty(email) || email.IndexOf('@') <= 0)
                    continue;

                var status = statusIdx < cols.Count ? (cols[statusIdx] ?? "").Trim() : "";
                if (string.IsNullOrEmpty(status))
                    status = "unknown";
                var subStatus = subStatusIdx >= 0 && subStatusIdx < cols.Count
                    ? (cols[subStatusIdx] ?? "").Trim() : "";

                var key = email.ToLowerInvariant();
                var message = BuildVerifyMessage(status, subStatus);

                var existing = connection.TryFirst<EmailVerificationResultRow>(q => q
                    .SelectTableFields()
                    .Where(new Criteria(f.Email) == key));

                if (existing != null)
                {
                    existing.Status = status;
                    existing.SubStatus = subStatus;
                    existing.Message = message;
                    existing.VerifiedByUserId = userId;
                    existing.VerifiedDate = now;
                    connection.UpdateById(existing);
                }
                else
                {
                    connection.InsertAndGetID(new EmailVerificationResultRow
                    {
                        Email = key,
                        Status = status,
                        SubStatus = subStatus,
                        Message = message,
                        VerifiedByUserId = userId,
                        VerifiedDate = now
                    });
                }

                saved++;
            }

            return saved;
        }

        // Case-insensitive header lookup; returns the first column whose name matches any candidate.
        private static int FindColumn(List<string> header, params string[] names)
        {
            for (int i = 0; i < header.Count; i++)
            {
                var h = (header[i] ?? "").Trim().ToLowerInvariant();
                foreach (var n in names)
                    if (h == n)
                        return i;
            }
            return -1;
        }

        // Minimal RFC-4180 CSV reader: handles quoted fields, escaped quotes ("") and
        // commas / newlines inside quotes. Enough for ZeroBounce result files.
        private static List<List<string>> ParseCsv(string text)
        {
            var rows = new List<List<string>>();
            if (string.IsNullOrEmpty(text))
                return rows;

            var row = new List<string>();
            var sb = new System.Text.StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                if (inQuotes)
                {
                    if (ch == '"')
                    {
                        if (i + 1 < text.Length && text[i + 1] == '"') { sb.Append('"'); i++; }
                        else inQuotes = false;
                    }
                    else sb.Append(ch);
                }
                else if (ch == '"') inQuotes = true;
                else if (ch == ',') { row.Add(sb.ToString()); sb.Clear(); }
                else if (ch == '\r') { /* handled together with \n */ }
                else if (ch == '\n')
                {
                    row.Add(sb.ToString()); sb.Clear();
                    rows.Add(row); row = new List<string>();
                }
                else sb.Append(ch);
            }

            if (sb.Length > 0 || row.Count > 0)
            {
                row.Add(sb.ToString());
                rows.Add(row);
            }

            return rows;
        }

        // Polls ZeroBounce for the progress of a previously uploaded bulk file.
        [HttpPost, IgnoreAntiforgeryToken]
        [Route("EmailVerification/BulkStatus")]
        public async Task<JsonResult> BulkStatus([FromForm] string fileId,
            [FromServices] IConfiguration configuration,
            [FromServices] IHttpClientFactory httpClientFactory,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IUserAccessor userAccessor,
            [FromServices] IPermissionService permissions)
        {
            if (!CanVerify(permissions))
                return new JsonResult(new BulkVerificationResult { Success = false, Message = "You do not have permission to run bulk verification." });

            if (string.IsNullOrWhiteSpace(fileId))
                return new JsonResult(new BulkVerificationResult { Success = false, Message = "Missing file id." });

            var apiKey = GetApiKey(sqlConnections, configuration);
            if (string.IsNullOrWhiteSpace(apiKey))
                return new JsonResult(new BulkVerificationResult { Success = false, Message = ApiKeyMissingMessage });

            try
            {
                var url = "https://bulkapi.zerobounce.net/v2/filestatus?api_key=" +
                          Uri.EscapeDataString(apiKey) + "&file_id=" + Uri.EscapeDataString(fileId);

                var client = httpClientFactory.CreateClient();
                using var apiResponse = await client.GetAsync(url);
                var body = await apiResponse.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(body);
                var root = doc.RootElement;

                var status = GetJsonString(root, "file_status");
                var percentage = GetJsonString(root, "complete_percentage");
                var error = GetJsonString(root, "error_reason");

                // Once the file is done, fold every verified email into the shared cache so the
                // as-you-type search shows "already verified" for these addresses, and charge the
                // uploader's quota for the records that were verified (best-effort: a failure here
                // must never break the status poll the page relies on).
                var imported = 0;
                if (!string.IsNullOrEmpty(status) &&
                    status.Equals("Complete", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var userId = GetCurrentUserId(userAccessor);
                        imported = await ImportBulkResultsToCache(fileId, apiKey, client,
                            sqlConnections, userId);
                        if (imported > 0)
                            ConsumeQuotaBy(sqlConnections, configuration, userId, imported);
                    }
                    catch { /* ignore — results can still be downloaded manually */ }
                }

                return new JsonResult(new BulkVerificationResult
                {
                    Success = true,
                    FileId = fileId,
                    Status = status,
                    Percentage = percentage,
                    Imported = imported,
                    Message = string.IsNullOrEmpty(error)
                        ? (imported > 0 ? imported + " email(s) verified, saved to search and counted in your quota." : null)
                        : error
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new BulkVerificationResult { Success = false, Message = "Status check failed: " + ex.Message });
            }
        }

        // Streams the finished ZeroBounce results CSV back to the browser as a download.
        [HttpGet]
        [Route("EmailVerification/BulkResult")]
        public async Task<IActionResult> BulkResult(string fileId,
            [FromServices] IConfiguration configuration,
            [FromServices] IHttpClientFactory httpClientFactory,
            [FromServices] ISqlConnections sqlConnections,
            [FromServices] IPermissionService permissions)
        {
            if (!CanVerify(permissions))
                return Content("You do not have permission to download bulk results.", "text/plain");

            if (string.IsNullOrWhiteSpace(fileId))
                return Content("Missing file id.", "text/plain");

            var apiKey = GetApiKey(sqlConnections, configuration);
            if (string.IsNullOrWhiteSpace(apiKey))
                return Content(ApiKeyMissingMessage, "text/plain");

            var url = "https://bulkapi.zerobounce.net/v2/getfile?api_key=" +
                      Uri.EscapeDataString(apiKey) + "&file_id=" + Uri.EscapeDataString(fileId);

            var client = httpClientFactory.CreateClient();
            var bytes = await client.GetByteArrayAsync(url);
            return File(bytes, "text/csv", "EmailVerification_Results_" + fileId + ".csv");
        }

        // A blank Excel template matching the bulk upload layout: [Firstname | Email] with a header row.
        [HttpGet]
        [Route("EmailVerification/DownloadBulkTemplate")]
        public IActionResult DownloadBulkTemplate([FromServices] IPermissionService permissions)
        {
            if (!CanVerify(permissions))
                return Content("You do not have permission to run bulk verification.", "text/plain");

            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new OfficeOpenXml.ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Emails");

            ws.Cells[1, 1].Value = "Firstname";
            ws.Cells[1, 2].Value = "Email";
            ws.Cells[1, 1].Style.Font.Bold = true;
            ws.Cells[1, 2].Style.Font.Bold = true;

            ws.Cells[2, 1].Value = "Jennifer";
            ws.Cells[2, 2].Value = "name@example.com";

            ws.Column(1).Width = 20;
            ws.Column(2).Width = 34;

            var bytes = package.GetAsByteArray();
            return File(bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "EmailVerification_Bulk_Template.xlsx");
        }
    }

    /// <summary>Shape returned by the bulk upload / status endpoints.</summary>
    public class BulkVerificationResult
    {
        public bool Success { get; set; }
        public string FileId { get; set; }
        public string Status { get; set; }
        public string Percentage { get; set; }
        public string Message { get; set; }
        /// <summary>Number of emails verified from the bulk file (and charged to the uploader's quota).</summary>
        public int Imported { get; set; }
    }

    /// <summary>Common shape returned by the verification endpoints.</summary>
    public class EmailVerificationResult
    {
        public bool Success { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        /// <summary>True when the result came from the shared cache (no quota was spent).</summary>
        public bool FromCache { get; set; }
        /// <summary>True when the request was refused because the user's quota is exhausted.</summary>
        public bool LimitReached { get; set; }
        /// <summary>When cached, when the email was last verified (display string).</summary>
        public string VerifiedDate { get; set; }
        public int Allowed { get; set; }
        public int Used { get; set; }
        public int Remaining { get; set; }
    }

    /// <summary>The signed-in user's quota, plus whether they can manage other users' quotas.</summary>
    public class QuotaStatusResult
    {
        public bool Success { get; set; }
        public int Allowed { get; set; }
        public int Used { get; set; }
        public int Remaining { get; set; }
        public bool CanManageQuota { get; set; }
        /// <summary>True when the user may run verifications; false = search/view only.</summary>
        public bool CanVerify { get; set; }
    }

    /// <summary>One row in the admin quota table.</summary>
    public class QuotaAdminItem
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public int AllowedCount { get; set; }
        public int UsedCount { get; set; }
    }

    public class QuotaListResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool CanManageQuota { get; set; }
        public List<QuotaAdminItem> Items { get; set; } = new List<QuotaAdminItem>();
    }

    /// <summary>Minimal ok/message shape for admin save actions.</summary>
    public class SimpleResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    /// <summary>Current Email Verification runtime settings for the admin "API Setup" form.</summary>
    public class SettingsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        /// <summary>The API key saved in the DB (null when only an appsettings key, or none, exists).</summary>
        public string ApiKey { get; set; }
        /// <summary>True when a usable key exists anywhere (DB or appsettings).</summary>
        public bool HasApiKey { get; set; }
        /// <summary>"database", "config" or "none" — where the active key comes from.</summary>
        public string Source { get; set; }
        public int DefaultQuota { get; set; }
        public bool CanManage { get; set; }
    }

    /// <summary>One contact row surfaced by the as-you-type search, from either source table.</summary>
    public class ContactSearchItem
    {
        public string Source { get; set; }
        public int? Id { get; set; }
        public string CampaignId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string WorkPhone { get; set; }
        public string Country { get; set; }
        /// <summary>Cached verification status for this row's email, if it was ever verified (else null).</summary>
        public string CachedStatus { get; set; }
        public string CachedMessage { get; set; }
        public string CachedVerifiedDate { get; set; }
    }

    public class ContactSearchResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ContactSearchItem> Items { get; set; } = new List<ContactSearchItem>();
        /// <summary>True when either source hit its row cap, so the grid can say so.</summary>
        public bool Truncated { get; set; }
        /// <summary>True when the typed term is a full email already present in the shared cache.</summary>
        public bool CacheHit { get; set; }
        public string CachedEmail { get; set; }
        public string CachedStatus { get; set; }
        public string CachedMessage { get; set; }
        public string CachedVerifiedDate { get; set; }
    }
}
