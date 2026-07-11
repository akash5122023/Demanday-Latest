using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Forms
{
    [FormScript("Toolkit.MasterSupression")]
    [BasedOnRow(typeof(MasterSupressionRow), CheckNames = true)]
    public class MasterSupressionForm
    {
        [Category("Master Supression Details")]
        [HalfWidth, DisplayName("Sr No")]
        public Int32 SrNo { get; set; }
        [HalfWidth]
        public Int32 MasterAccountId { get; set; }
        [HalfWidth]
        [LookupEditor(typeof(Masters.DemandayCampaignIdRow), CascadeFrom = "MasterAccountId", CascadeField = "DemandayMasterAccountId")]
        public Int32 CampaignId { get; set; }
        [HalfWidth]
        public String CompanyName { get; set; }
        [HalfWidth]
        public String FirstName { get; set; }
        [HalfWidth]
        public String LastName { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        [HalfWidth]
        public String Domain { get; set; }
        [HalfWidth, DateTimeEditor]
        public DateTime Date { get; set; }
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}