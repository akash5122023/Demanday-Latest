using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow>;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryQuestionAnswersRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayTeleMarketingEnquiryQuestionAnswersListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryQuestionAnswersListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryQuestionAnswersListHandler
    {
        public DemandayTeleMarketingEnquiryQuestionAnswersListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}