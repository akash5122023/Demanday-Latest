using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.OpenCampaignRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.OpenCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface IOpenCampaignSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class OpenCampaignSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IOpenCampaignSaveHandler
    {
        public OpenCampaignSaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void SetInternalFields()
        {
            base.SetInternalFields();

            if (IsCreate)
            {
                var userId = Convert.ToInt32(User.GetIdentifier());
                Row.OwnerId = userId;

                // Dialog adds without a SrNo get the next free serial number.
                if (Row.SrNo == null)
                    Row.SrNo = ToolkitSrNoHelper.NextSrNo(Connection, "[dbo].[OpenCampaign]");

                // "Demanday User" and "Time Stamp" record who added the domain and when. Neither is
                // editable on the form, so until now nothing ever filled them in.
                if (Row.DemandayUserId == null)
                    Row.DemandayUserId = userId;

                if (Row.TimeStamp == null)
                    Row.TimeStamp = DateTime.Now;
            }
        }
    }
}
