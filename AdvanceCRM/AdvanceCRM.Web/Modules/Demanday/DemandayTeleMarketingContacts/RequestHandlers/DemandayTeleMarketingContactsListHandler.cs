using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingContactsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingContactsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingContactsListHandler
    {
        public DemandayTeleMarketingContactsListHandler(IRequestContext context)
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