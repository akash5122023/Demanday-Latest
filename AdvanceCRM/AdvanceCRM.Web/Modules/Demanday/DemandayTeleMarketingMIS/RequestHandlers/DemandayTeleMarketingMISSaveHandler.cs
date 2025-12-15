using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeleMarketingMISRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingMISRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingMISSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingMISSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingMISSaveHandler
    {
        public DemandayTeleMarketingMISSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}