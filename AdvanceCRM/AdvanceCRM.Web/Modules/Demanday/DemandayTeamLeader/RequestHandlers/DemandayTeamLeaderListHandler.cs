using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeamLeaderRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeamLeaderListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeamLeaderListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeamLeaderListHandler
    {
        public DemandayTeamLeaderListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}