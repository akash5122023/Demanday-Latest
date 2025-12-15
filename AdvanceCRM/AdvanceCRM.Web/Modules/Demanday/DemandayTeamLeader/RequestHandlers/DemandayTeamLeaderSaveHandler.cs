using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeamLeaderRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeamLeaderSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeamLeaderSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeamLeaderSaveHandler
    {
        public DemandayTeamLeaderSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}