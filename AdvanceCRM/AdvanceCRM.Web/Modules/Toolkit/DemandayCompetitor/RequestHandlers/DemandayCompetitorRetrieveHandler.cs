using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Toolkit.DemandayCompetitorRow>;
using MyRow = AdvanceCRM.Toolkit.DemandayCompetitorRow;

namespace AdvanceCRM.Toolkit
{
    public interface IDemandayCompetitorRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCompetitorRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCompetitorRetrieveHandler
    {
        public DemandayCompetitorRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ValidatePermissions()
        {
            if (Permissions.HasPermission("DemandayCompetitor:Read") ||
                Permissions.HasPermission("Toolkit:VerifySheets") ||
                Permissions.HasPermission("Toolkit:VerifySheets:Competitor") ||
                Permissions.HasPermission("Toolkit:VerifySheets:DemandayCompetitor"))
            {
                return;
            }

            base.ValidatePermissions();
        }
    }
}