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

            // Get current logged-in user's ID using the ClaimsPrincipal extension
            var userIdStr = Context.User.GetIdentifier();

            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
            {
                // Check if user has a role containing "Enquiry"
                if (HasEnquiryRole(userId))
                {
                    return; // Users with Enquiry role can see all campaigns
                }

                // Filter records where AgentsName matches the logged-in user's UserId
                // This ensures users only see records assigned to them
                query.Where(MyRow.Fields.AgentsName == userId);
            }
        }

        private bool HasEnquiryRole(int userId)
        {
            // Get all role IDs for the current user
            var userRoleIds = Connection.Query<int>(
                "SELECT RoleId FROM UserRoles WHERE UserId = @userId",
                new { userId }).ToList();

            if (userRoleIds.Count == 0)
                return false;

            // Check if any of these roles contain "Enquiry" in their name
            var hasEnquiryRole = Connection.Exists<RoleRow>(
                new Criteria(RoleRow.Fields.RoleId).In(userRoleIds) &
                new Criteria(RoleRow.Fields.RoleName).Contains("Enquiry"));

            return hasEnquiryRole;
        }
    }
}