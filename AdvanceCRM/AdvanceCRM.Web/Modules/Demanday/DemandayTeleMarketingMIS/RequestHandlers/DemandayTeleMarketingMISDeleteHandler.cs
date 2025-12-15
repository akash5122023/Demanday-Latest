using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingMISRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingMISDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingMISDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingMISDeleteHandler
    {
        public DemandayTeleMarketingMISDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}