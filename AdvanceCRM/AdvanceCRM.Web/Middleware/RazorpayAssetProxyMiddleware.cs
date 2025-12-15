using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdvanceCRM.Web.Middleware
{
    public class RazorpayAssetProxyMiddleware
    {
        private static readonly string[] SupportedPaths = new[] { "/public", "/v1/public" };

        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RazorpayAssetProxyMiddleware> _logger;

        public RazorpayAssetProxyMiddleware(
            RequestDelegate next,
            IHttpClientFactory httpClientFactory,
            ILogger<RazorpayAssetProxyMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!ShouldProxy(context.Request))
            {
                await _next(context);
                return;
            }

            var client = _httpClientFactory.CreateClient("RazorpayCheckoutAssets");
            var downstreamPath = BuildDownstreamPath(context.Request);

            try
            {
                using var response = await client.GetAsync(downstreamPath, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted)
                    .ConfigureAwait(false);

                context.Response.StatusCode = (int)response.StatusCode;
                CopyHeaders(response, context.Response);

                await response.Content.CopyToAsync(context.Response.Body).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // request aborted by client - do not treat as error
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Razorpay][Proxy] Failed to proxy Razorpay asset {Path}{Query}.", context.Request.Path, context.Request.QueryString);
                if (!context.Response.HasStarted)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = StatusCodes.Status502BadGateway;
                    context.Response.Headers["Content-Type"] = "application/json";
                    await context.Response.WriteAsync("{\"error\":\"RazorpayAssetProxyFailure\"}");
                }
            }
        }

        private static bool ShouldProxy(HttpRequest request)
        {
            if (!HttpMethods.IsGet(request.Method))
                return false;

            //if (!SupportedPaths.Contains(request.Path, StringComparer.OrdinalIgnoreCase))
            var path = request.Path.Value; // PathString -> string
            if (string.IsNullOrEmpty(path))
                return false;

            // Allow exact match or prefix (e.g., /public/checkout.js)
            var match = SupportedPaths.Any(p =>
                path.Equals(p, StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith(p + "/", StringComparison.OrdinalIgnoreCase));

            if (!match)
                return false;

            return request.Query.ContainsKey("traffic_env");
        }

        private static string BuildDownstreamPath(HttpRequest request)
        {
            var raw = request.Path.Value?.TrimStart('/') ?? string.Empty;
            if (string.IsNullOrWhiteSpace(raw))
                return "v1/checkout.js" + request.QueryString; // safe fallback

            // Split and normalize segments
            var segments = raw.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();

            // Remove leading v1 (we'll prepend once later)
            if (segments.Count > 0 && string.Equals(segments[0], "v1", StringComparison.OrdinalIgnoreCase))
                segments.RemoveAt(0);

            // Remove leading 'public' when present (Razorpay's CDN does not include this segment)
            if (segments.Count > 0 && string.Equals(segments[0], "public", StringComparison.OrdinalIgnoreCase))
                segments.RemoveAt(0);

            // If nothing left (e.g. request was just /public or /v1/public) default to checkout.js
            if (segments.Count == 0)
                segments.Add("checkout.js");

            var downstream = "v1/" + string.Join('/', segments);
            return downstream + request.QueryString;
        }

        private static void CopyHeaders(HttpResponseMessage source, HttpResponse target)
        {
            foreach (var header in source.Headers)
            {
                target.Headers[header.Key] = header.Value.ToArray();
            }

            foreach (var header in source.Content.Headers)
            {
                target.Headers[header.Key] = header.Value.ToArray();
            }

            target.Headers.Remove("transfer-encoding");
            target.Headers.Remove("Connection");
            target.Headers.Remove("Keep-Alive");

            // Add permissive CORS/expose headers to minimize console warnings for header access attempts
            // Only for these proxied static assets
            if (!target.Headers.ContainsKey("Access-Control-Allow-Origin"))
                target.Headers["Access-Control-Allow-Origin"] = "*";
            // Expose specific headers that Razorpay scripts might try to read
            var exposeList = "x-rtb-fingerprint-id, etag, last-modified";
            if (!target.Headers.ContainsKey("Access-Control-Expose-Headers"))
                target.Headers["Access-Control-Expose-Headers"] = exposeList;
            else
            {
                var existing = string.Join(",", target.Headers["Access-Control-Expose-Headers"].ToArray());
                if (!existing.Contains("x-rtb-fingerprint-id", StringComparison.OrdinalIgnoreCase))
                    target.Headers["Access-Control-Expose-Headers"] = existing + ", x-rtb-fingerprint-id";
            }
        }
    }
}
