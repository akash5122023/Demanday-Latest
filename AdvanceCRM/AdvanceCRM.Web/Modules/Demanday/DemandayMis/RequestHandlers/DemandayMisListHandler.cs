using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayMisRow>;
using MyRow = AdvanceCRM.Demanday.DemandayMisRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayMisListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMisListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMisListHandler
    {
        public DemandayMisListHandler(IRequestContext context)
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