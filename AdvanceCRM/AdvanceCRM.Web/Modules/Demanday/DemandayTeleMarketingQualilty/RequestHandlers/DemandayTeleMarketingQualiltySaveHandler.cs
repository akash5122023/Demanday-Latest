using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingQualiltySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingQualiltySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingQualiltySaveHandler
    {
        public DemandayTeleMarketingQualiltySaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}