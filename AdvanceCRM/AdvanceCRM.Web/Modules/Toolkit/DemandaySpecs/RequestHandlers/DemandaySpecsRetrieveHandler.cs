using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Toolkit.DemandaySpecsRow>;
using MyRow = AdvanceCRM.Toolkit.DemandaySpecsRow;

namespace AdvanceCRM.Toolkit
{
    public interface IDemandaySpecsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandaySpecsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandaySpecsRetrieveHandler
    {
        public DemandaySpecsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ValidatePermissions()
        {
            if (Permissions.HasPermission("DemandaySpecs:Read") ||
                Permissions.HasPermission("Toolkit:VerifySheets") ||
                Permissions.HasPermission("Toolkit:VerifySheets:Specification"))
            {
                return;
            }

            base.ValidatePermissions();
        }
    }
}