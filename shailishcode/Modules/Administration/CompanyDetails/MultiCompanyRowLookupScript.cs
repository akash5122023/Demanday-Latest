using Serenity;
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

        public MultiCompanyRowLookupScript(ISqlConnections connections, IUserAccessor userAccessor)
            : base(connections)
        {
            _userAccessor = userAccessor;
            Expiration = TimeSpan.FromDays(-1);
        }

        public MultiCompanyRowLookupScript()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IUserAccessor>())
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

            return LocalCache.GetLocalStoreOnly(
                "MultiCompanyLookup:" + this.ScriptName + ":" + user.CompanyId,
                TimeSpan.FromHours(1),
                new TRow().GetFields().GenerationKey,
                () => base.GetScript());
        }
    }
}
