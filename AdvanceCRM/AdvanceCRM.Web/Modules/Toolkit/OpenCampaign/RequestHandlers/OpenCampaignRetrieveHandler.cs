using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Toolkit.OpenCampaignRow>;
using MyRow = AdvanceCRM.Toolkit.OpenCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface IOpenCampaignRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class OpenCampaignRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IOpenCampaignRetrieveHandler
    {
        public OpenCampaignRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}