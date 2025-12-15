using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdvanceCRM.Administration
{
    public class SubdomainService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SubdomainService> _logger;
        private readonly string _apiToken;
        private readonly string _apiKey;
        private readonly string _apiEmail;
        private readonly string _zoneId;
        private readonly string _serverIp;
        private readonly string _rootDomain;

        public SubdomainService(HttpClient httpClient, IConfiguration configuration, ILogger<SubdomainService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger;
            var section = configuration.GetSection("Cloudflare");
            _apiToken = section["ApiToken"];
            if (string.IsNullOrEmpty(_apiToken))
                _apiToken = Environment.GetEnvironmentVariable("CLOUDFLARE_API_TOKEN");

            _apiKey = section["ApiKey"];
            if (string.IsNullOrEmpty(_apiKey))
                _apiKey = Environment.GetEnvironmentVariable("CLOUDFLARE_API_KEY");

            _apiEmail = section["Email"];
            if (string.IsNullOrEmpty(_apiEmail))
                _apiEmail = Environment.GetEnvironmentVariable("CLOUDFLARE_EMAIL");

            _zoneId = section["ZoneId"];
            if (string.IsNullOrEmpty(_zoneId))
                _zoneId = Environment.GetEnvironmentVariable("CLOUDFLARE_ZONE_ID");

            _serverIp = section["ServerIp"];
            if (string.IsNullOrEmpty(_serverIp))
                _serverIp = Environment.GetEnvironmentVariable("CLOUDFLARE_SERVER_IP");

            _rootDomain = section["RootDomain"];
            if (string.IsNullOrEmpty(_rootDomain))
                _rootDomain = Environment.GetEnvironmentVariable("CLOUDFLARE_ROOT_DOMAIN");
        }

        public async Task<string> CreateSubdomainAsync(string subdomain)
        {
            if (string.IsNullOrWhiteSpace(subdomain))
                throw new ArgumentNullException(nameof(subdomain));

            var hasToken = !string.IsNullOrEmpty(_apiToken);
            var hasKey = !string.IsNullOrEmpty(_apiKey) && !string.IsNullOrEmpty(_apiEmail);
            if ((!hasToken && !hasKey) || string.IsNullOrEmpty(_zoneId) || string.IsNullOrEmpty(_serverIp) || string.IsNullOrEmpty(_rootDomain))
            {
                _logger.LogWarning("Cloudflare credentials, zone ID, server IP, or root domain not configured.");
                return "Skipped: Cloudflare credentials, zone ID, server IP, or root domain not configured.";
            }

            var baseUrl = $"https://api.cloudflare.com/client/v4/zones/{_zoneId}/dns_records";
            var fullName = $"{subdomain}.{_rootDomain}";
            var encodedName = Uri.EscapeDataString(fullName);

            // check if record already exists

            try
            {
                var (checkResponse, checkUsedToken, checkUsedApiKey, checkFallbackAttempted) =
                    await SendCloudflareAsync(HttpMethod.Get, $"{baseUrl}?name={encodedName}&type=A");

                var checkContent = await checkResponse.Content.ReadAsStringAsync();
                if (checkResponse.IsSuccessStatusCode)
                {
                    try
                    {
                        using var checkDoc = JsonDocument.Parse(checkContent);
                        if (checkDoc.RootElement.GetProperty("result").EnumerateArray().Any())
                        {
                            _logger.LogInformation("Subdomain {Subdomain} already exists in Cloudflare.", fullName);
                            return $"Exists: DNS record for {fullName} already present.";
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error parsing Cloudflare lookup response for {Subdomain}: {Content}", fullName, checkContent);
                    }
                }
                else
                {
                    var hint = BuildAuthenticationHint(checkResponse.StatusCode, checkUsedToken, checkUsedApiKey, checkFallbackAttempted);
                    if (!string.IsNullOrEmpty(hint))
                    {
                        _logger.LogWarning(
                            "Failed to check existing record for {Subdomain}. Status: {Status}. Content: {Content}. {Hint}",
                            fullName,
                            checkResponse.StatusCode,
                            checkContent,
                            hint);
                    }
                    else
                    {
                        _logger.LogWarning(
                            "Failed to check existing record for {Subdomain}. Status: {Status}. Content: {Content}",
                            fullName,
                            checkResponse.StatusCode,
                            checkContent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking existence of subdomain {Subdomain}", fullName);
            }

            // Cloudflare API requires TTL when proxied is specified; using automatic (1) here
            var payload = new { type = "A", name = fullName, content = _serverIp, proxied = true, ttl = 1 };
            var json = JsonSerializer.Serialize(payload);


            try
            {
                var (response, usedToken, usedApiKey, fallbackAttempted) = await SendCloudflareAsync(HttpMethod.Post, baseUrl, json);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {

                    var diagnosticHint = BuildAuthenticationHint(response.StatusCode, usedToken, usedApiKey, fallbackAttempted);

                    if (!string.IsNullOrEmpty(diagnosticHint))
                    {
                        _logger.LogError(
                            "Failed to create subdomain {Subdomain}. Status: {Status}. Content: {Content}. {Hint}",
                            subdomain,
                            response.StatusCode,
                            content,
                            diagnosticHint);
                    }
                    else
                    {
                        _logger.LogError(
                            "Failed to create subdomain {Subdomain}. Status: {Status}. Content: {Content}",
                            subdomain,
                            response.StatusCode,
                            content);
                    }

                    throw new CloudflareProvisioningException(
                        subdomain,
                        response.StatusCode,
                        content,

                        $"Cloudflare API error {(int)response.StatusCode}: {content}",
                        diagnosticHint);

                }

                try
                {
                    var doc = JsonDocument.Parse(content);
                    if (!doc.RootElement.GetProperty("success").GetBoolean())
                    {
                        _logger.LogError("Cloudflare returned success=false while creating {Subdomain}. Response: {Content}", subdomain, content);
                        throw new CloudflareProvisioningException(
                            subdomain,
                            response.StatusCode,
                            content,
                            $"Cloudflare API returned success=false: {content}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error parsing Cloudflare response for {Subdomain}: {Content}", subdomain, content);
                    throw;
                }

                return $"Created DNS record {fullName}.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subdomain {Subdomain}", subdomain);
                if (ex is CloudflareProvisioningException)
                    throw;

                throw new CloudflareProvisioningException(subdomain, null, ex.Message, ex.Message, innerException: ex);

            }
        }

        private async Task<(HttpResponseMessage Response, bool UsedToken, bool UsedApiKey, bool FallbackAttempted)> SendCloudflareAsync(HttpMethod method, string url, string json = null)
        {
            HttpRequestMessage CreateMessage(bool useToken)
            {
                var msg = new HttpRequestMessage(method, url);
                if (json != null)
                    msg.Content = new StringContent(json, Encoding.UTF8, "application/json");
                ApplyAuth(msg, useToken);
                return msg;
            }


            var hasToken = !string.IsNullOrEmpty(_apiToken);
            var hasKey = !string.IsNullOrEmpty(_apiKey) && !string.IsNullOrEmpty(_apiEmail);

            var response = await _httpClient.SendAsync(CreateMessage(hasToken));
            var usedToken = hasToken;
            var usedApiKey = !hasToken && hasKey;
            var fallbackAttempted = false;
            if (response.StatusCode == HttpStatusCode.Unauthorized && hasToken)
            {
                _logger.LogWarning(

                    "Cloudflare token request returned 401 for ZoneId {ZoneId}. Token may be missing zone DNS permissions or ZoneId may be mismatched. Configure ApiKey/Email if available.",
                    _zoneId);


                if (hasKey)
                {
                    fallbackAttempted = true;
                    response.Dispose();
                    response = await _httpClient.SendAsync(CreateMessage(false));
                    usedToken = false;
                    usedApiKey = true;
                }
            }

            return (response, usedToken, usedApiKey, fallbackAttempted);
        }

        private void ApplyAuth(HttpRequestMessage message, bool useToken)
        {
            if (useToken && !string.IsNullOrEmpty(_apiToken))

            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
            }
            else if (!string.IsNullOrEmpty(_apiKey) && !string.IsNullOrEmpty(_apiEmail))
            {
                message.Headers.Add("X-Auth-Key", _apiKey);
                message.Headers.Add("X-Auth-Email", _apiEmail);
            }
        }

        private string BuildAuthenticationHint(HttpStatusCode statusCode, bool usedToken, bool usedApiKey, bool fallbackAttempted)
        {
            if (statusCode != HttpStatusCode.Unauthorized)
                return null;

            var zoneId = string.IsNullOrWhiteSpace(_zoneId) ? "<unset>" : _zoneId.Trim();
            var rootDomain = string.IsNullOrWhiteSpace(_rootDomain) ? "<unset>" : _rootDomain.Trim();

            var builder = new StringBuilder();
            if (usedToken)
            {
                builder.Append("The request used the Cloudflare API token (Cloudflare:ApiToken / CLOUDFLARE_API_TOKEN)")
                    .Append(" for zone '").Append(zoneId).Append("' (root domain '").Append(rootDomain).Append("'). ")
                    .Append("Ensure the token is associated with the correct account and includes Zone.Zone:Read and Zone.DNS:Edit permissions for this zone.");

                if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_apiEmail))
                {
                    builder.Append(" You can also configure Cloudflare:ApiKey and Cloudflare:Email (or CLOUDFLARE_API_KEY / CLOUDFLARE_EMAIL) to enable the legacy fallback credentials.");
                }
            }
            else if (usedApiKey)
            {
                builder.Append("The request used the legacy Cloudflare API key (Cloudflare:ApiKey / CLOUDFLARE_API_KEY)")
                    .Append(" and email (Cloudflare:Email / CLOUDFLARE_EMAIL). Confirm that both values are correct, unrestricted, and belong to an account with DNS edit permissions for zone '")
                    .Append(zoneId).Append("'.");

                if (fallbackAttempted)
                {
                    builder.Append(" The service first attempted the API token and then retried with the API key, but both credentials were rejected.");
                }
            }
            else
            {
                builder.Append("No Cloudflare authentication headers were attached to the request. Verify that API credentials are configured correctly.");
            }

            builder.Append(" Review the Cloudflare:ZoneId and Cloudflare:RootDomain settings (currently '")
                .Append(zoneId).Append("' and '").Append(rootDomain).Append("') before retrying.");

            return builder.ToString();
        }
    }
}
