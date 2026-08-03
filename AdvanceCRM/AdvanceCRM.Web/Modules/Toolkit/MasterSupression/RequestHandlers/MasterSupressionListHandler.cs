using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Toolkit.MasterSupressionRow>;
using MyRow = AdvanceCRM.Toolkit.MasterSupressionRow;

namespace AdvanceCRM.Toolkit
{
    public interface IMasterSupressionListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class MasterSupressionListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IMasterSupressionListHandler
    {
        public MasterSupressionListHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ValidatePermissions()
        {
            if (Permissions.HasPermission("MasterSupression:Read") ||
                Permissions.HasPermission("Toolkit:VerifySheets") ||
                Permissions.HasPermission("Toolkit:VerifySheets:MasterSuppression"))
            {
                return;
            }

            base.ValidatePermissions();
        }
    }
}