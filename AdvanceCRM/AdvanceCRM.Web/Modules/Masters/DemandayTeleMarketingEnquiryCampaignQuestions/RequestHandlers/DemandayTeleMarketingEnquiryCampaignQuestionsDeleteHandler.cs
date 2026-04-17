using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandayTeleMarketingEnquiryCampaignQuestionsRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayTeleMarketingEnquiryCampaignQuestionsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryCampaignQuestionsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryCampaignQuestionsDeleteHandler
    {
        public DemandayTeleMarketingEnquiryCampaignQuestionsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}