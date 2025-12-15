namespace AdvanceCRM.Administration.Scripts
{
    
    using AdvanceCRM.Web.Helpers;
    using Serenity;
    using Serenity.Abstractions;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Web;
    using System;

    [LookupScript("Administration.BranchLookup", Permission = "?")]
    public class BranchLookup : RowLookupScript<BranchRow>
    {
        private readonly IUserAccessor _userAccessor;

        public BranchLookup(ISqlConnections connections, IUserAccessor userAccessor)
            : base(connections)
        {
            _userAccessor = userAccessor;
            IdField = BranchRow.Fields.Id.PropertyName;
            TextField = BranchRow.Fields.Branch.PropertyName;
            Expiration = TimeSpan.FromDays(-1);
        }

        protected override void PrepareQuery(SqlQuery query)
        {
            base.PrepareQuery(query);

            var user = _userAccessor.User?.ToUserDefinition();

            if (user != null)
                query.Where(BranchRow.Fields.CompanyId == user.CompanyId);
        }

        public override string GetScript()
        {
            var user = _userAccessor.User?.ToUserDefinition();

            if (user == null)
                return string.Empty;

            return LocalCache.GetLocalStoreOnly(
                "BranchLookup:" + this.ScriptName + ":" + user.CompanyId,
                TimeSpan.FromHours(1),
                new BranchRow().GetFields().GenerationKey,
                () => base.GetScript());
        }
    }
}
