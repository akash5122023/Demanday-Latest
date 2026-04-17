using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayTeleMarketingEnquiryQuestionAnswersSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryQuestionAnswersSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryQuestionAnswersSaveHandler
    {
        public DemandayTeleMarketingEnquiryQuestionAnswersSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}