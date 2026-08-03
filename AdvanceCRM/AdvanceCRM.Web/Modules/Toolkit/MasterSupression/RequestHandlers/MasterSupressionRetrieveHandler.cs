using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Toolkit.MasterSupressionRow>;
using MyRow = AdvanceCRM.Toolkit.MasterSupressionRow;

namespace AdvanceCRM.Toolkit
{
    public interface IMasterSupressionRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class MasterSupressionRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IMasterSupressionRetrieveHandler
    {
        public MasterSupressionRetrieveHandler(IRequestContext context)
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