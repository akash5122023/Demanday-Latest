using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayTeleMarketingTeamLeaderRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingTeamLeaderRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingTeamLeaderRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingTeamLeaderRetrieveHandler
    {
        public DemandayTeleMarketingTeamLeaderRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}