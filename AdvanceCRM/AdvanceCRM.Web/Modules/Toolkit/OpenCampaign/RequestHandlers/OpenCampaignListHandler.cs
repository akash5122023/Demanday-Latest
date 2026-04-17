using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Toolkit.OpenCampaignRow>;
using MyRow = AdvanceCRM.Toolkit.OpenCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface IOpenCampaignListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class OpenCampaignListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IOpenCampaignListHandler
    {
        public OpenCampaignListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}