using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Toolkit.ToolkitTMEnquiryRow>;
using MyRow = AdvanceCRM.Toolkit.ToolkitTMEnquiryRow;

namespace AdvanceCRM.Toolkit
{
    public interface IToolkitTMEnquiryRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ToolkitTMEnquiryRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IToolkitTMEnquiryRetrieveHandler
    {
        public ToolkitTMEnquiryRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ValidatePermissions()
        {
            if (Permissions.HasPermission("ToolkitTMEnquiry:Read") ||
                Permissions.HasPermission("Toolkit:VerifySheets") ||
                Permissions.HasPermission("Toolkit:VerifySheets:ToolkitTMEnquiry"))
            {
                return;
            }

            base.ValidatePermissions();
        }
    }
}
