using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.TalCampaignRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.TalCampaignRow;

namespace AdvanceCRM.Toolkit
{
    public interface ITalCampaignSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class TalCampaignSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ITalCampaignSaveHandler
    {
        public TalCampaignSaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void SetInternalFields()
        {
            base.SetInternalFields();

            // Dialog adds without a SrNo get the next free serial number.
            if (IsCreate && Row.SrNo == null)
                Row.SrNo = ToolkitSrNoHelper.NextSrNo(Connection, "[dbo].[TalCampaign]");
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            // An edit must never silently drop the row's assignments.
            //
            // The Agent editor is bound to "Administration.EnquiryUsersLookup", which only lists
            // users holding an Enquiry role, while the Excel import can assign ANY user. When the
            // assigned agent is missing from that lookup the editor cannot round-trip the value and
            // posts null — that wiped AgentsName on save, and because the list is filtered by agent
            // the record then belonged to nobody and disappeared from the grid.
            //
            // These fields are read-only on the form anyway, so an update that arrives without a
            // value means "unchanged", not "clear it".
            if (IsUpdate && Old != null)
            {
                if (Row.AgentsName == null)
                    Row.AgentsName = Old.AgentsName;
                if (Row.CampaignId == null)
                    Row.CampaignId = Old.CampaignId;
                if (Row.MasterAccountId == null)
                    Row.MasterAccountId = Old.MasterAccountId;
            }
        }
    }
}