using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquiryRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryRetrieveHandler
    {
        public DemandayTeleMarketingEnquiryRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}