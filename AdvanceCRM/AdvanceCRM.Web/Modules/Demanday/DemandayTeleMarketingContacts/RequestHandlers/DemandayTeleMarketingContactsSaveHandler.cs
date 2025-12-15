using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingContactsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingContactsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingContactsSaveHandler
    {
        public DemandayTeleMarketingContactsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}