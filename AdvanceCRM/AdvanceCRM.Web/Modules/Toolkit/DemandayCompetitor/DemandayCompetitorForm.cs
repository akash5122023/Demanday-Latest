using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Forms
{
    [FormScript("Toolkit.DemandayCompetitor")]
    [BasedOnRow(typeof(DemandayCompetitorRow), CheckNames = true)]
    public class DemandayCompetitorForm
    {
        [Category("Competitor Details")]
        [HalfWidth, DisplayName("Sr No")]
        public Int32 SrNo { get; set; }
        [HalfWidth,Hidden]
        public Int32 MasterAccountId { get; set; }
        [HalfWidth, Hidden]
        [LookupEditor(typeof(Masters.DemandayCampaignIdRow), CascadeFrom = "MasterAccountId", CascadeField = "DemandayMasterAccountId")]
        public Int32 CampaignId { get; set; }
        [HalfWidth]
        public String CompanyName { get; set; }
        [HalfWidth]
        public String Domain { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        [HalfWidth]
        public String Cpc { get; set; }
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}