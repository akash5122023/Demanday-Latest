namespace AdvanceCRM.Administration.Scripts
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Web;
    using System;
    using AdvanceCRM.Web.Helpers;

    [LookupScript("Administration.UserLookup", Permission = "?")]
    public class UserLookup : RowLookupScript<UserRow>
    {
        public UserLookup(ISqlConnections sqlConnections)
            : base(sqlConnections)
        {
            IdField = UserRow.Fields.UserId.PropertyName;
            TextField = UserRow.Fields.DisplayName.PropertyName;
            Expiration = TimeSpan.FromDays(-1);
        }

        protected override void PrepareQuery(SqlQuery query)
        {
            base.PrepareQuery(query);

            var user = Authorization.UserDefinition as UserDefinition;

            if (user != null)
                query.Where(UserRow.Fields.CompanyId == user.CompanyId &&
                            UserRow.Fields.IsActive == 1);
        }

        public override string GetScript()
        {
            var user = Authorization.UserDefinition as UserDefinition;

            if (user == null)
                return string.Empty;

            return LocalCache.GetLocalStoreOnly(
                "UserLookup:" + this.ScriptName + ":" + user.CompanyId,
                TimeSpan.FromHours(1),
                new UserRow().GetFields().GenerationKey,
                () => base.GetScript()
            );
        }
    }
}
