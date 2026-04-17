using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow>;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayTeleMarketingEnquiryCampaignQuestionsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryCampaignQuestionsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryCampaignQuestionsListHandler
    {
        public DemandayTeleMarketingEnquiryCampaignQuestionsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}