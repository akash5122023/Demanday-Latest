using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow>;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayTeleMarketingEnquiryQuestionAnswersRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryQuestionAnswersRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryQuestionAnswersRetrieveHandler
    {
        public DemandayTeleMarketingEnquiryQuestionAnswersRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}