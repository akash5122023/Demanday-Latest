using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Toolkit.OpenCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface IOpenCampaignDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class OpenCampaignDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IOpenCampaignDeleteHandler
    {
        public OpenCampaignDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}