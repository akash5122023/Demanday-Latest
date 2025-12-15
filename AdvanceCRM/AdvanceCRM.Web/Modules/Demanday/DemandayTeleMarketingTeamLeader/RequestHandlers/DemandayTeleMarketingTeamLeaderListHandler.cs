using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeleMarketingTeamLeaderRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingTeamLeaderListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingTeamLeaderListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingTeamLeaderListHandler
    {
        public DemandayTeleMarketingTeamLeaderListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}