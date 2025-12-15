using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayTeleMarketingMISRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingMISRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingMISRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingMISRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingMISRetrieveHandler
    {
        public DemandayTeleMarketingMISRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}