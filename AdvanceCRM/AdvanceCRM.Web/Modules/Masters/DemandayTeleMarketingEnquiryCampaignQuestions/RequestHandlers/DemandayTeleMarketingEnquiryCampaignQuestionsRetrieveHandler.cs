using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow>;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayTeleMarketingEnquiryCampaignQuestionsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryCampaignQuestionsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryCampaignQuestionsRetrieveHandler
    {
        public DemandayTeleMarketingEnquiryCampaignQuestionsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}