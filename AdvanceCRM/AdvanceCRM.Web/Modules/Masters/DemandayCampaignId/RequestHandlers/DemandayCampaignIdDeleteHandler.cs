using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandayCampaignIdRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayCampaignIdDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCampaignIdDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCampaignIdDeleteHandler
    {
        public DemandayCampaignIdDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}