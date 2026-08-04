using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Toolkit.ToolkitTMEnquiryRow;

namespace AdvanceCRM.Toolkit
{
    public interface IToolkitTMEnquiryDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ToolkitTMEnquiryDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IToolkitTMEnquiryDeleteHandler
    {
        public ToolkitTMEnquiryDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
