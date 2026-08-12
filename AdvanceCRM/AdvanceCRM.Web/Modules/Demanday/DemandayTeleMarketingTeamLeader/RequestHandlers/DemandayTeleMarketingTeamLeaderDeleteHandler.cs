using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingTeamLeaderDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingTeamLeaderDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingTeamLeaderDeleteHandler
    {
        public DemandayTeleMarketingTeamLeaderDeleteHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void OnBeforeDelete()
        {
            base.OnBeforeDelete();

            // Same reason as the Move to Quality path: a toolkit copy still referencing this
            // record would otherwise block the delete on FK_ToolkitTMEnquiry_TeamLeaderId.
            if (Row.Id.HasValue)
                TMEnquirySyncHandler.UnlinkTeamLeader(Connection, Row.Id.Value);
        }
    }
}