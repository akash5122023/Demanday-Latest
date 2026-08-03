using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Toolkit.TalCampaignRow>;
using MyRow = AdvanceCRM.Toolkit.TalCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface ITalCampaignRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class TalCampaignRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, ITalCampaignRetrieveHandler
    {
        public TalCampaignRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ValidatePermissions()
        {
            if (Permissions.HasPermission("TalCampaign:Read") ||
                Permissions.HasPermission("Toolkit:VerifySheets") ||
                Permissions.HasPermission("Toolkit:VerifySheets:TalList") ||
                Permissions.HasPermission("Toolkit:VerifySheets:TalCampaign"))
            {
                return;
            }

            base.ValidatePermissions();
        }
    }
}