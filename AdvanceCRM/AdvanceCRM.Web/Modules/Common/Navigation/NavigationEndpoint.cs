
namespace AdvanceCRM.Common.Endpoints
{
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;

    using System.Collections.Generic;
    using System.Linq;
    using System.Configuration;
    using Serenity.Web.Providers;
    using System.Text.RegularExpressions;
    using System.IO;
    using System;
    using AdvanceCRM.MultiTenancy;

    [Route("Services/Common/Navigation/[action]")]
    public class NavigationController : ServiceEndpoint
    {
        private readonly ITenantAccessor tenantAccessor;

        public NavigationController(ITenantAccessor tenantAccessor)
        {
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
        }

        [HttpPost]
        public StandardResponse MultiCompany()
        {
            return BuildModuleResponse("MultiCompany");
        }

        [HttpPost]
        public StandardResponse MultiLocation()
        {
            return BuildModuleResponse("MultiLocation");
        }

        [HttpPost]
        public StandardResponse ChannelsManagement()
        {
            return BuildModuleResponse("ChannelsManagement", "ChannelManagement");
        }

        private StandardResponse BuildModuleResponse(params string[] modules)
        {
            var activeModules = GetActiveModules();
            var hasModule = modules != null && modules.Any(module => activeModules.Contains(module));

            return new StandardResponse
            {
                Status = hasModule ? "No" : "Remove"
            };
        }

        private ISet<string> GetActiveModules()
        {
            var tenant = tenantAccessor?.CurrentTenant;
            if (tenant != null && tenant.ShouldFilterModules())
                return tenant.Modules;

            var configured = ConfigurationManager.AppSettings["Modules"];
            return TenantInfo.ParseModules(configured);
        }
    }
}
