using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeamLeaderRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeamLeaderDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeamLeaderDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeamLeaderDeleteHandler
    {
        public DemandayTeamLeaderDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}