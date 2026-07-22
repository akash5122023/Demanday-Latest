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

            // Admin users with Administration:Security permission can see all campaigns.
            if (Context.Permissions.HasPermission(AdvanceCRM.Administration.PermissionKeys.Security))
            {
                return;
            }

            // NOTE: "TalCampaign:Insert" is deliberately NOT an exemption. Being able to add or
            // import rows does not mean you may read everyone else's — a user who imports the sheet
            // still only sees the rows where they are the Agent.

            // Everyone else sees only the rows assigned to them via the Agent column.
            // If the agent column is empty, show the data to all users.
            var userIdStr = Context.User.GetIdentifier();

            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
            {
                query.Where(
                    (MyRow.Fields.AgentsName == userId) |
                    (MyRow.Fields.AgentsName.IsNull())
                );
            }
            else
            {
                // No identifiable user: show nothing rather than leaking everyone's data.
                query.Where(MyRow.Fields.AgentsName == -1);
            }
        }
    }
}