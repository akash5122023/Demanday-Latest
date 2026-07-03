using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using System.Linq;
using AdvanceCRM.Administration.Entities;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Toolkit.TalCampaignRow>;
using MyRow = AdvanceCRM.Toolkit.TalCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface ITalCampaignListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class TalCampaignListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, ITalCampaignListHandler
    {
        public TalCampaignListHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ApplyFilters(SqlQuery query)
        {
            base.ApplyFilters(query);

            // Admin users with Administration:Security permission can see all campaigns
            if (Context.Permissions.HasPermission(AdvanceCRM.Administration.PermissionKeys.Security))
            {
                return;
            }

            // Team Leaders upload and assign the data, so they (anyone who can insert) see all rows.
            if (Context.Permissions.HasPermission("TalCampaign:Insert"))
            {
                return;
            }

            // Everyone else (Enquiry agents) sees only the rows assigned to them via the "User Name" column.
            var userIdStr = Context.User.GetIdentifier();

            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
            {
                query.Where(MyRow.Fields.AgentsName == userId);
            }
            else
            {
                // No identifiable user: show nothing rather than leaking everyone's data.
                query.Where(MyRow.Fields.AgentsName == -1);
            }
        }
    }
}