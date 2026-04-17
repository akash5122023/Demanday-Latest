using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.OpenCampaignRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.OpenCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface IOpenCampaignSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class OpenCampaignSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IOpenCampaignSaveHandler
    {
        public OpenCampaignSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}