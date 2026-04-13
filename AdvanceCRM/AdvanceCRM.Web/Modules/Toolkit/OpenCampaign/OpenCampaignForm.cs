using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Forms
{
    [FormScript("Toolkit.OpenCampaign")]
    [BasedOnRow(typeof(OpenCampaignRow), CheckNames = true)]
    public class OpenCampaignForm
    {
        [Category("Open Campaign Details")]
        [HalfWidth]
        public Int32 MasterAccountId { get; set; }
        [HalfWidth]
        [LookupEditor(typeof(Masters.DemandayCampaignIdRow), CascadeFrom = "MasterAccountId", CascadeField = "DemandayMasterAccountId")]
        public String CampaignId { get; set; }
        [HalfWidth]
        public String Domain { get; set; }
        [HalfWidth]
        public Int32 DemandayUserId { get; set; }
        [HalfWidth, DateTimeEditor]
        public DateTime TimeStamp { get; set; }
        //[HalfWidth]
        //public Int32 ClientAccountId { get; set; }
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}