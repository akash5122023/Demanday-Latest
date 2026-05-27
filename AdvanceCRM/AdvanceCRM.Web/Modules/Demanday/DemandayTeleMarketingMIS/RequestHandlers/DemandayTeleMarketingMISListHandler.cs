using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeleMarketingMISRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingMISRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingMISListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingMISListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingMISListHandler
    {
        public DemandayTeleMarketingMISListHandler(IRequestContext context)
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