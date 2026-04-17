using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandayCampaignIdRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandayCampaignIdRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayCampaignIdSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCampaignIdSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCampaignIdSaveHandler
    {
        public DemandayCampaignIdSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}