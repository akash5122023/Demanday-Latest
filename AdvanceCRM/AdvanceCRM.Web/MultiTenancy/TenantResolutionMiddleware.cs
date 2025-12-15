using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AdvanceCRM.MultiTenancy
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ITenantResolver resolver;
        private readonly ITenantAccessor tenantAccessor;
        private readonly MultiTenancyOptions options;
        private readonly ILogger<TenantResolutionMiddleware> logger;

        public TenantResolutionMiddleware(RequestDelegate next, ITenantResolver resolver, ITenantAccessor tenantAccessor, IOptions<MultiTenancyOptions> options, ILogger<TenantResolutionMiddleware> logger)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.options = options?.Value ?? new MultiTenancyOptions();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var originalTenant = tenantAccessor.CurrentTenant;
            try
            {
                tenantAccessor.CurrentTenant = null;

                var host = context.Request.Host.Host;
                if (!string.IsNullOrEmpty(host) && options.MarketingHosts != null && options.MarketingHosts.Length > 0)
                {
                    foreach (var marketingHost in options.MarketingHosts)
                    {
                        if (!string.IsNullOrWhiteSpace(marketingHost) && string.Equals(host, marketingHost.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            await next(context);
                            return;
                        }
                    }
                }
                var subdomain = ExtractSubdomain(host);
                if (string.IsNullOrEmpty(subdomain) && options.EnforceTenant)
                {
                    logger.LogWarning("Rejecting request with no tenant for host {Host}", host);
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("Tenant required");
                    return;
                }

                if (!string.IsNullOrEmpty(subdomain))
                {
                    var tenant = await resolver.ResolveAsync(subdomain, context.RequestAborted);
                    if (tenant != null)
                    {
                        tenantAccessor.CurrentTenant = tenant;
                    }
                    else if (options.EnforceTenant)
                    {
                        logger.LogWarning("Rejecting request for unresolved tenant subdomain {Subdomain}", subdomain);
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsync("Tenant not found");
                        return;
                    }
                }

                await next(context);
            }
            finally
            {
                tenantAccessor.CurrentTenant = originalTenant;
            }
        }

        private string? ExtractSubdomain(string? host)
        {
            if (string.IsNullOrWhiteSpace(host))
                return null;

            var sanitizedHost = host.Trim();
            var separatorIndex = sanitizedHost.IndexOf(':');
            if (separatorIndex > -1)
                sanitizedHost = sanitizedHost[..separatorIndex];

            if (string.IsNullOrEmpty(sanitizedHost))
                return null;

            if (IPAddress.TryParse(sanitizedHost, out _))
                return null;

            if (!string.IsNullOrEmpty(options.RootDomain))
            {
                var rootDomain = options.RootDomain.Trim().TrimStart('.');
                if (!sanitizedHost.EndsWith(rootDomain, StringComparison.OrdinalIgnoreCase))
                    return null;

                if (sanitizedHost.Equals(rootDomain, StringComparison.OrdinalIgnoreCase))
                    return null;

                var prefix = sanitizedHost[..(sanitizedHost.Length - rootDomain.Length)].TrimEnd('.');
                if (string.IsNullOrEmpty(prefix))
                    return null;

                if (string.Equals(prefix, "www", StringComparison.OrdinalIgnoreCase))
                    return null;

                return prefix.ToLowerInvariant();
            }

            var parts = sanitizedHost.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
                return null;

            if (string.Equals(parts[0], "www", StringComparison.OrdinalIgnoreCase))
                return null;

            return parts[0].ToLowerInvariant();
        }
    }
}

