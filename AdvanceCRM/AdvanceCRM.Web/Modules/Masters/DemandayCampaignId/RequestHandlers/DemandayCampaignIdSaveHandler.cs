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

        // " 79580" and "79580" are the same campaign to a user, so the padding is stripped before
        // the row's UniqueConstraint compares it — otherwise the duplicate check would let it in.
        protected override void ValidateRequest()
        {
            if (Row.CampaignId != null)
                Row.CampaignId = Row.CampaignId.Trim();

            base.ValidateRequest();
        }
    }
}