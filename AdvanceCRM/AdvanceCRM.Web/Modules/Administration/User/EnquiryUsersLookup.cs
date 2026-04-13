namespace AdvanceCRM.Administration.Scripts
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Web;
    using System;
    using AdvanceCRM.Web.Helpers;
    using AdvanceCRM.Administration.Entities;

    [LookupScript("Administration.EnquiryUsersLookup", Permission = "?")]
    public class EnquiryUsersLookup : RowLookupScript<UserRow>
    {
        public EnquiryUsersLookup(ISqlConnections sqlConnections)
            : base(sqlConnections)
        {
            IdField = UserRow.Fields.UserId.PropertyName;
            TextField = UserRow.Fields.DisplayName.PropertyName;
            Expiration = TimeSpan.FromDays(-1);
        }

        protected override void PrepareQuery(SqlQuery query)
        {
            base.PrepareQuery(query);

            // Filter only active users with "Enquiry" role
            query.Where(UserRow.Fields.IsActive == 1);

            // Filter only users whose role name contains "Enquiry" using EXISTS subquery
            query.Where(new Criteria("EXISTS (SELECT 1 FROM [dbo].[UserRoles] ur INNER JOIN [dbo].[Roles] r ON r.RoleId = ur.RoleId WHERE ur.UserId = T0.UserId AND r.RoleName LIKE '%Enquiry%')"));
        }

        public override string GetScript()
        {
            return LocalCache.GetLocalStoreOnly(
                "EnquiryUsersLookup:" + this.ScriptName,
                TimeSpan.FromHours(1),
                new UserRow().GetFields().GenerationKey,
                () => base.GetScript()
            );
        }
    }
}
