using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeamLeaderRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeamLeaderListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeamLeaderListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeamLeaderListHandler
    {
        public DemandayTeamLeaderListHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ApplyFilters(SqlQuery query)
        {
            base.ApplyFilters(query);

            if (Context.Permissions.HasPermission(AdvanceCRM.Administration.PermissionKeys.Security))
            {
                return;
            }

            var userIdStr = Context.User.GetIdentifier();
            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
            {
                query.Where(MyRow.Fields.OwnerId == userId);
            }
        }
    }
}