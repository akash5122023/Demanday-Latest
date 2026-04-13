using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayTeleMarketingEnquiryCampaignQuestionsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryCampaignQuestionsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryCampaignQuestionsSaveHandler
    {
        public DemandayTeleMarketingEnquiryCampaignQuestionsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}