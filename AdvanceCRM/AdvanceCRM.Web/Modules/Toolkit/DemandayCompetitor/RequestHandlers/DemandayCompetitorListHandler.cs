using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Toolkit.DemandayCompetitorRow>;
using MyRow = AdvanceCRM.Toolkit.DemandayCompetitorRow;

namespace AdvanceCRM.Toolkit
{
    public interface IDemandayCompetitorListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCompetitorListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCompetitorListHandler
    {
        public DemandayCompetitorListHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ValidatePermissions()
        {
            if (Permissions.HasPermission("DemandayCompetitor:Read") ||
                Permissions.HasPermission("Toolkit:VerifySheets") ||
                Permissions.HasPermission("Toolkit:VerifySheets:Competitor") ||
                Permissions.HasPermission("Toolkit:VerifySheets:DemandayCompetitor"))
            {
                return;
            }

            base.ValidatePermissions();
        }
    }
}