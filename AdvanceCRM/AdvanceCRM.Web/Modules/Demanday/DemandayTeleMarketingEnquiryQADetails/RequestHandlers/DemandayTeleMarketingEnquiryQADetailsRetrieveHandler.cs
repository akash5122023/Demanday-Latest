using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryQADetailsRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryQADetailsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquiryQADetailsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryQADetailsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryQADetailsRetrieveHandler
    {
        public DemandayTeleMarketingEnquiryQADetailsRetrieveHandler(IRequestContext context)
            : base(context)
        {
        }
    }
}
