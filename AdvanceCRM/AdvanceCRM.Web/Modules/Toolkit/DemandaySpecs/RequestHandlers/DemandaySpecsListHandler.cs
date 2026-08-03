using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Toolkit.DemandaySpecsRow>;
using MyRow = AdvanceCRM.Toolkit.DemandaySpecsRow;

namespace AdvanceCRM.Toolkit
{
    public interface IDemandaySpecsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandaySpecsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandaySpecsListHandler
    {
        public DemandaySpecsListHandler(IRequestContext context)
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