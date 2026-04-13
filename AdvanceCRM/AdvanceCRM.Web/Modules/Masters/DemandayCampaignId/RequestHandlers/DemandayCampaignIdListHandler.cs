using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandayCampaignIdRow>;
using MyRow = AdvanceCRM.Masters.DemandayCampaignIdRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayCampaignIdListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCampaignIdListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCampaignIdListHandler
    {
        public DemandayCampaignIdListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}