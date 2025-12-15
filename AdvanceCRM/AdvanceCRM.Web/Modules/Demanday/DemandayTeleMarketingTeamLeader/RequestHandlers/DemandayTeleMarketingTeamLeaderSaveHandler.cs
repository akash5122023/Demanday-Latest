using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeleMarketingTeamLeaderRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingTeamLeaderSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingTeamLeaderSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingTeamLeaderSaveHandler
    {
        public DemandayTeleMarketingTeamLeaderSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}