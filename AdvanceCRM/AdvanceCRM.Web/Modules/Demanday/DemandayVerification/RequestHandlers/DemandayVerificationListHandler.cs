using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayVerificationRow>;
using MyRow = AdvanceCRM.Demanday.DemandayVerificationRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayVerificationListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayVerificationListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayVerificationListHandler
    {
        public DemandayVerificationListHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ApplyFilters(SqlQuery query)
        {
            base.ApplyFilters(query);

            // Admins with Administration:Security can see every record.
            if (Context.Permissions.HasPermission(AdvanceCRM.Administration.PermissionKeys.Security))
                return;

            // Everyone else sees only:
            //   (a) rows where AgentName matches their own DisplayName or Username, OR
            //   (b) rows they created themselves (OwnerId).
            var userIdStr = Context.User.GetIdentifier();
            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
            {
                // Resolve the current user's names directly from the DB connection
                // that the base ListRequestHandler already holds open for this request.
                string displayName = null;
                string username = null;
                var userRow = Connection.TryById<AdvanceCRM.Administration.UserRow>(userId);
                displayName = userRow?.DisplayName?.Trim();
                username    = userRow?.Username?.Trim();

                var f = MyRow.Fields;

                // Build: (AgentName IS NULL OR AgentName = '' OR AgentName = displayName
                //         OR AgentName = username) OR OwnerId = userId
                BaseCriteria agentFilter = f.AgentName.IsNull() | new Criteria(f.AgentName) == "";
                if (!string.IsNullOrWhiteSpace(displayName))
                    agentFilter = agentFilter | (new Criteria(f.AgentName) == displayName);
                if (!string.IsNullOrWhiteSpace(username) && username != displayName)
                    agentFilter = agentFilter | (new Criteria(f.AgentName) == username);

                query.Where(agentFilter | (f.OwnerId == userId));
            }
            else
            {
                // No identifiable user — show nothing rather than leaking everyone's data.
                query.Where(MyRow.Fields.Id == -1);
            }
        }
    }
}