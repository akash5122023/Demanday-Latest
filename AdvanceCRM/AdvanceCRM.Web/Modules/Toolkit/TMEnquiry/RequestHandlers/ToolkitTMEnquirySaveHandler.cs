using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.ToolkitTMEnquiryRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.ToolkitTMEnquiryRow;

namespace AdvanceCRM.Toolkit
{
    public interface IToolkitTMEnquirySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ToolkitTMEnquirySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IToolkitTMEnquirySaveHandler
    {
        public ToolkitTMEnquirySaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
