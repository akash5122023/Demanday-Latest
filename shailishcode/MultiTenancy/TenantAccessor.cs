using System.Threading;
using Microsoft.AspNetCore.Http;

namespace AdvanceCRM.MultiTenancy
{
    public interface ITenantAccessor
    {
        TenantInfo? CurrentTenant { get; set; }
    }

    public class TenantAccessor : ITenantAccessor
    {
        private const string TenantContextKey = "__CurrentTenant";
        private static readonly AsyncLocal<TenantInfo?> AsyncLocalTenant = new();
        private readonly IHttpContextAccessor httpContextAccessor;

        public TenantAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public TenantInfo? CurrentTenant
        {
            get
            {
                var httpContext = httpContextAccessor.HttpContext;
                if (httpContext != null && httpContext.Items.TryGetValue(TenantContextKey, out var value))
                    return value as TenantInfo;

                return AsyncLocalTenant.Value;
            }
            set
            {
                var httpContext = httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    if (value == null)
                        httpContext.Items.Remove(TenantContextKey);
                    else
                        httpContext.Items[TenantContextKey] = value;
                }

                AsyncLocalTenant.Value = value;
            }
        }
    }
}
