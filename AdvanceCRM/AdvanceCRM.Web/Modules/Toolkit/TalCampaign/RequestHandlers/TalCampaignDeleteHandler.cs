using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Toolkit.TalCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface ITalCampaignDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class TalCampaignDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, ITalCampaignDeleteHandler
    {
        public TalCampaignDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}