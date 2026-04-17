using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandayCampaignId")]
    [BasedOnRow(typeof(DemandayCampaignIdRow), CheckNames = true)]
    public class DemandayCampaignIdForm
    {
        public String CampaignId { get; set; }

        [LookupEditor(typeof(DemandayMasterAccountRow))]
        public Int32 DemandayMasterAccountId { get; set; }
    }
}