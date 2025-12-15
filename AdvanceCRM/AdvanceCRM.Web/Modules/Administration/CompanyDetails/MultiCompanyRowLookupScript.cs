using Serenity;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Serenity.Abstractions;
using Serenity.Data;
using Serenity.Web;
using System;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Scripts
{
    public class MultiCompanyRowLookupScript<TRow> :
        RowLookupScript<TRow>
        where TRow : class, IRow, IMultiCompanyRow, new()
    {
        private readonly IUserAccessor _userAccessor;
        private readonly ITenantAccessor _tenantAccessor;

        public MultiCompanyRowLookupScript(ISqlConnections connections, IUserAccessor userAccessor, ITenantAccessor tenantAccessor)
            : base(connections)
        {
            _userAccessor = userAccessor;
            _tenantAccessor = tenantAccessor;
            Expiration = TimeSpan.FromDays(-1);
        }

        public MultiCompanyRowLookupScript()
            : this(
                Dependency.Resolve<ISqlConnections>(),
                Dependency.Resolve<IUserAccessor>(),
                Dependency.Resolve<ITenantAccessor>())
        {
        }

        protected override void PrepareQuery(SqlQuery query)
        {
            base.PrepareQuery(query);
            AddCompanyFilter(query);
        }

        protected void AddCompanyFilter(SqlQuery query)
        {
            var r = new TRow();
            var user = _userAccessor.User?.ToUserDefinition();

            if (user != null)
                query.Where(r.CompanyIdField == user.CompanyId);
        }

        public override string GetScript()
        {
            var user = _userAccessor.User?.ToUserDefinition();

            if (user == null)
                return string.Empty;

            var tenantId = _tenantAccessor?.CurrentTenant?.TenantId ?? 0;
            var cacheKey = $"MultiCompanyLookup:{this.ScriptName}:{tenantId}:{user.CompanyId}";

            return LocalCache.GetLocalStoreOnly(
                cacheKey,
                TimeSpan.FromHours(1),
                new TRow().GetFields().GenerationKey,
                () => base.GetScript());
        }
    }
}
