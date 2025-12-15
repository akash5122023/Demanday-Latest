using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayTeamLeaderRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeamLeaderRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeamLeaderRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeamLeaderRetrieveHandler
    {
        public DemandayTeamLeaderRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}