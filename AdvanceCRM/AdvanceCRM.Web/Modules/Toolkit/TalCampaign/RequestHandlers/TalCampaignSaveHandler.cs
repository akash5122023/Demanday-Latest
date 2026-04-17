using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.TalCampaignRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.TalCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface ITalCampaignSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class TalCampaignSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ITalCampaignSaveHandler
    {
        public TalCampaignSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}