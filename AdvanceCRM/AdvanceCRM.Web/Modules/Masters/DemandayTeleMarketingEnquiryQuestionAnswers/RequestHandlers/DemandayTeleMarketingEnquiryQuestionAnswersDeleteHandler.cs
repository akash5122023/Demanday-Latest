using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayTeleMarketingEnquiryQuestionAnswersDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryQuestionAnswersDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryQuestionAnswersDeleteHandler
    {
        public DemandayTeleMarketingEnquiryQuestionAnswersDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}