using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryQADetailsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryQADetailsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquiryQADetailsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryQADetailsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryQADetailsSaveHandler
    {
        public DemandayTeleMarketingEnquiryQADetailsSaveHandler(IRequestContext context)
            : base(context)
        {
        }
    }
}
