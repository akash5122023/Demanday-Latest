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
    }
}