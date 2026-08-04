using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Toolkit.ToolkitTMEnquiryRow>;
using MyRow = AdvanceCRM.Toolkit.ToolkitTMEnquiryRow;

namespace AdvanceCRM.Toolkit
{
    public interface IToolkitTMEnquiryListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ToolkitTMEnquiryListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IToolkitTMEnquiryListHandler
    {
        public ToolkitTMEnquiryListHandler(IRequestContext context)
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
