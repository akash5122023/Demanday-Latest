using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Forms
{
    [FormScript("Toolkit.TalCampaign")]
    [BasedOnRow(typeof(TalCampaignRow), CheckNames = true)]
    public class TalCampaignForm
    {
        [Category("Tal Campaign Details")]
        [HalfWidth]
        public Int32 MasterAccountId { get; set; }

        [HalfWidth]
        [LookupEditor(typeof(Masters.DemandayCampaignIdRow), CascadeFrom = "MasterAccountId", CascadeField = "DemandayMasterAccountId")]
        public Int32 CampaignId { get; set; }
        [HalfWidth]
        public String CompanyName { get; set; }
        [HalfWidth]
        public String Domain { get; set; }

        [HalfWidth]
        public Int64 Cpc { get; set; }
        [HalfWidth]
        [LookupEditor("Administration.EnquiryUsersLookup")]
        public Int32 AgentsName { get; set; }
        [HalfWidth]
        public String Reason { get; set; }
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}