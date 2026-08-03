using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.DNCContact.Forms
{
    [FormScript("DNCContact.DncContacts")]
    [BasedOnRow(typeof(DncContactsRow), CheckNames = true)]
    public class DncContactsForm
    {
        [HalfWidth, DisplayName("Sr No")]
        public Int32 SrNo { get; set; }
        [HalfWidth]
        public Int32 MasterAccountId { get; set; }
        [HalfWidth]
        [LookupEditor(typeof(Masters.DemandayCampaignIdRow), CascadeFrom = "MasterAccountId", CascadeField = "DemandayMasterAccountId")]
        public Int32 CampaignId { get; set; }
        [HalfWidth]
        public String FirstName { get; set; }
        [HalfWidth]
        public String LastName { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        [HalfWidth]
        public String DncStatus { get; set; }
        [HalfWidth]
        public String Number { get; set; }
    }
}