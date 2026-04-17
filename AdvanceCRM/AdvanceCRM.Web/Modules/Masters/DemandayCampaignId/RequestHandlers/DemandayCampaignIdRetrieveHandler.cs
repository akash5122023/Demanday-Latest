using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandayCampaignIdRow>;
using MyRow = AdvanceCRM.Masters.DemandayCampaignIdRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayCampaignIdRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCampaignIdRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCampaignIdRetrieveHandler
    {
        public DemandayCampaignIdRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}