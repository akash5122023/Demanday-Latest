using Serenity;
using Serenity.Services;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.VerifySheet.TMEnquiryRow>;
using MyRow = AdvanceCRM.VerifySheet.TMEnquiryRow;

namespace AdvanceCRM.VerifySheet
{
    public interface ITMEnquiryListHandler : IListHandler<MyRow, MyRequest, MyResponse> { }

    public class TMEnquiryListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, ITMEnquiryListHandler
    {
        public TMEnquiryListHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ValidatePermissions()
        {
            if (Permissions.HasPermission("TMEnquiry:Read") ||
                Permissions.HasPermission("Toolkit:VerifySheets") ||
                Permissions.HasPermission("Toolkit:VerifySheets:TMEnquiry"))
            {
                return;
            }

            base.ValidatePermissions();
        }
    }
}
